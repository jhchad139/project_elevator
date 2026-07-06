using UnityEngine;

public class Bite : MonoBehaviour
{
    public int damage = 10;
    public float damageTime = 0f;
    public float lifeTime = 1f; // 일단 좀비랑 똑같이 맞춰

    bool hit = false; //한번만 뎀지주려고

    void OnEnable()
    {
        damageTime = 0;
        hit = false;
        Invoke(nameof(Hide), lifeTime); //1.5초뒤 사라짐
    }

    void Hide() //사라지는 함수
    {
        gameObject.SetActive(false);
    } 

    void OnDisable()
    {
        CancelInvoke();
    } // 비활성화 될때, invoke를 취소한다

  
    void Update()
    {
            damageTime += Time.deltaTime;  
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (hit)
            return;

        if (!other.CompareTag("Player"))
            return;

        if (damageTime <= 0.3f)
            return;

        player_status status = other.GetComponent<player_status>(); // 충돌한 객체의 컴포넌트 호출

        if (status == null)
            return;

        status.Hp_change(-damage);

        hit = true;
    }
}