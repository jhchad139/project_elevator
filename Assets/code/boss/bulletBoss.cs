using System.Collections;
using UnityEngine;

public class bulletBoss : Bossbase // 보스
{
    public bool isOrbit = false;

    public int phase  = 0;

    float patternTimer = 0;
    float patternTerm = 8f;

    float fallingTimer = 0f;
    float fallintTerm = 10f;
    private void Awake()
    {
        Gamemanager.Instance.ui.OnBossCanvas();
         Gamemanager.Instance.ui.targetBoss = this;

    }
    private void Start()
    {
        //StartCoroutine(SpinCirclePattern());
        //Invoke("ExplodePattern",1f);
        //StartCoroutine(OrbitPattern());

    }
    IEnumerator SpinCirclePattern()
    {

        int i = 0;
        while (true)
        {
            Debug.Log(i);
            Circle(0,i);
            i += 4;
            yield return new WaitForSeconds(0.25f);
            if (i > 100)
                yield break;
        }
    }

    

    private void Update()
    {
        //나중에, isAngry감지해서 패턴텀을 줄이는 식으로 속도 조절 ㄱㄱ
        patternTimer += Time.deltaTime;
        fallingTimer += Time.deltaTime;

        if (patternTimer >= patternTerm)
        {
            UsePattern();
            patternTimer = 0;
        }

        if (fallingTimer >= fallintTerm)
        {
            StartCoroutine(FallingWheel());
            fallingTimer = 0;
        }


    }

    void UsePattern()
    {
        //랜덤으로 숫자 하나 골라
        int n = Random.Range(1,4+phase);

        switch (n)
        {
            case 1: //패턴1
                StartCoroutine(SpinCirclePattern());
                Debug.Log("1");
                break;

            case 2: //패턴2
                Invoke("ExplodePattern", 1f);
                Debug.Log("2");
                break;

            case 3: //패턴3

                StartCoroutine(OrbitPattern());
                Debug.Log("3");
                break;

        }

    }

    IEnumerator FallingWheel()
    {
        yield return new WaitForSeconds(1f);
        GameObject obj = Gamemanager.Instance.pool.boss.Get(2);

        obj.GetComponent<fallingwheel>()
           .Init(Gamemanager.Instance.player.transform.position);

    }
    //void DestroyGun() {}
    //void +
    //void X

    IEnumerator OrbitPattern()
    {
        isOrbit = true;
        int i = 0;
        while (true)
        {
            Circle(0,0,5f);
            yield return new WaitForSeconds(0.3f);
            Circle(4,0,5f);
            i++ ;
            yield return new WaitForSeconds(0.3f);
            if (i > 10)
            {
                isOrbit = false;
                yield break;
            }
        }
        
    }
    
    void ExplodePattern()
    {
        Vector2 dir = Gamemanager.Instance.player.transform.position- gameObject.transform.position ;
        SpawnBullet(dir.normalized, 30f, 1);
    }

    void SpawnBullet(Vector2 dir, float speed , int i = 0, Transform point = null)
    {
        if (point == null)
        {
            point = attackPoint;
        }
        GameObject bulletObj = Gamemanager.Instance.pool.boss.Get(i);

        bulletObj.transform.position = point.position;

        if (bulletObj.TryGetComponent<bossbullet>(out var bullet)) // 새로 생긴거라는데, 기존에 선언해서 겟컴포넌트보다 짧아졌대! 익숙해지자
        {
            bullet.boss = this;
            bullet.Init(dir, speed);
        }
    }

    public void Circle(int value = 0,int i = 0,float speed = 6f, Transform point = null)
    {
        if (point == null)
        {
            point = attackPoint;
        }
        for (int angle = i; angle < 360 + i; angle += 20)
        {
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right; // right방향벡터를 angle각도만큼 돌려달라는뜻
            SpawnBullet(dir, speed,value,point);
        }
    }
}
/*
 지금 있는 탄막 - 
circle - 십자가, 원형 등으로 변형시켜 사용할수 있음.
orbit - 공전, 퍼지는 탄이 빙글빙글
explode - 큰거 한방 빵 ,  벽에 닿으면 잔탄 퍼짐

생각중인 패턴
십자가(십자가로 쭉 할지, 회전을 넣을지, 십자가랑 X를 동시에 쏘게 할지)
공전 - 빙글빙글 도는걸 좀 촘촘하게, 간격을 다르게
레이저를 쏘면서 , 돌리면서, explode를 쏴 ? 일단 레이저 나중에...?

circle 응용 3개
orbit 응용 3개
explode 는 모든 패턴? 에 섞고



추가 할 패턴 
관람차 낙하패턴
기관총 파괴 패턴 

시작은 일반. 

hp가 75%이하로 떨어지면 
관람차 낙하패턴 추가, 쿨타임 15초

hp가 25% 이하로 떨어지면
관람차 낙하패턴 쿨타임 8초

기본 패턴





hp가 50% 이하로 떨어지면 광폭화
 */