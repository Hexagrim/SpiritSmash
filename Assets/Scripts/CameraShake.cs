using UnityEngine;
using Cinemachine;
// THIS IS AI GENERATED BTW DONT SMOKE ME, IM LAZY TO DO IT rn HAIYA.
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin perlin;

    void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        if (vCam == null)
        {
            Debug.LogError("CameraShake requires a Cinemachine Virtual Camera!");
            return;
        }

        // Get the noise component
        perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin == null)
        {
            Debug.LogError("CinemachineBasicMultiChannelPerlin not found on this vCam.");
        }
    }

    /// <summary>
    /// Shake the camera
    /// </summary>
    /// <param name="duration">Time the shake lasts</param>
    /// <param name="frequency">Oscillation frequency (Hz)</param>
    /// <param name="amplitude">Shake amplitude (strength)</param>
    public void Shake(float duration, float frequency, float amplitude)
    {
        if (perlin == null) return;

        StopAllCoroutines(); // stop previous shake
        StartCoroutine(DoShake(duration, frequency, amplitude));
    }

    private System.Collections.IEnumerator DoShake(float duration, float frequency, float amplitude)
    {
        perlin.m_FrequencyGain = frequency;
        perlin.m_AmplitudeGain = amplitude;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset after shake
        perlin.m_AmplitudeGain = 0f;
        perlin.m_FrequencyGain = 0f;
    }
}
