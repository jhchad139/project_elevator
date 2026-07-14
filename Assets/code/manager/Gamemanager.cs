using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    public Player player;
    public player_status status;
    public Poolmanager pool;
    public Choicemanager choice;
    public Floormanager floor;
    public Listmanager list;
    public Enemymanager monster;
    public Mapmanager map;
    public MapPoolmanager mappool;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
