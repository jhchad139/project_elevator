using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Poolmanager : MonoBehaviour
{
    public GameObject[] prefabs; // 프리팹 보관

    List<GameObject>[] pools; // 선언

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // 이후 생성

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>(); // 한칸에 한 풀씩 pools를 채워준다.
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index]) { // pools의 index부터 순찰 돌거임, item은 생성된 게임옵젝 변수(바꿔도됨)
            //비활성화 -> 활성화 후 반환
            if (item.activeSelf == false) {
                select = item;
                select.SetActive(true); // 풀에 있는 객체를 활성화
                break;
            }
        }
            // 반환값이 없을때 복제 ( !select 로도 사용 가능)
         if (select == null) {
                select = Instantiate(prefabs[index],transform);
                pools[index].Add(select); // 없으니 추가함.
         }
        
        return select;  
    }
}
