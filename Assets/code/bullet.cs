using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed ;
    
    

    // Update is called once per frame
    void Update()
    {
        
        this.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


}
