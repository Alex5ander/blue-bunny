using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 0;
    [SerializeField] float JumpPower = 0;
    [SerializeField] GameObject Bullet;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] Transform BulletTransform;
    [SerializeField] UnityEvent onGameOver;
    Animator animator;
    Rigidbody2D body;
    BoxCollider2D boxCollider2D;
    float horizontal;
    bool isFloor = false;
    bool shooting = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isFloor)
        {
            body.AddForceY(JumpPower, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Z) && !shooting)
        {
            Shoot();
        }
        if (horizontal != 0)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = horizontal > 0 ? 0 : 180;
            transform.eulerAngles = eulerAngles;
        }
        animator.SetBool("Jump", !isFloor);
    }

    void FixedUpdate()
    {
        isFloor = IsFloor();
        if (!shooting)
        {
            body.velocity = new(horizontal * Speed, body.velocity.y);
        }
        animator.SetBool("Walk", body.velocity.x != 0);
    }

    void Shoot()
    {
        if (animator.GetBool("WithWeapon"))
        {
            Instantiate(Bullet, BulletTransform.position, transform.rotation);
            body.AddForceX(-transform.right.x * 2f, ForceMode2D.Impulse);
            animator.SetTrigger("Shoot");
            shooting = true;
        }
    }

    public void OnEndShoot()
    {
        shooting = false;
    }

    bool IsFloor() => Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, GroundLayer).collider != null;

    void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.TryGetComponent(out Insect insect);
        if (insect)
        {
            Time.timeScale = 0;
            onGameOver.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Weapon")
        {
            animator.SetBool("WithWeapon", true);
            Destroy(other.gameObject);
        }
    }
}
