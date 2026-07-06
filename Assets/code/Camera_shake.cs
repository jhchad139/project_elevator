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



    public void Shake(int n)
    {
        Debug.Log("Shake");
        if (n == 0) //0은 플레이어
        {
            if (player.target)
            {
                Vector2 dir = player.target.transform.position - player.transform.position;
                Vector2 cdir = dir.normalized;
                impulse.GenerateImpulse(cdir * 0.2f);
            }
            else impulse.GenerateImpulse(Vector2.right * 0.2f);
        }
        if(n == 1) //1은 그냥 흔들기
        {
            impulse.GenerateImpulse();
        }
    }

}


