using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float maxScale = 3f;
    public float chargeSpeed = 1f;

    GameObject currentFB;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentFB = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            currentFB.transform.localScale = Vector3.one * 0.3f;

            // Update emission
            currentFB.GetComponent<Renderer>().material.SetFloat("_EmissionStrength",
                currentFB.transform.localScale.x);
        }

        if (Input.GetMouseButton(0) && currentFB != null)
        {
            currentFB.transform.localScale += Vector3.one * chargeSpeed * Time.deltaTime;

            if (currentFB.transform.localScale.x > maxScale)
                currentFB.transform.localScale = Vector3.one * maxScale;

            // Update emission during charging
            currentFB.GetComponent<Renderer>().material.SetFloat("_EmissionStrength",
                currentFB.transform.localScale.x);
        }

        if (Input.GetMouseButtonUp(0) && currentFB != null)
        {
            Rigidbody rb = currentFB.GetComponent<Rigidbody>();
            rb.linearVelocity = firePoint.forward * 20f;

            currentFB = null;
        }
    }
}
