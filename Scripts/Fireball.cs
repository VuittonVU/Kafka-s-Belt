using UnityEngine;
using System.Collections.Generic;

public class Fireball : MonoBehaviour
{
    [Header("General Settings")]
    public float lifeTime = 5f;
    public int damage = 1;

    [Header("AOE Settings")]
    public float aoeThreshold = 2f;  
    public float aoeMultiplier = 1.0f;
    public LayerMask enemyMask;

    [Header("Effects")]
    public GameObject explosionFX;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;
        Skeleton sk = other.GetComponentInParent<Skeleton>();
        if (sk != null)
        {
            sk.TakeDamage(damage);
        }
        TryAOE();

        Destroy(gameObject);
    }

    void TryAOE()
    {
        float scale = transform.localScale.x;

        if (scale < aoeThreshold)
        {
            SpawnFX();
            return;
        }

        float radius = scale * aoeMultiplier;

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            radius,
            enemyMask,
            QueryTriggerInteraction.Ignore
        );

        HashSet<Skeleton> damaged = new HashSet<Skeleton>();

        foreach (Collider c in hits)
        {
            Skeleton sk = c.GetComponentInParent<Skeleton>();
            if (sk != null && !damaged.Contains(sk))
            {
                sk.TakeDamage(damage);
                damaged.Add(sk);
            }
        }

        SpawnFX();
    }

    void SpawnFX()
    {
        if (explosionFX != null)
        {
            GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(fx, 1f);
        }
    }
}
