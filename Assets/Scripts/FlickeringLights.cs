using UnityEngine;

public class FireballLightFlicker : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 1.2f;
    public float maxIntensity = 2.2f;
    public float flickerSpeed = 8f;

    void Update()
    {
        if (fireLight == null) return;

        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        fireLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
