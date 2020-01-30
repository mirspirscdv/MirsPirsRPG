 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    
    public bool isPurple;
    public float distance = 0.3f;
    void Start()
    {
        if (isPurple == false)
        {
            //wymienic find na inne rozwiazanie
            destination = GameObject.FindGameObjectWithTag("GreenPortal").GetComponent<Transform>();
            
            
        } else
        {
            destination = GameObject.FindGameObjectWithTag("FalseAnswer").GetComponent<Transform>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            other.transform.position = new Vector2(destination.position.x, destination.position.y);
            
        }

    }
}
