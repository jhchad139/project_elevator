using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed ;
    public int dmg = 3;

    Vector2 btargetvec;

    SpriteRenderer sprite;

    private TrailRenderer trail; // TrailRenderer 참조 추가

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();
        // 근접태그라면 , 1초뒤 비활성화
    }

    private void OnEnable()
    {
        // 오브젝트 풀에서 꺼내질 때 이전 트레일 궤적을 즉시 지운다.
        if (trail != null)
        {
            trail.Clear();
        }
        if (gameObject.CompareTag("Melee"))
        {
            Invoke("DisableAttack", 0.3f);
        }
    }


    public void init(Vector2 target)
    {
        // 회전 및 위치 초기화 직후 한 번 더 Clear하여 튀는 현상을 완전히 방지한다.
        if (trail != null)
        {
            trail.Clear();
        }

        if (gameObject.CompareTag("Melee"))
        {
            sprite.flipX = true;
        }
        btargetvec = target.normalized;
        float angle = Mathf.Atan2(btargetvec.y, btargetvec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    void Update()
    {
        
        this.transform.Translate(btargetvec * speed * Time.deltaTime, Space.World);
    }

    private void DisableAttack()
    {
        gameObject.SetActive(false);
    }
}
