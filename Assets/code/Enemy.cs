using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        is_dead = false;
    }
    void FixedUpdate()
    {
        if (!is_knockback)
        {
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
    }

    private void Update()
    {
        
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
    
