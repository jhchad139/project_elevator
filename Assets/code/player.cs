using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;

    Rigidbody2D rigid;
    Animator anima;
    SpriteRenderer sprite;
    Vector2 inputvec;
   
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movevec = inputvec.normalized * speed * Time.deltaTime;

        rigid.MovePosition(rigid.position + movevec);

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
}
