using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemScoreManager : MonoBehaviour
{
    public static GemScoreManager instanceGem;
    public TextMeshProUGUI text;
    int scoreGems;
    void Start()
    {
        if (instanceGem == null)
        {
            instanceGem = this;
        }
    }

    public void ChangeScoreGems(int coinGem)
    {
        scoreGems += coinGem;
        text.text = "x" + scoreGems.ToString();
    }
}
