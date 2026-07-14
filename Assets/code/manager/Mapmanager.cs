using UnityEngine;

public class Mapmanager : MonoBehaviour
{

    public GameObject curruntMap;

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
}
