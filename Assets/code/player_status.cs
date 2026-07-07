using UnityEngine;

public class player_status : MonoBehaviour
{
    //player의 자원을 관리
    // 회복 , 피해, 대쉬, 탄약? 등등
    public int maxHp = 100;
    public int hp;

    public bool isDead = false;
    public int maxdashCount = 3;
    public int dashCount;

    public int ammo = 0;
    public int max_ammo = 6;

    public int bullet_count = 0;

    
    void Awake()
    {
        hp = maxHp;
        dashCount = maxdashCount;
        isDead = false;
    }

    private void Update()
    {
        if (hp > maxHp)
            hp = maxHp;
        if (dashCount > maxdashCount)
            dashCount = maxdashCount;
        if (dashCount < 0)
            dashCount = 0;
    }

    public bool UseDash()
    {
        if (dashCount <= 0)
            return false;
        dashCount--;
        return true;
    }
    public void Max_hpchange(int n)
    {
        maxHp += n;
        
    }
    public void Hp_change(int n)
    {
        hp += n;

        if ( hp >= maxHp)
            hp = maxHp;

        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            Gamemanager.Instance.player.PlayerDead();
        }

        if (n < 0)
        {
            Gamemanager.Instance.player.HitEffect();
        }
        // 뭐 나중에 비활성화 로직을 넣던 하자구용
    }

    

    
}
