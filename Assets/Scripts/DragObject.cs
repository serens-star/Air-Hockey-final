
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private const float movespeed = 5f;
    [SerializeField] private float minX, maxX, minY, maxY; // Define boundary limits
    [SerializeField] private float MaxEnemySpeed = 20;
    [SerializeField] private float moveSpeed = movespeed;
    [SerializeField] private float forcePower;

    private bool isClicked = true;
    private Rigidbody2D rb;
    private bool canMove;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        print("start");
    }

    void Update()
    {
        if (gameObject.CompareTag(("Player")))
        {
            if (Input.GetMouseButton(0))//if user clicks left mouse button do the following:
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D playerCol = gameObject.GetComponent<Collider2D>();

                if (isClicked)
                {
                    if (playerCol.OverlapPoint(mousePos))//when LMB is clickd, find out whether th click was within the playerPuck's collidr, if yes then move the playrPuck
                    {
                        canMove = true;
                        isClicked = false;
                    }
                    else//if the user's click is not within the playerPuck's collider, do not move the playrPuck
                    {
                        canMove = false;
                    }
                }

                if (canMove)//handles the movment for the playrPuck when permission has been given
                {
                    transform.position = mousePos;
                    rb.MovePosition(mousePos);

                    mousePos.x = Mathf.Clamp(mousePos.x, minX, maxX);//creats boundaries so that th playerPuck may not leave the game screen
                    mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);
                    transform.position = mousePos;
                }
            }
        }
        if(gameObject.CompareTag(("AI")))//if this script is attached to the AI
        {
            Puck puck = FindObjectOfType<Puck>();

            if (puck.AI_can_Attack == true)//when the Puck enters the AI's field the AI is given permission to attack/go after the ball
            {
                rb.MovePosition(Vector2.MoveTowards(rb.position, puck.transform.position, MaxEnemySpeed * Time.deltaTime));
            }
            else if (puck.AI_can_Attack == false)//without permission, AI stops moving. Permission is taken away when Puck leaves the AI's field
            {
                transform.position = new Vector2(transform.position.x, transform.position.y);
                rb.velocity = new Vector2(0, 0);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Puck"))//when object that this script is attached to collides with the Puck, give the puck an extra boost
        {
            Vector2 forceDirection = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * forcePower, ForceMode2D.Impulse);
        }
    }
}
