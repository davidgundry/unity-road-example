using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class BGMBehaviour : MonoBehaviour
{
    public VehicleController player;

    private AudioSource _audioSource;
    private AudioLowPassFilter _filter;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _filter = GetComponent<AudioLowPassFilter>();
    }


    void Update()
    {
        if (player.Speed < 1)
            _audioSource.volume = 0.4f;
        else if (player.Speed < 10)
            _audioSource.volume = 0.3f + BezierBlend(player.Speed/player.MaxSpeed) * 0.2f;
        else
            _audioSource.volume = 0.5f;
        
        if (Time.timeScale == 0)
            _audioSource.volume = 0.02f;
        _filter.enabled = Time.timeScale == 0;
    }

    //https://stackoverflow.com/a/25730573
    float BezierBlend(float t)
    {
        return t * t * (3.0f - 2.0f * t);
    }
}
