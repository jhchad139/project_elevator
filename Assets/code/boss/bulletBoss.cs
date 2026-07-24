using UnityEngine;

public class bulletBoss : Bossbase // 보스
{
    private void Start()
    {
       
    }
    void Update()
    {
        
    }

    void SpawnBullet(Vector2 dir, float speed)
    {
        GameObject bulletObj = Gamemanager.Instance.pool.boss.Get(0);

        bulletObj.transform.position = attackPoint.position;

        if (bulletObj.TryGetComponent<bossbullet>(out var bullet)) // 새로 생긴거라는데, 기존에 선언해서 겟컴포넌트보다 짧아졌대! 익숙해지자
        {
            bullet.Init(dir, speed);
        }
    }

    public void CirclePattern()
    {
        for (int angle = 0; angle < 360; angle += 20)
        {
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
            SpawnBullet(dir, 5f);
        }
    }
}
