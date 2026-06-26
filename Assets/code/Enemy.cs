using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public Rigidbody2D etarget;
    public int hp;
    public int maxHp = 100;

    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer sprite;
    void Awake()
    {
        rigid =  GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        etarget = Gamemanager.Instance.player.GetComponent<Rigidbody2D>();
        rigid.transform.position = Gamemanager.Instance.monster.spawnpoint.transform.position;
        hp = maxHp;
    }
    void FixedUpdate()
    {
        Vector2 dirvec = etarget.position - rigid.position; // 방향
        Vector2 nextvec = dirvec.normalized * speed * Time.deltaTime; // 이동

        rigid.MovePosition(rigid.position +nextvec);
        rigid.linearVelocity = Vector2.zero; // 관성 0

        if (dirvec.x < 0)
        {
            sprite.flipX = true;
        }
        else
            sprite.flipX = false;
    }
}
