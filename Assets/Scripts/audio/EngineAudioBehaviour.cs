using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudioBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public VehicleController Player { get; private set;}

    [field: SerializeField]
    public AudioSource EngineSound { get; private set; }


    void Update()
    {
        if (Time.timeScale == 0)
        {
            EngineSound.volume = 0.01f;
            EngineSound.pitch = 0f;
        }
        else
        {
            float speedProp = Player.Speed/Player.MaxSpeed;
            EngineSound.volume = InRange(0.4f, 1, speedProp);
            EngineSound.pitch = InRange(0.3f, 0.5f, speedProp);
        }
    }

    float InRange(float min, float max, float value)
    {
        float range = max-min;
        return value*range + min;
    }
}
