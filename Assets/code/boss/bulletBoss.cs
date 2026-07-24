using UnityEngine;
using System.Collections;

public class bulletBoss : Bossbase // 보스
{
    private void Start()
    {
        StartCoroutine(SpinCirclePattern());
    }

    IEnumerator SpinCirclePattern()
    {
        int i = 0;
        while (true)
        {



            Circle(i);

            i += 4;

            yield return new WaitForSeconds(0.3f);
            if (i > 100)
                yield break;
        }
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

    public void Circle(int i = 0)
    {
        for (int angle = i; angle < 360 + i; angle += 30)
        {
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right; // right방향벡터를 angle각도만큼 돌려달라는뜻
            SpawnBullet(dir, 4f);
        }
    }
}
