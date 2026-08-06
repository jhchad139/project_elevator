using System.Collections;
using UnityEngine;

public class fallingwheel : MonoBehaviour
{

    Vector3 target;

    GameObject shadow;
    Animator anima;

    const int inPoolShadow = 3;
    const int inPoolWheel = 2;

    float fallSpeed = 70f;
    bool isFalling = false;
    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFalling)
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y <= shadow.transform.position.y) {


            transform.position = shadow.transform.position;
            isFalling = false;
        }
    }

    
    private void OnEnable()
    {
        anima.Rebind();
        anima.Update(0f);
        //¹ÙÄû ¾Ö´Ï ÃÊ±âÈ­À×
    }
    public void Init(Vector3 targetPos)
    {
        target = targetPos +  Vector3.down * 0.8f;

        shadow = Gamemanager.Instance.pool.boss.Get(3);

        shadow.transform.position = target;

        transform.position = target + Vector3.up * 56f;

        StartCoroutine(FallingWheel());
    }

    IEnumerator FallingWheel()
    {
        float fallingTiming = 56f / fallSpeed;
        isFalling = true;
        
        yield return new WaitForSeconds(fallingTiming*0.85f);
        anima.SetTrigger("fall");
        yield return new WaitForSeconds(1f);
        shadow.SetActive(false);
        gameObject.SetActive(false);
    }
    
}
