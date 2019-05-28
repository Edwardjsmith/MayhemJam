using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Inventory : MonoBehaviour
{
    const int bagSpace = 4;
    Dictionary<string, Sprite> items;
    public Sprite[] currentItems = new Sprite[bagSpace];
    public Image[] slots = new Image[bagSpace];

    Sprite defaultSprite;

    // Use this for initialization
    void Start()
    {
        items = new Dictionary<string, Sprite>();

        foreach (string file in Directory.GetFiles("Sprites"))
        {
            string itemName = Path.GetFileNameWithoutExtension(file);
            items.Add(itemName, (Sprite)Resources.Load(file));

            if (itemName == "default")
            {
                defaultSprite = items[itemName];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addItem(string itemName)
    {
        for (int i = 0; i < currentItems.Length; i++)
        {
            if (currentItems[i] == defaultSprite)
            {
                currentItems[i] = items[itemName];
            }
        }
    }

    public void removeItem(int position)
    {
        currentItems[position] = defaultSprite;
    }
}
