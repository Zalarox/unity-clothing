using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Equipment equipmentScript;
    private ScoreManager scoreManager;
    private List<string> questions = new List<string>(new string[] { "What would you wear in the Summers?", "What would you wear in the Winters?", "What would you wear at a marriage?" });
    private int currentIndex;
    public Text question;

    public AudioClip win;
    public AudioClip lose;
    public AudioSource audioSource;

    Image summer, winter, marriage;

    IEnumerator UploadAndExit()
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

    private void chooseRandom()
    {
        if (questions.Count > 0)
        {
            currentIndex = Random.Range(0, questions.Count);
            string selQuestion = questions[currentIndex];
            question.text = selQuestion;
            questions.RemoveAt(currentIndex);
        }
    }

    IEnumerator changeColour(Image img, Color col)
    {
        img.color = col;
        yield return new WaitForSeconds(2);
        img.color = Color.white;
    }

    private void Start()
    {
        equipmentScript = GetComponent<Equipment>();
        equipmentScript.InitializeEquipptedItemsList();
        scoreManager = GetComponent<ScoreManager>();
        summer = GameObject.Find("Summer").GetComponent<Image>();
        winter = GameObject.Find("Winter").GetComponent<Image>();
        marriage = GameObject.Find("Marriage").GetComponent<Image>();
        chooseRandom();
    }

    public void EquipWinter()
    {
        if (question.text == "What would you wear in the Winters?")
        {
            equipmentScript.UnequipAll();
            EquipItem("Legs", "trousers");
            EquipItem("Chest", "sweater");
            audioSource.PlayOneShot(win, 0.7F);
            StartCoroutine(changeColour(winter, Color.green));
            scoreManager.IncreaseScore();
            chooseRandom();
        }
        else
        {
            audioSource.PlayOneShot(lose, 0.7F);
            scoreManager.DecreaseScore();
            Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
            if (img != null)
            {
                StartCoroutine(changeColour(img, Color.red));
            }
        }
    }

    public void EquipSummer()
    {
        if (question.text == "What would you wear in the Summers?")
        {
            equipmentScript.UnequipAll();
            EquipItem("Legs", "pants");
            EquipItem("Chest", "shirt");
            audioSource.PlayOneShot(win, 0.7F);
            StartCoroutine(changeColour(summer, Color.green));
            scoreManager.IncreaseScore();
            chooseRandom();
        }
        else
        {
            audioSource.PlayOneShot(lose, 0.7F);
            scoreManager.DecreaseScore();
            Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
            if (img != null)
            {
                StartCoroutine(changeColour(img, Color.red));
            }
        }
    }

    public void EquipMarriage()
    {
        if (question.text == "What would you wear at a marriage?")
        {
            equipmentScript.UnequipAll();
            EquipItem("Legs", "pajama");
            EquipItem("Chest", "kurta");
            audioSource.PlayOneShot(win, 0.7F);
            StartCoroutine(changeColour(marriage, Color.green));
            scoreManager.IncreaseScore();
            chooseRandom();
        }
        else
        {
            audioSource.PlayOneShot(lose, 0.7F);
            Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
            if (img != null)
            {
                StartCoroutine(changeColour(img, Color.red));
            }
            scoreManager.DecreaseScore();
        }
    }

    public void EquipItem(string itemType, string itemSlug)
    {
        for (int i = 0; i < equipmentScript.equippedItems.Count; i++)
        {
            if (equipmentScript.equippedItems[i].ItemType == itemType)
            {
                equipmentScript.equippedItems[i] = ItemDatabase.instance.FetchItemBySlug(itemSlug);
                equipmentScript.AddEquipment(equipmentScript.equippedItems[i]);
                break;
            }
        }
    }

    public void UnequipItem(string itemType, string itemSlug)
    {
        for (int i = 0; i < equipmentScript.equippedItems.Count; i++)
        {
            if (equipmentScript.equippedItems[i].ItemType == itemType)
            {
                equipmentScript.RemoveEquipment(equipmentScript.equippedItems[i]);
                break;
            }
        }
    }
}