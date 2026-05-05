using UnityEngine;

public class UI_Trigger : MonoBehaviour
{
    public Choicemanager choicemanager; // 객체A를 B이름으로 써먹을거에요

   

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            choicemanager.Openchoice();
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
