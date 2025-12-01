using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject explosionFX;
    public float selfDestructTime = 5f;

    void Start()
    {
        // Auto destroy kalau fireball gak kena apa-apa
        Destroy(gameObject, selfDestructTime);
    }

    void OnTriggerEnter(Collider col)
    {
        // Ignore collision dengan Player
        if (col.CompareTag("Player")) return;

        // Kena enemy
        if (col.CompareTag("Enemy"))
        {
            Skeleton sk = col.GetComponent<Skeleton>();
            if (sk != null)
            {
                sk.TakeDamage(1);
            }
        }

        // Spawn explosion effect
        if (explosionFX != null)
        {
            GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(fx, 1f); // auto cleanup
        }

        Destroy(gameObject);
    }
}
