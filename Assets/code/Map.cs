using UnityEngine;

public class Map : MonoBehaviour
{

    public int monsterNumber;
    public Transform[] enemySpawnpoints;

    public void OnEnable()
    {
        int n;
        for (int i = 0;i<enemySpawnpoints.Length;i++)
        {
            n = Random.Range(1, 10);
            if (n <= 6)
            {
                Gamemanager.Instance.monster.Summon(monsterNumber, enemySpawnpoints[i]);
            }
        }
    }
}
