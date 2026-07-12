using System.Collections;
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


    public player_status status;

    public bool can_choice = true; // 조건

    ChoiceSet Now_list; // 지금 선택된 리스트 class

    //bool is_summon = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       choicePanel.SetActive(false);
        leftchoice.onClick.AddListener(SelectL); // 클릭했을때 기능
        rightchoice.onClick.AddListener(SelectR); // 클릭했을때 기능
       
    }

  // 나중에 옮길거임 확인
    // Update is called once per frame


    void SelectL()
    {
        ActionChoice(Now_list.left);
    }

    void SelectR()
    {
        ActionChoice(Now_list.right);
    }


    Coroutine typingCrt; // 코루틴의 주소록, 실행중인 코루틴을 멈추려면, 주소가 필요함. 
    //주소 없이 그냥 스탑코루틴 해버리면, 새로운 인자가 생성되어 오류가 난다고 함.

    public void Openchoice()// 여기서 텍스트를 받기 때문에 , 여기서 출력 연출 하겠음.
    {
        if (can_choice != true)
            return;
        if (Gamemanager.Instance.floor.isTopfloor == true)
        {
            return;
        }
         // 조건 만족을 하면 판넬이 꺼진 상태
        choicePanel.SetActive(true);

        /*
        Now_list = Gamemanager.Instance.list.GetNormalChoiceSet();
        if (Now_list == null)
            return;
        */

        if (Gamemanager.Instance.floor.IsSpecial())
            Now_list = Gamemanager.Instance.list.GetSpecialChoiceSet();
        else
            Now_list = Gamemanager.Instance.list.GetNormalChoiceSet();

        if (Now_list == null)
            return;

        lefttxt.text = Now_list.left.text;
        righttxt.text = Now_list.right.text;
        
    }

    /// <summary>
    /// choice ui
    /// </summary>
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

    /// <summary>
    /// 선택지 적용
    /// </summary>
    void ApplyOption(int hp, int maxHp, int dash, int monster, int ammo)
    {
        if (hp != 0)
            status.Hp_change(hp);

        if (maxHp != 0)
            status.Max_hpchange(maxHp);

        if (dash != 0)
            status.dashCount += dash;

        if (monster != 0)
            Gamemanager.Instance.monster.Summon(monster);

        if (ammo != 0)
            status.bullet_count += ammo;
    } // 새로운 변수 n이 들어온다면, 같은 방식으로

    void ApplyMap(int map)
    {
       Gamemanager.Instance.map.LoadMap(map);
    }
    void ActionChoice(ChoiceOption option) // left, right의 형식으로 들어오니까
    {
        
        if (option == null)
            return;

        int triggerRand = Random.Range(1, 100); // 확률비교변수

        if (option.triggerChance == 0 || option.triggerChance >= triggerRand)
        {
            //성공
            ApplyOption(
            option.hpchange,
            option.maxhpchange,
            option.dashcountchange,
            option.monsterNumber,
            option.ammochange
            );
            ApplyMap(option.mapNumber);
        }
        else if (option.Fail != null)
        {
            //실패
            ApplyOption(
            option.Fail.failHpChange,
            option.Fail.failMaxHpChange,
            option.Fail.failDashChange,
            option.Fail.failMonster,
            option.Fail.failAmmo
            );
        }


        Gamemanager.Instance.floor.CompleteChoice();
        Endchoice();
    }
    /*
         함수 구조
        어플라이옵션 함수에, 선택지의 변수값을 받아서 넘기고, 
        어플라이옵션 함수 내부에서, 0이아니면 그 변수에 맞는 함수를 작동시키는 구조임
     */
}
