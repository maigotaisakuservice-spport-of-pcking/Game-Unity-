using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour, IAnomaly
{
    private Light targetLight;
    private Coroutine flickerCoroutine;

    private void Awake()
    {
        targetLight = GetComponent<Light>();
        targetLight.enabled = false; // Start with the light off (or in its default state)
    }

    public void Activate()
    {
        if (flickerCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(Flicker());
        }
    }

    public void Deactivate()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }
        targetLight.enabled = false; // Reset to a known state
    }

    private IEnumerator Flicker()
    {
        // For demonstration, let's make it flicker for a few seconds
        float endTime = Time.time + 5f;

        while(Time.time < endTime)
        {
            targetLight.enabled = !targetLight.enabled;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
        }

        targetLight.enabled = false; // Ensure it ends in off state
    }
}
