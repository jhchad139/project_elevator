using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Listmanager : MonoBehaviour
{
    public List<ChoiceSet> ChoiceSetl;

    [System.Serializable]
    public class ChoiceSet // 선택지 총괄
    {
        public ChoiceOption left;
        public ChoiceOption right;
    } // 왼쪽과 오른쪽 버튼을 인스펙터에 보여주기 위함

    [System.Serializable]
    public class ChoiceOption
    {
        public string button_dec;
        public string text;

        public int hpchange;
        public int maxhpchange;
        public int dashcountchange;

        public int monsterNumber; // 풀 번호 입력하면 됨, 지금은 2가 좀비 

        public int ammochange;

        public int triggerChance; // 1~100, 확률임.

        public FailOption Fail; // 실패하면 
        
        [System.Serializable]
        public class FailOption
        {
            public int failMaxHpChange;
            public int failHpChange;
            public int failDashChange;
            public int failMonster;
            public int failAmmo;
        }


    } // 버튼 클릭 시 변화하는 머시깽이들

   

    public ChoiceSet GetRandomChoiceSet() // 관리중인 리스트중 i번째 인덱스 추출
    {
        int i = Random.Range(0, ChoiceSetl.Count);
        return ChoiceSetl[i];
    } 
}
