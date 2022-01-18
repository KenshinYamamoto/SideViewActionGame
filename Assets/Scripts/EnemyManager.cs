using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    };

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rigidbody2D;
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameObject deathEffect;
    float speed;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        // ‰E‚Ö
        direction = DIRECTION_TYPE.RIGHT;
    }

    private void Update()
    {
        if (!isGround())
        {
            // •ûŒü‚ð•Ï‚¦‚é
            ChangeDirection();
        }
    }

    bool isGround()
    {
        Vector3 startPosition = transform.position + transform.right * 0.5f * transform.localScale.x;
        Vector3 endPosition = startPosition - transform.up * 0.5f;
        Debug.DrawLine(startPosition, endPosition);
        return Physics2D.Linecast(startPosition,endPosition,blockLayer);
    }

    void ChangeDirection()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.RIGHT:
                direction = DIRECTION_TYPE.LEFT;
                break;
            case DIRECTION_TYPE.LEFT:
                direction = DIRECTION_TYPE.RIGHT;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0f;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 3f;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3f;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    public void DestroyEnemy()
    {
        Instantiate(deathEffect,transform.position,transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ChangeDirection();
        }
    }
}
