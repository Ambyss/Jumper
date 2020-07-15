using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    int force;
    float move;
    bool up;
    GameControl GC;
    [SerializeField]
    private GameObject bum;

    private void Start()
    {
        up = true;
        force = 400;
        GC = GameObject.Find("UpSpikes").GetComponent<GameControl>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, force));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) Move();
        if (collision.gameObject.CompareTag("Spike")) Death();
    }
    
    void Move()
    {
        force += 10;
        GC.AddScore();
        GC.ChangeSpikes(up);
        if (up)
        {
            rb.AddForce(new Vector2(0, -force));
            up = false;
        }
        else
        {
            rb.AddForce(new Vector2(0, force));
            up = true;
        }
    }

    
    
    void Death()
    {
        Destroy(gameObject);
        GC.StartCoroutine("Restart");
        Instantiate(bum, transform.position, Quaternion.identity);
    }

    public bool GetMove()
    {
        return up;
    }

    public void MoveRight()
    {
        move = 1;

    }
    
    public void MoveLeft()
    {
        move = -1;

    }

    public void StopMove()
    {
        move = 0;

    }
    
    private void FixedUpdate()
    {
        transform.position += new Vector3(0.1f * move, 0);
    }
}
