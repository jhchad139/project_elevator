using UnityEngine;

public class Area_Trigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        switch (transform.tag)
        {
            case "Bullet":
                Rigidbody2D rigid;
                rigid = GetComponent<Rigidbody2D>();
                rigid.linearVelocity = Vector3.zero;
                gameObject.SetActive(false);
                return;
        }
    }
}
