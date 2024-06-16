using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    void LateUpdate()
    {
        transform.position = new(target.position.x, target.position.y, transform.position.z);
    }
}
