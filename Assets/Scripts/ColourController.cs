using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ColourController : MonoBehaviour {

    public Text questionText;
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
        questionText.text = "What should be the colour of the shirt?";
        equipment = GetComponent<Equipment>();
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
        }
        else if (choosePants == true && currentCombination == Combination.None)
        {
            questionText.text = "What should be the colour of the trousers?";
        }
        else if (choosePants == true && currentCombination == Combination.Good)
        {
            questionText.text = "That is a good combination! Well done.";
        }
        else
        {
            questionText.text = "That is not a good combination, try again!";
        }
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "Siddhant");
        form.AddField("colorcombination", "");

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

        Application.Quit();
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
        //if(newColor.ToLower().Equals("blue"))
        //{
        //    if(choosePants == false)
        //    {              
        //        shirt.renderer.material.SetColor("_Color", new Color32(150, 208, 255, 255));
        //        choosePants = true;
        //        ChangeQuestion();
        //    }
        //    else
        //    {
        //        pants.renderer.material.SetColor("_Color", new Color32(150, 208, 255, 255));
        //    }
        //}
        //else if (newColor.ToLower().Equals("orange"))
        //{
        //    if (choosePants == false)
        //    {
        //        shirt.renderer.material.SetColor("_Color", new Color32(255, 218, 61, 255));
        //        choosePants = true;
        //        ChangeQuestion();
        //    }
        //    else
        //    {
        //        pants.renderer.material.SetColor("_Color", new Color32(255, 218, 61, 255));
        //    }
        //}
        //else if (newColor.ToLower().Equals("pink"))
        //{
        //    if (choosePants == false)
        //    {
        //        shirt.renderer.material.SetColor("_Color", new Color32(255, 175, 255, 255));
        //        choosePants = true;
        //        ChangeQuestion();
        //    }
        //    else
        //    {
        //        pants.renderer.material.SetColor("_Color", new Color32(255, 175, 255, 255));
        //    }
        //}
        //else if (newColor.ToLower().Equals("green"))
        //{
        //    if (choosePants == false)
        //    {
        //        shirt.renderer.material.SetColor("_Color", new Color32(204, 255, 150, 255));
        //        choosePants = true;
        //        ChangeQuestion();
        //    }
        //    else
        //    {
        //        pants.renderer.material.SetColor("_Color", new Color32(204, 255, 150, 255));
        //    }
        //}
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
