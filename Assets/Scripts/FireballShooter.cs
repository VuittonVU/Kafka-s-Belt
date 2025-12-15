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

            Fireball fb = chargingFB.GetComponent<Fireball>();
            fb.baseScale = minScale;
            fb.scaleGrowth = 1f;

            fb.Charge(0f, maxScale);

            chargingFB.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (Input.GetMouseButton(0) && chargingFB != null)
        {
            chargeTime += Time.deltaTime * chargeSpeed;

            Fireball fb = chargingFB.GetComponent<Fireball>();
            fb.Charge(chargeTime, maxScale);

            chargingFB.transform.position = firePoint.position;
            chargingFB.transform.rotation = firePoint.rotation;

            float scale = fb.manualScale;

            Renderer rend = chargingFB.GetComponent<Renderer>();
            if (rend != null && rend.material.HasProperty("_EmissionStrength"))
                rend.material.SetFloat("_EmissionStrength", scale * 2f);

            Light l = chargingFB.GetComponentInChildren<Light>();
            if (l != null)
                l.intensity = 1f + scale * 1.5f;
        }

        if (Input.GetMouseButtonUp(0) && chargingFB != null)
        {
            Fireball fb = chargingFB.GetComponent<Fireball>();
            float scale = fb.manualScale;

            int dmg = Mathf.Max(1, Mathf.RoundToInt(scale * 3f));
            fb.damage = dmg;

            Rigidbody rb = chargingFB.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.linearVelocity = firePoint.forward * shootSpeed;

            chargingFB = null;
        }
    }
}
