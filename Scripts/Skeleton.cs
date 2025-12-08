using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public int hp = 3;

    [Header("DPS Settings")]
    public float damageInterval = 1f;

    private Transform player;
    private Material[] mats;

    private bool touchingPlayer = false;
    private float damageTimer = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        // AMBIL SEMUA SkinnedMeshRenderer DI DALAM MODEL
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        if (renderers.Length == 0)
        {
            Debug.LogError("❌ Skeleton ERROR: Tidak menemukan SkinnedMeshRenderer!");
            return;
        }

        // Simpan semua material instance
        List<Material> list = new List<Material>();

        foreach (var r in renderers)
        {
            Material[] instanced = new Material[r.materials.Length];

            for (int i = 0; i < instanced.Length; i++)
            {
                instanced[i] = new Material(r.materials[i]); // buat material instance
                list.Add(instanced[i]);
            }

            r.materials = instanced; // apply instance ke renderer tersebut
        }

        mats = list.ToArray();
    }

    void Update()
    {
        MoveTowardsPlayer();
        HandleDPS();
    }

    // =============================
    // MOVEMENT 
    // =============================
    void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        transform.position += dir * speed * Time.deltaTime;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    // =============================
    // DAMAGE PER SECOND TO PLAYER
    // =============================
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

    // =============================
    // RECEIVE DAMAGE
    // =============================
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        StartCoroutine(HitFlash());

        if (hp <= 0)
            StartCoroutine(Death());
    }

    // =============================
    // HIT FLASH
    // =============================
    IEnumerator HitFlash()
    {
        // ON
        foreach (var m in mats)
        {
            if (m.HasProperty("_FlashStrength"))
                m.SetFloat("_FlashStrength", 1f);
        }

        yield return new WaitForSeconds(0.15f);

        // OFF
        foreach (var m in mats)
        {
            if (m.HasProperty("_FlashStrength"))
                m.SetFloat("_FlashStrength", 0f);
        }
    }

    // =============================
    // DEATH ANIMATION
    // =============================
    IEnumerator Death()
    {
        for (float t = 1f; t > 0f; t -= Time.deltaTime * 2f)
        {
            transform.localScale = Vector3.one * t;
            yield return null;
        }

        UIManager.Instance.AddScore(1);
        Destroy(gameObject);
    }
}
