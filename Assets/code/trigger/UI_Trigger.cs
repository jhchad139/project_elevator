using UnityEngine;

public class UI_Trigger : MonoBehaviour
{
    public Choicemanager choicemanager; // 객체A를 B이름으로 써먹을거에요
    public bool first = false;
   

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Gamemanager.Instance.floor.isTopfloor == true)
                return;
            if (Gamemanager.Instance.choice.can_choice == false)
                return;
            if (first == false) { 
                choicemanager.Openchoice();
                first = true;
             }
            else if (first ==true)
                
                choicemanager.Onchoice();
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            choicemanager.Offchoice();
        }
    }

}
