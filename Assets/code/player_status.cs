using UnityEngine;

public class player_status : MonoBehaviour
{
    //player의 자원을 관리
    // 회복 , 피해, 대쉬, 탄약? 등등
    public int maxHp = 100;
    public int hp;

    public int maxdashCount = 3;
    public int dashCount;
    void Awake()
    {
        hp = maxHp;
        dashCount = maxdashCount;
    }



    public bool UseDash()
    {
        if (dashCount <= 0)
            return false;
        dashCount--;
        return true;
    }
    public void Max_hp(int n)
    {
        maxHp += n;
        
    }
    public void Heal(int n)
    {
        hp += n;

        if ( hp >= maxHp)
            hp = maxHp;
    }

    public void Damage(int n)
    {
        hp -= n;

        if (hp <= 0)
            hp = 0;
        // 뭐 나중에 비활성화 로직을 넣던 하자구용
    }

    
}
