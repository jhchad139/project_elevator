using UnityEngine;

public class Enemymanager : MonoBehaviour
{
    

    private void Start()
    {
        if (Gamemanager.Instance != null)
        {
            Gamemanager.Instance.monster = this;
        }
    }

    public void Summon(int n,Transform point = default)
    {
            GameObject enemy = Gamemanager.Instance.pool.normal.Get(n);
            enemy.transform.position = point.position;
    }
}
