using UnityEngine;

public class Insect : MonoBehaviour
{
    [SerializeField] float Speed = 0;
    [SerializeField] LayerMask GroundLayer;
    Rigidbody2D body;
    BoxCollider2D boxCollider2D;
    float lastTime = 0;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (Time.time - lastTime >= 1)
            {
                Vector2 eulerAngles = transform.rotation.eulerAngles;
                eulerAngles.y -= 180;
                transform.eulerAngles = eulerAngles;
                lastTime = Time.time;
            }
            else
            {
                transform.position += Speed * Time.deltaTime * transform.right;
            }
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
}
