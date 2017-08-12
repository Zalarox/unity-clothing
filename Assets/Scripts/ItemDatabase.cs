﻿using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    public static ItemDatabase instance; 
    private void Awake()
    {
        //create singlton
        instance = this; 

        //naked
        itemList.Add(new Item(0, "", "", "naked_legs", "Legs"));
        itemList.Add(new Item(1, "", "", "naked_chest", "Chest"));
        itemList.Add(new Item(2, "", "", "bald_head", "Hair"));
        itemList.Add(new Item(3, "", "", "naked_feet", "Feet"));
        //clothing
        itemList.Add(new Item(50, "", "", "pants", "Legs", (GameObject)Resources.Load("Gear/pants")));
        itemList.Add(new Item(51, "", "", "boots", "Feet", (GameObject)Resources.Load("Gear/boots")));
        itemList.Add(new Item(54, "", "", "shirt", "Chest", (GameObject)Resources.Load("Gear/shirt")));
        //hair and beard
        itemList.Add(new Item(200, "", "", "long_hair", "Hair", (GameObject)Resources.Load("Gear/long_hair")));
        //itemList.Add(new Item(201, "", "", "beard", "Beard", (GameObject)Resources.Load("Gear/beard")));
        //itemList.Add(new Item(201, "", "", "mustache", "Mustache", (GameObject)Resources.Load("Gear/mustache")));
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ItemID == id)
            {
                return itemList[i];
            }
        }
        return null;
    }

    public Item FetchItemBySlug(string slugName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {

            if (itemList[i].Slug == slugName)
            {
                return itemList[i];
            }
        }
        return null;
    }


}