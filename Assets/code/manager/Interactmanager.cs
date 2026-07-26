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
                Debug.Log("a");
                Gamemanager.Instance.ui.OnInteractTalk();
                break;
        }
    }

    
}
