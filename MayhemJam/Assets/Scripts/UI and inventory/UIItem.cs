using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler {

    public Item item;
    private UIItem selectedItem;
    private Image spriteImage;
	void Start ()
    {
        spriteImage = GetComponent<Image>();
        updateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
	}
	
    public void updateItem(Item item)
    {
        this.item = item;

        if(this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.item != null)
        {
            if (selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);
                selectedItem.updateItem(this.item);
                updateItem(clone);
            }
            else
            {
                selectedItem.updateItem(this.item);
                updateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            updateItem(selectedItem.item);
            selectedItem.updateItem(null);
        }
    }
}
