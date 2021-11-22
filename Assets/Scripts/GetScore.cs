using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{
    private float score;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetFloat("score");
        Debug.Log(score);
        this.GetComponent<TextMeshProUGUI>().text = "SCORE: " + score.ToString("0");
    }
}
