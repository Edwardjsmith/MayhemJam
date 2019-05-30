using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void addItem(Item item)
    {
        items.Add(item);
    }

    void removeItem(Item item)
    {
        if(items.Contains(item))
        {
            items.Remove(item);
        }
    }

    public Item getItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item getItem(string name)
    {
        return items.Find(item => item.name == name);
    }
}
