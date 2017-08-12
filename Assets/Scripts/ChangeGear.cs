using UnityEngine;

public class ChangeGear : MonoBehaviour
{
    private Equipment equipmentScript; 

    private void Start()
    {
        equipmentScript = GetComponent<Equipment>();
        equipmentScript.InitializeEquipptedItemsList();
        //EquipItem("Legs", "pants"); 
    }

    public void EquipWinter()
    {
        EquipItem("Legs", "pants");
        EquipItem("Chest", "shirt");
        //EquipItem("Feet", "boots");
    }

    public void EquipSummer()
    {

    }

    public void EquipMarriage()
    {

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