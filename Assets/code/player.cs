using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public int dashCount;

    Rigidbody2D rigid;
    Animator anima;
    SpriteRenderer sprite;
    Vector2 inputvec;


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
    // 대시 관련 변수
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
        if (dashCount <= 0 || isCooldown == true)
            return;

        

        
        isDashing = true;
        isCooldown = true;
        dashtimeronoff = true;
        cooldowntimeronoff = true;
        dashCount--;

        // 타이머 구현


        

        

        return;
    }
}
