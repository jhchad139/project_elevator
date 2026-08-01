using UnityEngine;

public class InteractObject : MonoBehaviour
{
        
        public InteractType type;

        public string[] talks;

        public int itemId;

        public int itemvalue;

       
    
}
public enum InteractType
{
    Npc, Item, Action , Hp , poop
}
