using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;//biar tau kamera mana yang digunakan raycast mouse
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

        Vector3 moveDir = new Vector3(h, 0f, v).normalized;//normalized biar kecepatan tetap sama walaupun gerak diagonal 

        // Manual transform movement
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void RotateToMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);//dimulai dari kamera, menuju posisi mouse di layar.

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f; // biar ga miring ke atas/bawah

            if (lookDir != Vector3.zero)//membuat player hadap ke mana
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(//slerp biar smooth
                    transform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }
}

