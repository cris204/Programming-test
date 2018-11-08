using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    Vector3 desiredPosition;
    Vector3 smoothedPosition;

    [SerializeField]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float distanceView;

    private void Start()
    {
        desiredPosition.z = distanceView;
    }

    void FixedUpdate()
    {
        desiredPosition.x = target.position.x + offset.x;
        desiredPosition.y = target.position.y + offset.y;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
       // transform.LookAt(target.transform.position);
       // transform.rotation = Quaternion.Euler(Vector3.right * x);
    }

}