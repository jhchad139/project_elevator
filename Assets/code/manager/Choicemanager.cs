using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Choicemanager : MonoBehaviour
{
    public GameObject choicePanel; // 판넬 설정
    public Button leftchoice; //
    public Button rightchoice; // 버튼
    public TMP_Text paneltxt;
    public TMP_Text lefttxt;
    public TMP_Text righttxt; // 버튼의 텍스트
    public TMP_Text hptxt;
    public TMP_Text floortxt;

    public player_status status;

    private bool can_choice = true; // 조건

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       choicePanel.SetActive(false);
        leftchoice.onClick.AddListener(SelectL); // 클릭했을때 기능
        rightchoice.onClick.AddListener(SelectR); // 클릭했을때 기능
       
    }

    private void Update()
    {
        hptxt.text = $"HP : {status.hp}";
        floortxt.text = $"Floor : {Gamemanager.Instance.floor.currentFloor}";

    }
    // Update is called once per frame


    void SelectL()
    {
        status.Heal(5);
        Gamemanager.Instance.floor.CompleteChoice();
        Endchoice();
    
    }

    void SelectR()
    {
        status.Damage(5);
        Gamemanager.Instance.floor.CompleteChoice();
        Endchoice();
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

       
        lefttxt.text = "hp + 5";
        righttxt.text = "hp - 5";
    }

    public void Offchoice()
    {
        
        choicePanel.SetActive(false);
    }

    void Endchoice()
    {
        choicePanel.SetActive(false);
        can_choice = false;

        Invoke("Enablechoice",1f); //5초 뒤에 ""함수 실행
    }

    void Enablechoice() // 누를수 있는 자격
    {
        can_choice=true;
    }
}
