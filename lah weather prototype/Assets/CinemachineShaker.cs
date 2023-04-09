using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShaker : MonoBehaviour
{
    public static CinemachineShaker instance { get; private set; }
    private CinemachineVirtualCamera cinemachine;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake()
    {
        instance = this;
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Debug.Log(cinemachineBasicMultiChannelPerlin);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
        startingIntensity = intensity;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
            }
        }
    }
}
