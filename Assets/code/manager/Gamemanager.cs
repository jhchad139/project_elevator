using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    public Player player;
    public player_status status;
    public Poolmanager pool;
    public MapPoolmanager mappool;
    public Choicemanager choice;
    public Floormanager floor;
    public Listmanager list;
    public Enemymanager monster;
    public Mapmanager map;
    public Uimanager ui;


    public int baseMaxHp = 100;
    public int baseMaxDash = 5;
    public int baseBulletCount = 10;

    public int startMaxHp;
    public int startMaxDash;
    public int startBulletCount;


    //테스트 씬이동
    public void Test_elevatorstart()
    {
        SceneManager.LoadScene("Game");
    }

    void Awake()
    {
        ResetStartData();
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
        
        
    }

    void Update()
    {
        //게임타임 설정 해줄거임
    }

    public void ResetStartData()
    {
        startMaxHp = baseMaxHp;
        startMaxDash = baseMaxDash;
        startBulletCount = baseBulletCount;
    }
}
