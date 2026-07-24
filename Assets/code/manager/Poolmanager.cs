using UnityEngine;

public class Poolmanager : MonoBehaviour
{
    private void Start()
    {
        if (Gamemanager.Instance != null)
        {
            Gamemanager.Instance.pool = this;
        }
    }
    public Pool normal;
    public Pool boss;
}
