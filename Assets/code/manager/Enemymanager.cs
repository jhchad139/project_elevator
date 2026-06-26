using UnityEngine;

public class Enemymanager : MonoBehaviour
{
    public GameObject spawnpoint;


    public void Summon(int n)
    {
            GameObject enemy = Gamemanager.Instance.pool.Get(n);
            enemy.transform.position = spawnpoint.transform.position;
    }
}
