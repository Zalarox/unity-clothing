using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColourController : MonoBehaviour {

    public Text questionText;
    GameObject shirt, pants;
    Renderer shirtR, pantsR;
    Equipment equipment;

    private int i = 0;

    void Start()
    {
        questionText.text = "What should be the colour of the shirt?";
        equipment = GetComponent<Equipment>();
        equipment.InitializeEquipptedItemsList();
        EquipItem("Legs", "pants2");
        EquipItem("Chest", "shirt2");
        shirt = gameObject.transform.Find("shirt2").gameObject;
        pants = gameObject.transform.Find("pants2").gameObject;
        //shirtR = shirt.GetComponent<Renderer>();
        //pantsR = pants.GetComponent<Renderer>();
    }

    void ChangeQuestion()
    {
        questionText.text = "What should be the colour of the trousers?";
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "Siddhant");
        //form.AddField("score", score);
        form.AddField("gametype", "2D");

        WWW req = new WWW("localhost:3000/api/newshapedata", form);
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
        if(newColor.ToLower().Equals("blue"))
        {
            if(i==0)
            {              
                shirt.renderer.material.SetColor("_Color", new Color32(150, 208, 255, 255));
                i = 1;
                ChangeQuestion();
            }
            else
            {
                pants.renderer.material.SetColor("_Color", new Color32(150, 208, 255, 255));
            }
        }
        else if (newColor.ToLower().Equals("orange"))
        {
            if (i == 0)
            {
                shirt.renderer.material.SetColor("_Color", new Color32(255, 218, 61, 255));
                i = 1;
                ChangeQuestion();
            }
            else
            {
                pants.renderer.material.SetColor("_Color", new Color32(255, 218, 61, 255));
            }
        }
        else if (newColor.ToLower().Equals("pink"))
        {
            if (i == 0)
            {
                shirt.renderer.material.SetColor("_Color", new Color32(255, 175, 255, 255));
                i = 1;
                ChangeQuestion();
            }
            else
            {
                pants.renderer.material.SetColor("_Color", new Color32(255, 175, 255, 255));
            }
        }
        else if (newColor.ToLower().Equals("green"))
        {
            if (i == 0)
            {
                shirt.renderer.material.SetColor("_Color", new Color32(204, 255, 150, 255));
                i = 1;
                ChangeQuestion();
            }
            else
            {
                pants.renderer.material.SetColor("_Color", new Color32(204, 255, 150, 255));
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
