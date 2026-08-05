using UnityEngine;

public class Bossbase : MonoBehaviour
{
    public Transform attackPoint;

    public int hp;
    public int maxhp;

    public int angryHp;
    public bool isAngry = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {

            bullet bullet = collision.GetComponent<bullet>();
           Hit(bullet.dmg);
            //anima.SetTrigger("hit");

            

            collision.gameObject.SetActive(false); // ÃÑ¾Ë »ç¶óÁü
        }
    }

    virtual public void Hit(int n)
    {
        hp -= n;
        if (hp < angryHp)
        {
            isAngry = true;
        }
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }
    virtual public void Die()
    {

    }


}
