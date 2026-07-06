using System.Collections;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{   //player 기능 구현

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float speed;
    

    //임시적으로 총알 프리팹을 넣어줄 변수
    public Transform Bulletsp;
    public GameObject Bulletprefab;

    //타겟 설정
    public Enemy target;
    Vector2 targetdir;

    Rigidbody2D rigid;
    Animator anima;
    SpriteRenderer sprite;
    Vector2 inputvec;
    player_status status;




    // 대시 관련 변수
    Vector2 dashvec;
    float dashSpeed = 14; // 대시 속도
    double dashTimer = 0;
    double cooldownTimer = 0;
    double dashing = 0.2; // 대시 지속 시간
    double dashCooldown = 1; // 대시 쿨다운 시간

    bool isDashing = false;
    bool isCooldown = false;
    bool dashtimeronoff = false;
    bool cooldowntimeronoff = false;

    // 발사 쿨타임 관련 변수
    float firecooldownTimer = 0;
    double fireCooldown = 0.4f;

    bool is_firecool = false; //지금 쿨타임이니?
    bool firetimeronoff = false; // 타이머 돌고있니?
    public bool is_reroading = false;

    


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        status = GetComponent<player_status>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isDashing == false)
        {
            Vector2 movevec = inputvec.normalized * speed * Time.deltaTime;

            rigid.MovePosition(rigid.position + movevec);
        }
        else
        {
            Vector2 dashmovevec = dashvec * dashSpeed * Time.deltaTime;
            rigid.MovePosition(rigid.position + dashmovevec);
        }

    }

    void Update()
    {

        //대쉬
        if (dashtimeronoff == true)
            dashTimer += Time.deltaTime;
        if (dashTimer >= dashing)
        {
            isDashing = false;
            dashtimeronoff = false;
            dashTimer = 0;

        }

        if (cooldowntimeronoff == true)
             cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= dashCooldown)
        {
            isCooldown = false;
            cooldowntimeronoff = false;
            cooldownTimer = 0;
        }

        //발사
        if (firetimeronoff == true)
        {
            firecooldownTimer += Time.deltaTime;
        }
        if (firecooldownTimer >= fireCooldown)
        {
            firecooldownTimer = 0;
            firetimeronoff = false;
            is_firecool = false;
        }

        
        //타겟
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            
            TargetSet();
        }
    }

    void TargetSet()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //클릭 위치가 실제 터치보다 위에서 판정되어 임시 보정값 적용
        touchPos += Vector2.down * 1.5f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(touchPos,0.3f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                target = hit.GetComponent<Enemy>();
                break;
            }
        }
        
        
    }
    void OnMove(InputValue value)

    {
      inputvec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (inputvec.x != 0 )
        {
            sprite.flipX = inputvec.x < 0 ? true : false;
        }


        anima.SetFloat("speed", inputvec.magnitude); // inputvec을 감지해서 넣음, speed는 0.01이상이면 작동하니까    
    }

    
    public void Dash()
    {
        dashvec = inputvec.normalized;
        if (isCooldown == true || dashvec.magnitude == 0)
            return;

        if (status.UseDash() == false)
            return;

        isDashing = true;
        isCooldown = true;
        dashtimeronoff = true;
        cooldowntimeronoff = true;
        return;
    }
    public void Fire()
    {
        if (is_firecool == true || is_reroading == true)
            return;
        if (status.ammo <=0)
        {
            if (status.bullet_count > 0)
            {
                //대충 재장전 로직
                is_reroading = true;
                Invoke("Reloading", 2f);
                return;
            }

            else
                return;
        }

        CameraShake.Instance.Shake(0);
        is_firecool = true;
        firetimeronoff = true;

        GameObject bullet = Gamemanager.Instance.pool.Get(0);

        bullet.transform.position = Bulletsp.position;

        Vector2 dir = Vector2.right;

        if (target != null)
        {
            Vector2 bulletdir = target.transform.position - Bulletsp.position;
            dir = bulletdir.normalized;
        }

        bullet.GetComponent<bullet>().init(dir);
        
        status.ammo--;
        
    }

    public void Reloading()
    {
        int need = status.max_ammo - status.ammo;
        int load = Mathf.Min(need, status.bullet_count);

        status.ammo += load;
        status.bullet_count -= load;
        is_reroading = false;
    }

    public void HitEffect()
    {
        CameraShake.Instance.Shake(1);
        StartCoroutine(HitSprite());
        StartCoroutine(HitStop());
    }
    IEnumerator HitSprite()
    { 
        sprite.color = new Color(1f, 0.6f, 0.6f, 1f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    IEnumerator HitStop()
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }
}
