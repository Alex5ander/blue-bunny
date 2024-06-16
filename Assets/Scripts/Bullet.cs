using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += 10 * Time.deltaTime * transform.right;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        collider2D.gameObject.TryGetComponent(out Insect insect);
        if (insect)
        {
            insect.Kill();
        }
        Destroy(gameObject);
    }
}
