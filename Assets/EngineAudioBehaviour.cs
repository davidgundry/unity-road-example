using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudioBehaviour : MonoBehaviour
{

    [field: SerializeField]
    public VehicleController player { get; private set;}

    [field: SerializeField]
    public AudioSource engineSound { get; private set; }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            engineSound.volume = 0.01f;
            engineSound.pitch = 0f;
        }
        else
        {
            float speedProp = player.speed/player.maxSpeed;
            engineSound.volume = InRange(0.4f, 1, speedProp);
            engineSound.pitch = InRange(0.3f, 0.5f, speedProp);
        }
    }

    float InRange(float min, float max, float value)
    {
        float range = max-min;
        return value*range + min;
    }
}
