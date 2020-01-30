using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInterObj = null;

    public InteractionObject currentInterObjScript = null;

    public Inventory inventory;


    private void Update()
    {
        if (Input.GetButtonDown("Interact") && currentInterObj)
        {
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
            }
            if (currentInterObjScript.looked)
            {
                if (inventory.FindItem(currentInterObjScript.itemNeeded))
                {
                    currentInterObjScript.looked = false;
                }
            }
            else
            {
                currentInterObjScript.Open();
            }
        }

        if (Input.GetButtonDown("Use potion"))
        {
            GameObject potion = inventory.FindItemByType("HealthPotion");
            if (potion != null)
            { 
                
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("interObject"))
        {
            Debug.Log(other.name);
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("interObject"))
        {
            if (other.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }
    }
}
