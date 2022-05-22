using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 15F;
    public Rigidbody2D rb;

    // Update is called once per frame
    private void Update()
    {
        rb.velocity = transform.right * speed;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
