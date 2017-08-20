using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        equipmentScript = GetComponent<Equipment>();
        equipmentScript.InitializeEquipptedItemsList();
        scoreManager = GetComponent<ScoreManager>();
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
            scoreManager.IncreaseScore();
            chooseRandom();
        }
        else
        {
            audioSource.PlayOneShot(lose, 0.7F);
            scoreManager.DecreaseScore();
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
            scoreManager.IncreaseScore();
            chooseRandom();
        }
        else
        {
            audioSource.PlayOneShot(lose, 0.7F);
            scoreManager.DecreaseScore();
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
            scoreManager.IncreaseScore();
            chooseRandom();
        }
        else
        {
            audioSource.PlayOneShot(lose, 0.7F);
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