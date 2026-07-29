using UnityEngine;

public class bossbullet : MonoBehaviour // º¸½º°¡ ½î´Â Åº¸·
{
    public int damage = 5;
    public float speed;
    public bool isExplode;

    public bulletBoss boss;
    Vector2 dir;

    public void Init(Vector2 dir, float speed)
    {
       
        this.dir = dir.normalized;
        this.speed = speed;
    }

    void Update()
    {
        this.transform.Translate(dir * speed * Time.deltaTime, Space.World);
        
        if (boss.isOrbit)
        {
            Debug.Log("a");
            float baseAngle = gameObject.CompareTag("Reverse") ? -15f : 15f;

            float rotateAngle = baseAngle * Time.deltaTime;
            dir = Quaternion.Euler(0, 0, rotateAngle) * dir;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExplode)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                boss.Circle(0, 0, 3f, gameObject.transform);
                boss.Circle(0, 10, 3f, gameObject.transform);
                gameObject.SetActive(false);
            }
        }

        if (!collision.CompareTag("Player"))
            return;
        Gamemanager.Instance.status.Hp_change(-damage);
        gameObject.SetActive(false);

        

    }

   
}
