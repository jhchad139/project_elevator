using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public Rigidbody2D etarget;
    public int hp;
    public int maxHp = 10;


    public bool is_dead = false;

    public bool is_knockback = false;

    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer sprite;
    Animator anima;
    void Awake()
    {
        rigid =  GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
        hp = maxHp;
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

    private void Update()
    {
        
    }

    //충돌
    void OnTriggerEnter2D(Collider2D collision)
    {
        is_knockback = true;
        if (!collision.CompareTag("Bullet"))
            return;
        damage(1);
        anima.SetTrigger("hit");
        collision.gameObject.SetActive(false); // 총알 사라짐
    }

    void damage(int n)
    {
        hp -= n;

        if (hp <= 0) {  
            hp = 0;
            Die();
        }
            
    }

    void Die()
    {
        gameObject.SetActive(false);
        Gamemanager.Instance.player.target = null;
    }
}
    
