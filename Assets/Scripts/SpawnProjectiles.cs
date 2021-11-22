using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject cam;
    private float sceneWidth;
    private float sceneHeight;

    public float projectileForce;

    public float averageTimeBetweenSpawn = 100f;
    // Units in percent of average time
    public float spawnTimeVariance = .1f;
    private float framesLeftUntilSpawn = 20f;

    public float angleVariance = 50;

    private float secondsSinceSpeedup = 0;
    //Seconds between speedup
    public float speedupRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        sceneHeight = 2f * cam.GetComponent<Camera>().orthographicSize;
        sceneWidth = sceneHeight * cam.GetComponent<Camera>().aspect;
        secondsSinceSpeedup = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        framesLeftUntilSpawn -= 1;
        if (framesLeftUntilSpawn <= 0)
        {
            SpawnProjectile();
            framesLeftUntilSpawn = Random.Range(
                averageTimeBetweenSpawn - (int)spawnTimeVariance, 
                averageTimeBetweenSpawn + (int)spawnTimeVariance
            );
            if (framesLeftUntilSpawn < 0) framesLeftUntilSpawn = 0;
        }

        if ( secondsSinceSpeedup > speedupRate )
        {
            averageTimeBetweenSpawn = averageTimeBetweenSpawn > 1 ? averageTimeBetweenSpawn - 1 : averageTimeBetweenSpawn * .7f;
            //Debug.Log(averageTimeBetweenSpawn);
            secondsSinceSpeedup = 0;
        }
        secondsSinceSpeedup += Time.deltaTime;
    }

    private void SpawnProjectile()
    {
        float startY, startX;
        int topBottom = Random.Range(0, 2);
        int posOrNegEdge = Random.Range(0, 2) == 1 ? 1 : -1;
        if (topBottom == 1)
        {
            startY = posOrNegEdge * .5f * sceneHeight;
            startX = Random.Range(-.5f * sceneWidth, .5f * sceneWidth);
        }
        else
        {
            startY = Random.Range(-.5f * sceneHeight, .5f * sceneHeight);
            startX = posOrNegEdge * .5f * sceneWidth;
        }

        Quaternion rotation = CalculateProjectileRotation(startX, startY);
        GameObject projectile = Instantiate(projectilePrefab, new Vector2(startX,startY), rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(projectile.transform.up * projectileForce, ForceMode2D.Impulse);
    }

    private Quaternion CalculateProjectileRotation(float xPos, float yPos)
    {
        float originToPoint = Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;
        originToPoint = Mathf.Abs(originToPoint) < 90f ? Mathf.Abs(originToPoint) : 180 - Mathf.Abs(originToPoint);
        float aimDir = 90 - originToPoint;
        float rot;
        
        if (xPos > 0 && yPos > 0) rot = Random.Range(180-aimDir - angleVariance, 180-aimDir + angleVariance);
        else if (xPos > 0 && yPos < 0) rot = Random.Range(aimDir - angleVariance, aimDir + angleVariance);
        else if (xPos < 0 && yPos > 0) rot = Random.Range(180 + aimDir - angleVariance, 180 + aimDir + angleVariance);
        else if (xPos < 0 && yPos < 0) rot = Random.Range(0 - aimDir - angleVariance, 0 - aimDir + angleVariance);
        else rot = Random.Range(0, 360);

        return Quaternion.Euler(0, 0, rot);
    }
}
