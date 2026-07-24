using UnityEngine;

public class bossbullet : MonoBehaviour // º¸½º°¡ ½î´Â Åº¸·
{
    public int damage = 5;
    public float speed;
    Vector2 dir;

    public void Init(Vector2 dir, float speed)
    {
        this.dir = dir.normalized;
        this.speed = speed;
    }

    void Update()
    {
        this.transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        Gamemanager.Instance.status.Hp_change(-damage);
        gameObject.SetActive(false);

    }

}
