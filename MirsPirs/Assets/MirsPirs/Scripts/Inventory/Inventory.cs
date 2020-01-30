using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }









    public GameObject[] inventory = new GameObject[10];

    public void AddItem(GameObject item)
    {
        bool itemAdded = false;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                Debug.Log(item.name + "was added");
                itemAdded = true;

                item.SendMessage("DoInteraction");
                break;
            }
        }
        if (!itemAdded)
        {
            Debug.Log("Inventory is FULL - Item not added :(");
        }
    }
    public bool FindItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                return true;
            }
        }
        return false;
    }
    public GameObject FindItemByType(string itemType)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
                if (inventory[i].GetComponent<InteractionObject>().itemType == itemType)
                {
                    return inventory[i];
                }
        }
        return null;
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                inventory[i] = null;
                Debug.Log(item.name + "was removed");
                break;
            }
        }
    }
}
