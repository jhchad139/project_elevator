using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed ;

    Vector2 btargetvec;



    public void init(Vector2 target)
    {
        btargetvec = target.normalized;
    }
    void Update()
    {
        
        this.transform.Translate(btargetvec * speed * Time.deltaTime);
    }


}
