using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePortal : MonoBehaviour
{
    private Vector2 target;
    private Transform greeportal;
    private Transform purpleportal;
    // Start is called before the first frame update
    void Start()
    {
        greeportal = GameObject.FindGameObjectWithTag("GreenPortal").GetComponent<Transform>();
        purpleportal = GameObject.FindGameObjectWithTag("FalseAnswer").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            //camera.main == fing.gameobject
            //rozwiazanie = referencja + inspector
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            purpleportal.position = new Vector2(target.x, target.y);
           
        }
        
    }
}
