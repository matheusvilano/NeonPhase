using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public float CameraPos;

    private void Update() => FollowTarget();

    private void FollowTarget()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -CameraPos);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
