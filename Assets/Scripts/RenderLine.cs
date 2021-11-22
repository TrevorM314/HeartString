using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RenderLine : MonoBehaviour
{
    private LineRenderer lr;
    
    public GameObject p1;
    public GameObject p2;
    public GameObject ScoreKeeper;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        Vector2 p1Position = p1.GetComponent<Rigidbody2D>().position;
        Vector2 p2Position = p2.GetComponent<Rigidbody2D>().position;

        lr.SetPosition(0, p1Position);
        lr.SetPosition(1, p2Position);
        
        RaycastHit2D lineBroke = Physics2D.Raycast(p1Position, p2Position-p1Position, Vector2.Distance(p1Position, p2Position));
        if (lineBroke && lineBroke.transform.gameObject.CompareTag("Projectile"))
        {
            GameOver();
        }
        
    }

    private void GameOver()
    {
        //Handle Game Over logic
        PlayerPrefs.SetFloat("score", ScoreKeeper.GetComponent<ScoreKeeper>().score);
        SceneManager.LoadScene("Game Over");
    }
}
