using UnityEngine;

public class UI_Trigger : MonoBehaviour
{
    //가까이 오면 판넬 띄우는 코드
    public Choicemanager choicemanager; // 객체A를 B이름으로 써먹을거에요
    public GameObject interact;
    public bool first = false;


    InteractObject interactTarget;


    private void Awake()
    {
        interactTarget = GetComponent<InteractObject>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (choicemanager)
        {
            if (other.CompareTag("Player"))
            {
                if (Gamemanager.Instance.floor.isTopfloor == true)
                    return;
                if (Gamemanager.Instance.choice.can_choice == false)
                    return;
                if (first == false)
                {
                    choicemanager.Openchoice();
                    first = true;
                }
                else if (first == true)

                    choicemanager.Onchoice();
            }
        }
        else if (interact)
        {
            if(other.CompareTag("Player"))
            {
                interact.SetActive(true);
                Gamemanager.Instance.interact.SetTarget(interactTarget);
            }
                
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (interact != null && interact.activeSelf)
            {
                interact.SetActive(false);
                Gamemanager.Instance.interact.ClearTarget();
            }
            else
                choicemanager.Offchoice();
        }
        

    }

}
