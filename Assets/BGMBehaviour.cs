using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMBehaviour : MonoBehaviour
{

    public VehicleController player;

    private AudioSource audioSource;
    private AudioLowPassFilter filter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        filter = GetComponent<AudioLowPassFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.speed < 1)
            audioSource.volume = 0.4f;
        else if (player.speed < 10)
            audioSource.volume = 0.3f + BezierBlend(player.speed/player.maxSpeed) * 0.2f;
        else
            audioSource.volume = 0.5f;
        
        if (Time.timeScale == 0)
        {
            audioSource.volume = 0.02f;
        }
        filter.enabled = Time.timeScale == 0;
    }

    //https://stackoverflow.com/a/25730573
    float BezierBlend(float t)
    {
        return t * t * (3.0f - 2.0f * t);
    }
}
