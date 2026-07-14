using UnityEngine;

public class Enemymanager : MonoBehaviour
{
    public GameObject spawnpoint;

    private void Start()
    {
        if (Gamemanager.Instance != null)
        {
            Gamemanager.Instance.monster = this;
        }
    }

    public void Summon(int n)
    {
            GameObject enemy = Gamemanager.Instance.pool.Get(n);
            enemy.transform.position = spawnpoint.transform.position;
    }
}
