using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Move();
        RotateToMouse();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");   // A / D
        float v = Input.GetAxisRaw("Vertical");     // W / S

        Vector3 moveDir = new Vector3(h, 0f, v).normalized;

        // Manual transform movement
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void RotateToMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f; // biar ga miring ke atas/bawah

            if (lookDir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }
}
