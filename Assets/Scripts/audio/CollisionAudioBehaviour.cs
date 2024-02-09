using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollisionAudioBehaviour : MonoBehaviour
{
    private AudioSource _audioSource;


    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.pitch += (Random.value/2)-0.25f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.5f)
        {
            _audioSource.Play();
        }
    }
}
