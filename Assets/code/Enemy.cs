using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{

    public float speed = 1;
    public Rigidbody2D etarget;
    public int hp;
    public int maxHp = 10;

    [Header("Attack")]
    public float attackRangeX = 1f;
    public float attackRangeY = 1.5f;
    public float attackTime = 0.6f;
    public float attTimer = 0;

    public bool is_attack = false;
    public bool can_attack = true;
    public float attackCooldown=3f;

    public bool is_dead = false;

    public float range = 6f;

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
        etarget = null;
        //rigid.transform.position = Gamemanager.Instance.monster.spawnpoint.transform.position;
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
        {
            rigid.linearVelocity = Vector2.zero;
            anima.speed = 0;
            return;
        }

        //위에서 공격하는것도 고려했음
        Vector2 diff = etarget.position - rigid.position;

        if (Mathf.Abs(diff.x) < attackRangeX &&
            Mathf.Abs(diff.y) < attackRangeY)
        {
            if (!Gamemanager.Instance.status.isDead)
            Attack();
        }




        //움직임
        Vector2 dirvec = etarget.position - rigid.position; // 방향
        Vector2 nextvec = dirvec.normalized * speed * Time.deltaTime; // 이동

        rigid.MovePosition(rigid.position + nextvec);
        rigid.linearVelocity = Vector2.zero; // 관성 0
        anima.speed = 1;

        if (dirvec.x < 0)
       {
            sprite.flipX = true;
        }
        else
            sprite.flipX = false;

        

    }

    public void Attack()
    {
        if (is_attack) return;
        if (!can_attack) return;
        is_attack = true;
        can_attack = false;

        Vector2 dir = (etarget.position - rigid.position).normalized; // 방향잡고

        GameObject bite = Gamemanager.Instance.pool.normal.Get(1); // 이펙트 생성

        bite.transform.position = rigid.position + dir * 0.7f; // 플레이어 방향으로 소환

       


    }
    private void Update()
    {
        //타겟 
        if (etarget == null)
        {
            float dist = Vector2.Distance(rigid.position,
                Gamemanager.Instance.player.transform.position);

            if (dist <= range)
            {
                etarget = Gamemanager.Instance.player.GetComponent<Rigidbody2D>();
            }

            return;
        }

        if (!can_attack)
        {
            attTimer += Time.deltaTime;

            if (attTimer > attackTime)
            {
                is_attack = false;
            }
            if (attTimer > attackCooldown)
            {
                can_attack = true;
                attTimer = 0;
            }
        }
    }



    //충돌
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {

            bullet bullet = collision.GetComponent<bullet>();
            damage(bullet.dmg);
            anima.SetTrigger("hit");

            // transform은 벡터 3이고, rigid는 벡터2라서 형맞춤
            Vector2 bulletDir = rigid.position - (Vector2)collision.transform.position;
            if (!is_dead)
            {
                Debug.Log("hit");
                is_knockback = true;
                StartCoroutine(Knockback(bulletDir));
            }

            collision.gameObject.SetActive(false); // 총알 사라짐
        }
        else if (collision.CompareTag("Melee"))
        {
            bullet bullet = collision.GetComponent<bullet>();
            damage(bullet.dmg);
            anima.SetTrigger("hit");
            Vector2 bulletDir = rigid.position - (Vector2)collision.transform.position;
            if (!is_dead)
            {
                Debug.Log("hit");
                is_knockback = true;
                StartCoroutine(Knockback(bulletDir));
            }
        }
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
        //Gamemanager.Instance.player.target = null;
        gameObject.SetActive(false);
        
        
    }
}
    
