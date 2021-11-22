using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public GameObject scoreText;

    public float score;
    public float timeElapsed = 0;

    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + timeElapsed.ToString("0");
        score = timeElapsed;
    }
}
