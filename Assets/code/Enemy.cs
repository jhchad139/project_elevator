using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{

    public float speed;
    public Rigidbody2D etarget;
    public int hp;
    public int maxHp = 10;

    [Header("Attack")]
    public float attackRangeX = 1f;
    public float attackRangeY = 1.5f;
    public float attackCooldown = 1f;

    bool is_attack = false;
    float attackTimer = 0f;

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
        is_dead = false;
    }
    void FixedUpdate()
    {
        if (is_dead)
            return;

        if (is_knockback)
            return;

        if (is_attack)
            return;


        /*
        float distance = Vector2.Distance(rigid.position,etarget.position);
        if  (distance <= attackRange)
        {
            Attack();
            return;
        }
        *///위에서 공격하는것도 고려했음
        Vector2 diff = etarget.position - rigid.position;

        if (Mathf.Abs(diff.x) < attackRangeX &&
            Mathf.Abs(diff.y) < attackRangeY)
        {
            Attack();
        }
        //공격

        Vector2 dirvec = etarget.position - rigid.position; // 방향
        Vector2 nextvec = dirvec.normalized * speed * Time.deltaTime; // 이동


        rigid.MovePosition(rigid.position + nextvec);
        rigid.linearVelocity = Vector2.zero; // 관성 0

        if (dirvec.x < 0)
       {
            sprite.flipX = true;
        }
        else
            sprite.flipX = false;
        
    }

    public void Attack()
    {
        is_attack = true;

        attackTimer = attackCooldown;

        Vector2 dir = (etarget.position - rigid.position).normalized; // 방향잡고

        GameObject bite = Gamemanager.Instance.pool.Get(1); // 이펙트 생성

        bite.transform.position = rigid.position + dir * 0.7f; // 플레이어 방향으로 소환

        anima.speed = 0;

        rigid.constraints = RigidbodyConstraints2D.FreezePosition |
                        RigidbodyConstraints2D.FreezeRotation; // 움직이지 않게 얼리는것!

    }
    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                is_attack = false;
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                anima.speed = 1;
            }
        }
    }


 
    //충돌
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!collision.CompareTag("Bullet"))
            return;
        damage(1);
        anima.SetTrigger("hit");

        // transform은 벡터 3이고, rigid는 벡터2라서 형맞춤
        Vector2 bulletDir =rigid.position - (Vector2)collision.transform.position;
        if (!is_dead)
        {
            Debug.Log("hit");
            is_knockback = true;
            StartCoroutine(Knockback(bulletDir));
        }

        collision.gameObject.SetActive(false); // 총알 사라짐
    }

    IEnumerator Knockback(Vector2 dir)
    {
        
        yield return new WaitForFixedUpdate(); //yield는 객체를 넘겨 타이밍을 재는것. wait~얘는 객체임

     
        rigid.AddForce(dir.normalized * 3 , ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        is_knockback = false;
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
        is_dead = true;
        Gamemanager.Instance.player.target = null;
        gameObject.SetActive(false);
        
        
    }
}
    
