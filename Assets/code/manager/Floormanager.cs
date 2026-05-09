using UnityEngine;

public class Floormanager : MonoBehaviour
{
    public int currentFloor = 1;
    public int maxFloor = 5;

    public bool isTopfloor = false;

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
}
