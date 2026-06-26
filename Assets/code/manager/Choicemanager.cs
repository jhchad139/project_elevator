using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Listmanager;


public class Choicemanager : MonoBehaviour
{
    void Awake()///////////////////
    {
        Instance = this;
    }
    public static Choicemanager Instance; // 트리거 관리
    public UI_Trigger trigger;
    /////////////////////////////////////////////
    

    public GameObject choicePanel; // 판넬 설정
    public Button leftchoice; //
    public Button rightchoice; // 버튼
    public TMP_Text paneltxt;
    public TMP_Text lefttxt;
    public TMP_Text righttxt; // 버튼의 텍스트
    public TMP_Text hptxt;
    public TMP_Text floortxt;

    public player_status status;

    public bool can_choice = true; // 조건

    ChoiceSet Now_list; // 지금 선택된 리스트 class

    bool is_summon = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       choicePanel.SetActive(false);
        leftchoice.onClick.AddListener(SelectL); // 클릭했을때 기능
        rightchoice.onClick.AddListener(SelectR); // 클릭했을때 기능
       
    }

    private void Update()
    {
        hptxt.text = $"HP : {status.hp} / {status.maxHp}";
        floortxt.text = $"Floor : {Gamemanager.Instance.floor.currentFloor}";


    } // 나중에 옮길거임 확인
    // Update is called once per frame


    void SelectL()
    {
        ActionChoice(Now_list.left);
        
    
    }

    void SelectR()
    {
        ActionChoice(Now_list.right);
        
    }

    public void Openchoice()// 
    {
        if (can_choice != true)
            return;
        if (Gamemanager.Instance.floor.isTopfloor == true)
        {
            return;
        }
         // 조건 만족을 하면 판넬이 꺼진 상태
        choicePanel.SetActive(true);

        Now_list = Gamemanager.Instance.list.GetRandomChoiceSet();
        if (Now_list == null)
            return;

        lefttxt.text = Now_list.left.text;
        righttxt.text = Now_list.right.text;
    }

    public void Offchoice()
    {
        
        choicePanel.SetActive(false);
    }
    public void Onchoice()
    {
        if (can_choice != true)
            return;
        if (Gamemanager.Instance.floor.isTopfloor == true)
        {
            return;
        }
        choicePanel.SetActive(true);
    }

    void Endchoice()
    {
        choicePanel.SetActive(false);
        can_choice = false;

        trigger.first = false;
        Invoke("Enablechoice",1f); //5초 뒤에 ""함수 실행
    }

    void Enablechoice() // 누를수 있는 자격
    {
        can_choice=true;
    }

    void ActionChoice(ChoiceOption option) // left, right의 형식으로 들어오니까
    {
        /*
         리스트 구조
        if ( listmanager에 있는 변수 > 0 )
            heal이던 damage던 수정
         >0이 아니라,, 무기지급 등 0/1 이면 bool변수 t/f 해주면 됨
         */
        if (option == null)
            return;
        if (option.hpchange  != 0)
            status.Hp_change(option.hpchange);

        if (option.maxhpchange != 0)
            status.Max_hpchange(option.maxhpchange);

        if (option.dashcountchange != 0)
            status.dashCount += option.dashcountchange;


        if (option.monsterNumber != 0)
        {
            Gamemanager.Instance.monster.Summon(option.monsterNumber);
            is_summon = true;
        }
    

    Gamemanager.Instance.floor.CompleteChoice();
        Endchoice();
    }
}
