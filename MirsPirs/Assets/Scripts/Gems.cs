using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    public int GemValue = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GemScoreManager.instanceGem.ChangeScoreGems(GemValue);
        }

    }

}
