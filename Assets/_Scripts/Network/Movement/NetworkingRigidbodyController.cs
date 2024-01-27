using Fusion;
using UnityEngine;

public class NetworkingRigidbodyController : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 direction)
    {
        Vector3 dir = new Vector3(direction.x + rb.velocity.x, direction.y + rb.velocity.y, direction.z + rb.velocity.z);

        rb.velocity = dir * speed * Time.fixedDeltaTime;
    }
}
