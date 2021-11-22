using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if ( rb.position.y > 6f ||
             rb.position.y < -6f ||
             rb.position.x > 9.5f ||
             rb.position.x < -9.5f
            )
        {
            Destroy(gameObject);
        }
    }
}
