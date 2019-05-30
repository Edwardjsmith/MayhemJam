using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {

    List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public const int slots = 4;

	// Use this for initialization
	void Awake()
    {
        for(int i = 0; i < slots; i++)
        {
            GameObject g = Instantiate(slotPrefab);
            g.transform.SetParent(slotPanel);
            uiItems.Add(g.GetComponent<UIItem>());
        }
	}

    public void updateSlot(int slot, Item item)
    {
        uiItems[slot].updateItem(item);
    }

    public void addNewItem(Item item)
    {
        updateSlot(uiItems.FindIndex(i => i.item == null), item);
    }

    public void removeItem(Item item)
    {
        updateSlot(uiItems.FindIndex(i => i.item == item), null);
    }
}
