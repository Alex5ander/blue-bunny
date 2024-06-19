using UnityEngine;

public class Insect : MonoBehaviour
{
    [SerializeField] float Speed = 0;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float Range;
    Vector2 center;
    Rigidbody2D body;
    BoxCollider2D boxCollider2D;
    bool dead = false;
    float angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Vector2 next = center + new Vector2(Mathf.Cos(angle) * Range, 0);
            float direction = next.x - transform.position.x;
            transform.eulerAngles = new Vector3(0, direction > 0 ? 0 : 180, 0);
            transform.position = next;
            angle += Time.deltaTime * Speed;
        }
    }

    public void Kill()
    {
        dead = true;
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = 180;
        body.bodyType = RigidbodyType2D.Dynamic;
        boxCollider2D.enabled = false;
        transform.eulerAngles = eulerAngles;
        Destroy(gameObject, 2f);
    }

    void OnValidate()
    {
        center = transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(center + new Vector2(Range, 0), 0.5f);
        Gizmos.DrawWireSphere(center + new Vector2(-Range, 0), 0.5f);
    }
}
