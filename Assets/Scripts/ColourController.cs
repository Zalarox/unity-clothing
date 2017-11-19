using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;
using System.Linq;

public class ColourController : MonoBehaviour {

    public Text questionText;
    public GameObject fireworks;
    public Sprite filledStar;
    GameObject[] stars;
    int starIndex = 0;
    ParticleEmitter fireworkEmit;

    GameObject shirt, pants;
    Renderer shirtR, pantsR;
    Equipment equipment;

    Dictionary<string, Color32> clothColours;
    string shirtColour;

    private enum Combination { None, Good, Bad };
    private bool choosePants = false;
    private Combination currentCombination = Combination.None;
    
    void InitialiseColours()
    {
        clothColours = new Dictionary<string, Color32>();
        clothColours.Add("blue", new Color32(150, 208, 255, 255));
        clothColours.Add("green", new Color32(204, 255, 150, 255));
        clothColours.Add("pink", new Color32(255, 175, 255, 255));
        clothColours.Add("orange", new Color32(255, 218, 61, 255));
    }

    void Start()
    {
        StartCoroutine(FetchDetails("Siddhant"));
        questionText.text = "What should be the colour of the shirt?";
        equipment = GetComponent<Equipment>();
        fireworkEmit = fireworks.GetComponent<ParticleEmitter>();
        stars = GameObject.FindGameObjectsWithTag("star").OrderBy(g=>g.transform.GetSiblingIndex()).ToArray();
        equipment.InitializeEquipptedItemsList();
        EquipItem("Legs", "pants2");
        EquipItem("Chest", "shirt2");
        shirt = gameObject.transform.Find("shirt2").gameObject;
        pants = gameObject.transform.Find("pants2").gameObject;
        InitialiseColours();
    }

    Combination CheckCombination(string shirt, string pant)
    {
        if(pant == "blue" || pant == "orange")
        {
            StartCoroutine(Upload(shirt, pant));
            return Combination.Good;
        }
        else
        {
            return Combination.Bad;
        }
    }

    void ChangeQuestion()
    {
        if (choosePants == false)
        {
            questionText.text = "What should be the colour of the shirt?";
            StopCoroutine(Wait3());
        }
        else if (choosePants == true && currentCombination == Combination.None)
        {
            questionText.text = "What should be the colour of the trousers?";
        }
        else if (choosePants == true && currentCombination == Combination.Good)
        {
            questionText.text = "That is a good combination! Well done. Try another...";
            choosePants = false;
            currentCombination = Combination.None;

            //Vector3 pos = Camera.main.ScreenToWorldPoint(stars[starIndex].transform.position);
            //fireworkEmit.transform.position = pos;
            if (starIndex < 6)
            {
                stars[starIndex].GetComponent<Image>().sprite = filledStar;
                starIndex++;
            }
            fireworkEmit.Emit();
            //StartCoroutine(Upload());
            StartCoroutine(Wait3());
        }
        else
        {
            questionText.text = "That is not a good combination, try again!";
            choosePants = false;
            currentCombination = Combination.None;
            StartCoroutine(Wait3());
        }
    }

    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(3f);
        ChangeQuestion();
    }

    IEnumerator Upload(string shirt, string pant)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "Siddhant");
        form.AddField("shirtcolour", shirt);
        form.AddField("pantcolour", pant);
        WWW req = new WWW("localhost:3000/api/colourcombination", form);
        yield return req;

        if (!string.IsNullOrEmpty(req.error))
        {
            Debug.Log(req.error);
        }
        else
        {
            Debug.Log("Successfully posted! " + req.text);
        }

    }

    IEnumerator FetchDetails(string name)
    {
        WWW res = new WWW("localhost:3000/api/colourcombination/" + name);
        yield return res;

        if (!string.IsNullOrEmpty(res.error))
        {
            Debug.Log(res.error);
        }
        else
        {
            Debug.Log("Successfully Got " + res.text);
            JSONNode parsed = JSON.Parse(res.text);
            for(int i=0; i<parsed.Count; i++)
            {
                Debug.Log(parsed[i]["shirtcolour"].Value);
                Debug.Log(parsed[i]["pantcolour"].Value);
            }
        }

    }

    public void ChangeColour(string newColor)
    {
        newColor = newColor.ToLower();

        if(!clothColours.ContainsKey(newColor))
        {
            Debug.LogError("Specified colour not found.");
        }
        else
        {
            if(choosePants == false)
            {
                shirt.renderer.material.SetColor("_Color", clothColours[newColor]);
                choosePants = true;
                shirtColour = newColor;
                ChangeQuestion();
            }
            else
            {
                pants.renderer.material.SetColor("_Color", clothColours[newColor]);
                currentCombination = CheckCombination(shirtColour, newColor);
                ChangeQuestion();
            }
        }
    }

    public void EquipItem(string itemType, string itemSlug)
    {
        for (int i = 0; i < equipment.equippedItems.Count; i++)
        {
            if (equipment.equippedItems[i].ItemType == itemType)
            {
                equipment.equippedItems[i] = ItemDatabase.instance.FetchItemBySlug(itemSlug);
                equipment.AddEquipment(equipment.equippedItems[i]);
                break;
            }
        }
    }

    public void UnequipItem(string itemType, string itemSlug)
    {
        for (int i = 0; i < equipment.equippedItems.Count; i++)
        {
            if (equipment.equippedItems[i].ItemType == itemType)
            {
                equipment.RemoveEquipment(equipment.equippedItems[i]);
                break;
            }
        }
    }
}
