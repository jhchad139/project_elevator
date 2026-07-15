using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance;

        CinemachineImpulseSource impulse;

    public Player player;
        
    

        void Awake()
        {
            Instance = this;
            impulse = GetComponent<CinemachineImpulseSource>();
        }



    public void Shake(int n,Vector2 target = default)
    {
        Debug.Log("Shake");
        if (n == 0) //0은 플레이어
        {
            /*
            if (target)
            {
                Vector2 dir = player.target.transform.position - player.transform.position;
                Vector2 cdir = dir.normalized;
                impulse.GenerateImpulse(cdir * 0.2f);
            }
            */
            impulse.GenerateImpulse(target * 0.2f);
        }
        if(n == 1) //1은 그냥 흔들기
        {
            impulse.GenerateImpulse();
        }
        if (n == 2) // 엘리베이터
        {
            StartCoroutine(ElevatorShake());
        }
    }

    IEnumerator ElevatorShake()
    {
        
        for(int i = 0;i<12;i++)
        {
            impulse.GenerateImpulse(Vector2.down * 0.02f);
            yield return new WaitForSeconds(0.15f);
        }

        /*
        float power = 0.1f;
        while (power > 0.01f)
        {
            impulse.GenerateImpulse(Vector2.down * power);

            power *= 0.8f;
            yield return new WaitForSeconds(0.18f);
        }*/
    }

}


