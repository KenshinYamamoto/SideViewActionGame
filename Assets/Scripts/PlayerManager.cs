using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;

    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    };

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rigidbody2D;
    Animator animator;
    float speed;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameObserver.gameObserver.GameFinish)
        {
            return;
        }

        float x = Input.GetAxisRaw("Horizontal"); // 方向キーの取得
        animator.SetFloat("Speed", Mathf.Abs(x));

        if (x == 0)
        {
            //止まっている
            direction = DIRECTION_TYPE.STOP;
        }
        else if(x > 0)
        {
            // 右へ
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if(x < 0)
        {
            // 左へ
            direction = DIRECTION_TYPE.LEFT;
        }
        // スペースが押されたらJumpさせる
        if (IsGround())
        {
            animator.SetBool("isJumping", false);
            if (Input.GetKeyDown("space"))
            {
                rigidbody2D.AddForce(Vector2.up * ParamsSO.Entity.playerJumpPower);
            }
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        if (GameObserver.gameObserver.GameFinish)
        {
            return;
        }

        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0f;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = ParamsSO.Entity.playerSpeed;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -ParamsSO.Entity.playerSpeed;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        rigidbody2D.velocity = new Vector2(speed,rigidbody2D.velocity.y);
    }

    void PlayJumpSound()
    {
        // 上方向に力を加える
        rigidbody2D.AddForce(Vector2.up * ParamsSO.Entity.playerJumpPower);
        SoundObserver.soundObserver.PlaySE(SoundObserver.SE.Jump);
    }

    bool IsGround()
    {
        // 始点と終点を作成
        Vector3 leftStartPosition = transform.position - Vector3.right * 0.2f;
        Vector3 rightStartPosition = transform.position + Vector3.right * 0.2f;
        Vector3 endPosition = transform.position - Vector3.up * 0.1f;
        Debug.DrawLine(leftStartPosition, endPosition);
        Debug.DrawLine(rightStartPosition, endPosition);
        return Physics2D.Linecast(leftStartPosition,endPosition,blockLayer) || Physics2D.Linecast(rightStartPosition, endPosition, blockLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObserver.gameObserver.GameFinish)
        {
            return;
        }

        if (collision.CompareTag("Trap"))
        {
            PlayerDeath();
        }
        else if (collision.CompareTag("Finish"))
        {
            GameObserver.gameObserver.GameClear();
        }
        else if (collision.CompareTag("Item"))
        {
            // アイテム取得
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
        else if (collision.CompareTag("Enemy"))
        {
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();

            if (transform.position.y + 0.2f > enemy.transform.position.y)
            {
                // 上から踏んだら
                // ジャンプする
                GameObserver.gameObserver.AddScore(ParamsSO.Entity.enemyDownPoint);
                SoundObserver.soundObserver.PlaySE(SoundObserver.SE.EnemyDown);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                rigidbody2D.AddForce(Vector2.up * ParamsSO.Entity.playerEnemyDownJumpPower);
                // 敵を削除
                enemy.DestroyEnemy();
            }
            else
            {
                // 横からぶつかったら
                PlayerDeath();
            }
        }
    }

    void PlayerDeath()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Destroy(boxCollider2D);
        GameObserver.gameObserver.GameOver();
        rigidbody2D.velocity = new Vector2(0, 0);
        rigidbody2D.AddForce(Vector2.up * ParamsSO.Entity.playerDeadJumpPower);
        animator.SetBool("isJumping", false);
        animator.Play("PlayerDeathAnimation");
    }
}
