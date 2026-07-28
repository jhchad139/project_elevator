using UnityEngine;
public class Interactmanager : MonoBehaviour
{
    public InteractObject currntTarget;

    private void Awake()
    {
        if (Gamemanager.Instance != null)
        {
            Gamemanager.Instance.interact = this;
        }
    }
    public void SetTarget(InteractObject Target)
    {
        currntTarget = Target;
    }
    public void ClearTarget()
    {
        currntTarget = null;
    }

    public void Interact()
    {
        if (currntTarget == null)
            return;
        switch(currntTarget.type)
        {
            case InteractType.Item:
                Debug.Log("item");
                Gamemanager.Instance.ui.OnInteractTalk();
                break;

            case InteractType.Npc:
                Debug.Log("npc");
                Gamemanager.Instance.ui.OnInteractTalk();
                break;

            case InteractType.Action:
                Debug.Log("action");
                Gamemanager.Instance.ui.OnInteractTalk();
                break;

            case InteractType.Hp:
                Debug.Log("Hp");
                Item_Heal(currntTarget.itemvalue);
                currntTarget.gameObject.SetActive(false);
                break;
            
        }
    }

    void Item_Heal(int value)
    {
        Gamemanager.Instance.status.Hp_change(value);
    }

    
}
