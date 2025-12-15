using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public int hp = 7;

    [Header("DPS Settings")]
    public float damageInterval = 1f;

    private Transform player;
    private Material[] mats;

    private bool touchingPlayer = false;
    private float damageTimer = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        List<Material> list = new List<Material>();

        foreach (var r in renderers)
        {
            Material[] instanced = new Material[r.materials.Length];
            for (int i = 0; i < instanced.Length; i++)
            {
                instanced[i] = new Material(r.materials[i]);
                list.Add(instanced[i]);
            }
            r.materials = instanced;
        }

        mats = list.ToArray();
    }

    void Update()
    {
        MoveTowardsPlayer();
        HandleDPS();
    }

    void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        transform.position += dir * speed * Time.deltaTime;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    void HandleDPS()
    {
        if (!touchingPlayer) return;

        damageTimer += Time.deltaTime;
        if (damageTimer >= damageInterval)
        {
            damageTimer = 0f;
            player.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = true;
            damageTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            touchingPlayer = false;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        StartCoroutine(HitFlash());

        if (hp <= 0)
            StartCoroutine(Death());
    }

    IEnumerator HitFlash()
    {
        foreach (var m in mats)
            if (m.HasProperty("_FlashStrength"))
                m.SetFloat("_FlashStrength", 1f);

        yield return new WaitForSeconds(0.15f);

        foreach (var m in mats)
            if (m.HasProperty("_FlashStrength"))
                m.SetFloat("_FlashStrength", 0f);
    }

    IEnumerator Death()
    {
        foreach (var c in GetComponentsInChildren<Collider>())
            c.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        for (float t = 1f; t > 0f; t -= Time.deltaTime * 2f)
        {
            transform.localScale = Vector3.one * t;
            yield return null;
        }

        UIManager.Instance?.AddScore(1);

        Destroy(gameObject);
    }
}
