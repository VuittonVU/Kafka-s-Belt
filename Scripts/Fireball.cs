using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject explosionFX;
    public float selfDestructTime = 5f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, selfDestructTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player")) return;

        // Jika kena enemy langsung
        if (col.CompareTag("Enemy"))
        {
            ApplyDamage(col.gameObject);
        }

        // SELALU meledak (untuk AOE)
        ExplodeAOE();

        Destroy(gameObject);
    }

    void ApplyDamage(GameObject enemy)
    {
        Skeleton sk = enemy.GetComponent<Skeleton>();
        if (sk != null)
            sk.TakeDamage(damage);
    }

    void ExplodeAOE()
    {
        // AOE cuma aktif kalau fireball scale >= 2
        float aoeRadius = transform.localScale.x;

        if (aoeRadius < 2f)
        {
            // Tidak cukup besar → tidak AOE
            return;
        }

        // Deteksi semua enemy dalam radius
        Collider[] hits = Physics.OverlapSphere(transform.position, aoeRadius);

        foreach (Collider c in hits)
        {
            if (c.CompareTag("Enemy"))
                ApplyDamage(c.gameObject);
        }

        // Efek ledakan
        if (explosionFX != null)
        {
            GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(fx, 1f);
        }
    }
}
