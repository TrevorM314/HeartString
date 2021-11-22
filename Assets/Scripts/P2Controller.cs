using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controller : MonoBehaviour
{
    public GameObject cam;
    private float sceneHeight;
    private float sceneWidth;

    public float movementSpeed = 6f;
    private Rigidbody2D rb;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sceneHeight = 2f * cam.GetComponent<Camera>().orthographicSize;
        sceneWidth = sceneHeight * cam.GetComponent<Camera>().aspect;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xDir = 0;
        if (Input.GetKey("right")) xDir = 1;
        else if (Input.GetKey("left")) xDir = -1;
        if (Input.GetKey("right") && Input.GetKey("left")) xDir = 0;

        float yDir = 0;
        if (Input.GetKey("up")) yDir = 1;
        else if (Input.GetKey("down")) yDir = -1;
        if (Input.GetKey("up") && Input.GetKey("down")) yDir = 0;

        if (xDir != 0 || yDir != 0) animator.SetBool("p2Running", true);
        else animator.SetBool("p2Running", false);

        rb.velocity = new Vector2(xDir * movementSpeed, yDir * movementSpeed);
    }
}
