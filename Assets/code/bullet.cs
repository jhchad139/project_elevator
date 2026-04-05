using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed ;
    
    

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > 5)
        {
            Destroy(this.gameObject);
        }
        this.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
