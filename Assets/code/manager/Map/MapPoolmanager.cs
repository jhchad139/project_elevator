using System.Collections.Generic;
using UnityEngine;

public class MapPoolmanager : MonoBehaviour
{
    public GameObject[] prefabs; // 프리팹 보관

    public Transform mapSpawnpoint;

    
    List<GameObject>[] pools; // 선언

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // 이후 생성

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>(); // 한칸에 한 풀씩 pools를 채워준다.
        }
    }

    private void Start()
    {
        if (Gamemanager.Instance != null)
        {
            Gamemanager.Instance.mappool = this;
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (item.activeSelf == false)
            {
                select = item;
                break;
            }
        } // 대충 돌면서 꺼진거 있으면 그걸로

        if (select == null)
        {
            select = Instantiate(prefabs[index],transform);
            pools[index].Add(select); 
        } //없으면 만들고

        select.transform.position  = mapSpawnpoint.position;
        select.SetActive(true); // 풀에 있는 객체를 활성화
        return select;
    }

    
}
