
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    ItemDatabase database;
    UIInventory uiInventory;
    List<Item> inventory = new List<Item>();

    // Use this for initialization
    void Start()
    {
        database = FindObjectOfType<ItemDatabase>();
        uiInventory = FindObjectOfType<UIInventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addItem(string itemName)
    {
        Item itemToAdd = database.getItem(itemName);
        inventory.Add(itemToAdd);
        uiInventory.addNewItem(itemToAdd);
    }

    public void addItem(int itemID)
    {
        Item itemToAdd = database.getItem(itemID);
        inventory.Add(itemToAdd);
        uiInventory.addNewItem(itemToAdd);
    }

    public void removeItem(int id)
    {
        Item itemToRemove = checkInventory(id);
        if (itemToRemove != null)
        {
            inventory.Remove(itemToRemove);
            uiInventory.removeItem(itemToRemove);
        }
    }

    public Item checkInventory(int ID)
    {
        return inventory.Find(item => item.id == ID);
    }
}
