using System;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [field: SerializeField, Range(0f, 40f)]
    public float MaxSpeed {get; private set;}

    [field: SerializeField, Range(0f, 40f), Tooltip("Change in velocity per second")]
    public float Acceleration {get; private set;}

    [field: SerializeField, Range(0f, 40f)]
    public float BreakAcceleration {get; private set;}

    [field: SerializeField, Range(0f, 40f)]
    public float ReverseAcceleration {get; private set;}

    [field: SerializeField, Range(0f, 0.03f)]
    public float Friction { get; private set;}

    [field: SerializeField, Range(0f, 60f)]
    public float TurnSpeed {get; private set;}

    [field: SerializeField, Range(0f, 40f), Header("Read only")]
    public float Speed { get; private set;}

    void Start()
    {
        Speed = 0;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float turnAmount = horizontalInput * EaseOutQuintic(Math.Abs(Speed/MaxSpeed));
        if (transform.position.x < -8)
            turnAmount = Math.Max(turnAmount, (-8-transform.position.x)/5);
        if (transform.position.x > 8)
            turnAmount = Math.Min(turnAmount, -(transform.position.x -8)/5);
        if (Speed < 0)
            turnAmount = -turnAmount;
        transform.Rotate(Vector3.up * Time.deltaTime  * TurnSpeed * turnAmount);

        if (verticalInput > 0)
            Speed += Time.deltaTime * Acceleration * verticalInput;
        if (Speed > 0 && verticalInput < 0)
            Speed += Time.deltaTime * BreakAcceleration * verticalInput;
        if (Speed <= 0 && verticalInput < 0)
            Speed += Time.deltaTime * ReverseAcceleration * verticalInput;
        Speed *= 1-Friction;
        Speed = Math.Min(MaxSpeed, Speed);

        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }

    float EaseOutQuintic(float value)
    {
        value--;
        return 1 * (value * value * value * value * value + 1) + 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Cone":
            case "Barrier":
                Speed *= 0.95f;
                break;
            case "Barrel":
            case "Crate":
                Speed *= 0.9f;
                break;
            case "Spool":
            case "Rock":
                Speed *= 0.8f;
                break;
        }
    }
}
