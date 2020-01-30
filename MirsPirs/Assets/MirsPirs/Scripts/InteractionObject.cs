using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public bool inventory;
    public bool openable;
    public bool looked;
    public GameObject itemNeeded;
    public Animator anim;
    public string itemType;
    public void DoInteraction()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        Destroy(gameObject);
    }

}
