using UnityEngine;

public class MarioCamera_VNLong : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float yOffset = 1f;

    private float minX;

    void Start()
    {
        if (target != null)
        {
            minX = target.position.x;
            transform.position = new Vector3(minX, target.position.y + yOffset, transform.position.z);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        float targetX = Mathf.Max(minX, target.position.x);
        Vector3 desiredPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        minX = Mathf.Max(minX, target.position.x);
    }
}
