using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatText : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
