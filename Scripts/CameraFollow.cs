using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Wizard
    public Vector3 offset = new Vector3(0, 12, -10);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Target position camera = posisi player + offset
        Vector3 desiredPos = target.position + offset;

        // Smooth follow (manual lerp)
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );

        // Optional: camera tetap menghadap player
        transform.LookAt(target);
    }
}
