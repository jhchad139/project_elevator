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



    public void Shake()
    {
        Debug.Log("Shake");
        if (player.target)
        {
            Vector2 dir = player.target.transform.position - player.transform.position;
            Vector2 cdir = dir.normalized;
            impulse.GenerateImpulse(cdir * 0.2f);
        }
        else impulse.GenerateImpulse(Vector2.right * 0.2f);
    }

    }


