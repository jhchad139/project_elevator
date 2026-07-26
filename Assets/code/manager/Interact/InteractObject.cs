using UnityEngine;

public class InteractObject : MonoBehaviour
{
        
        public InteractType type;

        public string[] talks;

        public int itemId;

        public bool once;
        public bool interacted;
    
}
public enum InteractType
{
    Npc, Item, Action
}
