using UnityEngine;

public class Mapmanager : MonoBehaviour
{

    public GameObject curruntMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}
