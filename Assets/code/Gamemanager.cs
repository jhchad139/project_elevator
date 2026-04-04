using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    public Player player;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // 체력 설정 해줄거임
    }

    
    void Update()
    {
        //게임타임 설정 해줄거임
    }
}
