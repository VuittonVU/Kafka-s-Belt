using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public GameObject fireballPrefab;//prefab yang akan di munculkan
    public Transform firePoint;//posisi dan rotasi fireball ditembak 
    public float maxScale = 3f;
    public float chargeSpeed = 1f;

    GameObject currentFB;//fireball yang dicharge
    float charge = 0f;

    void Update()
    {
        // === BEGIN HOLD ===
        if (Input.GetMouseButtonDown(0))
        {
            charge = 0f;

            // spawn small fireball (charging)
            currentFB = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            currentFB.transform.localScale = Vector3.one * 0.3f;

            // Update emission
            currentFB.GetComponent<Renderer>().material.SetFloat("_EmissionStrength",
                currentFB.transform.localScale.x);//makin besar makin terang
        }

        // === HOLDING ===
        if (Input.GetMouseButton(0) && currentFB != null)
        {
            charge += Time.deltaTime * chargeSpeed;

            // scale grows with charge
            float newScale = Mathf.Clamp(currentFB.transform.localScale.x + Time.deltaTime * chargeSpeed, 0.3f, maxScale);//fireball yang dicharge+skala yang di sumbu x, clamp buat paksa max value
            currentFB.transform.localScale = Vector3.one * newScale;

            // Update emission
            currentFB.GetComponent<Renderer>().material.SetFloat("_EmissionStrength", newScale);
        }

        // === RELEASE ===
        if (Input.GetMouseButtonUp(0) && currentFB != null)
        {
            // Apply velocity (FIX)
            Rigidbody rb = currentFB.GetComponent<Rigidbody>();
            rb.linearVelocity = firePoint.forward * 20f;//tembak ke depan 

            // Set damage based on scale (ceil to int)
            int dmg = Mathf.CeilToInt(currentFB.transform.localScale.x);
            currentFB.GetComponent<Fireball>().damage = dmg;

            currentFB = null;
        }
    }
}

