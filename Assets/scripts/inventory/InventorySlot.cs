﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private List<Loot> items = new List<Loot>();
    private Image image;
    private Text text;
    private InventoryTooltip tooltip;

    void Start()
    {
        image = transform.FindChild("Image").GetComponent<Image>();
        text = transform.FindChild("Text").GetComponent<Text>();
        tooltip = GameObject.Find("InventoryTooltip").GetComponent<InventoryTooltip>();
    }

    public bool IsEmpty()
    {
        return items.Count <= 0;
    }

    public void AddItem(Loot item)
    {
        items.Add(item);
        image.sprite = item.sprite;
        text.text = "" + items.Count;
        item.AddToInventorySlot(this);
    }

    public void RemoveItem(Loot item)
    {
        items.Remove(item);

        Debug.Log("REMOVE" + item.title);

        if (IsEmpty())
        {
            image.sprite = null;
            text.text = "";
        }
        else
        {
            text.text = "" + items.Count;
        }
    }

    public bool CanAddItem(Loot item)
    {
        return IsEmpty() || (items[0].stackable && items[0].title == item.title);
    }

    public bool SetOrAdd(Loot item)
    {
        if (CanAddItem(item))
        {
            AddItem(item);
            return true;
        }

        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsEmpty())
        {
            Loot item = items[items.Count - 1];
            item.AddToInventorySlot(this);
            tooltip.transform.position = transform.position;
            tooltip.Activate(item);
        }
    }
}
