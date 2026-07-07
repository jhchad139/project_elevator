using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed ;

    Vector2 btargetvec;



    public void init(Vector2 target)
    {
        btargetvec = target.normalized;
        float angle = Mathf.Atan2(btargetvec.y, btargetvec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    void Update()
    {
        
        this.transform.Translate(btargetvec * speed * Time.deltaTime, Space.World);
    }


}
