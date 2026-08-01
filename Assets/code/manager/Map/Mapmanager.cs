using UnityEngine;

public class Mapmanager : MonoBehaviour
{

    public GameObject curruntMap;

    public GameObject elevatorWall;

    public Animator elevator;
    private void Start()
    {
        if (Gamemanager.Instance != null)
        {
            Gamemanager.Instance.map = this;
        }
    }

    public void LoadMap(int n)
    {
        DisableMap();
        curruntMap = Gamemanager.Instance.mappool.Get(n);
    }

    void DisableMap()
    {
        if (curruntMap == null)
            return;
        curruntMap.SetActive(false);
        curruntMap = null;
    }

    public void OpenElevator()
    {
         elevator.SetBool("isOpen", true);
         Invoke("OpenWall", 0.5f);
    }

    void OpenWall()
    {
        elevatorWall.SetActive(false);
    }

    public void CloseElevator()
    {
        elevatorWall.SetActive(true);
        elevator.SetBool("isOpen", false);
    }
}
