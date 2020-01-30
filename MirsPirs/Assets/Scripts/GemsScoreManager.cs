using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemsScoreManager : MonoBehaviour
{
    public static GemsScoreManager instancegems;
    public TextMeshProUGUI text;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        if (instancegems == null)
        {
            instancegems = this;
        }
    }

    // Update is called once per frame
    public void ChangeScoreGems(int gemValue)
    {
        score += gemValue;
        text.text = "x" + score.ToString();
        
    }
}
