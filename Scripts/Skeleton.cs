using System.Collections;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed = 2f;
    public int hp = 2;
    Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        transform.position += dir * speed * Time.deltaTime;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        StartCoroutine(HitFlash());

        if (hp <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator HitFlash()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        Material mat = r.material;

        mat.SetFloat("_HitStrength", 1f);
        yield return new WaitForSeconds(0.1f);
        mat.SetFloat("_HitStrength", 0f);
    }

    IEnumerator Death()
    {
        for (float t = 1f; t > 0f; t -= Time.deltaTime * 2f)
        {
            transform.localScale = Vector3.one * t;
            yield return null;
        }
        Destroy(gameObject);
    }
}
