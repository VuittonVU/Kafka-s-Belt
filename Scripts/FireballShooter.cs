using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;

    public float minScale = 0.3f;
    public float maxScale = 3f;
    public float chargeSpeed = 1f;
    public float shootSpeed = 20f;

    private GameObject chargingFB;
    private float chargeTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            chargeTime = 0f;

            chargingFB = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            chargingFB.transform.localScale = Vector3.one * minScale;

            chargingFB.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (Input.GetMouseButton(0) && chargingFB != null)
        {
            chargeTime += Time.deltaTime * chargeSpeed;

            float newScale = Mathf.Clamp(minScale + chargeTime, minScale, maxScale);
            chargingFB.transform.localScale = Vector3.one * newScale;

            chargingFB.transform.position = firePoint.position;
            chargingFB.transform.rotation = firePoint.rotation;

            var rend = chargingFB.GetComponent<Renderer>();
            if (rend != null && rend.material.HasProperty("_EmissionStrength"))
                rend.material.SetFloat("_EmissionStrength", newScale * 2f);

            Light l = chargingFB.GetComponentInChildren<Light>();
            if (l != null)
                l.intensity = 1f + newScale * 1.5f;
        }

        if (Input.GetMouseButtonUp(0) && chargingFB != null)
        {
            float scale = chargingFB.transform.localScale.x;

            int dmg = Mathf.Max(1, Mathf.RoundToInt(scale * 3f));
            chargingFB.GetComponent<Fireball>().damage = dmg;

            Rigidbody rb = chargingFB.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.linearVelocity = firePoint.forward * shootSpeed;

            chargingFB = null;
        }
    }
}
