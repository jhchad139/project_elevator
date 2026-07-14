using UnityEngine;

public class Floormanager : MonoBehaviour
{
    public int currentFloor = 1;
    public int maxFloor;

    public bool isTopfloor = false;

    private void Start()
    {
        if (Gamemanager.Instance != null)
        {
            Gamemanager.Instance.floor = this;
        }
    }

    public void CompleteChoice()
    {
        if (isTopfloor)
            return;

        currentFloor++;

        if (currentFloor >= maxFloor)
        {
            Top_floor();
        }
    }

    public void Top_floor()
    {
        isTopfloor = true;
    }

    public bool IsSpecial()
    {
        if (currentFloor % 3  == 0)
            return true;
        return false;
    }
}
