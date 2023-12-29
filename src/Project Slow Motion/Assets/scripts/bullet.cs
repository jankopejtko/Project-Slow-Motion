using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] GameObject self;
    Rigidbody rb;
    [SerializeField] int TTL;
    public float force;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "gun") 
        {
        }
        else 
        {
            rb.useGravity = true;
        }
    }
    private void Update()
    {
        Destroy(self, TTL);
    }
}
