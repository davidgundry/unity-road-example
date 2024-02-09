using System;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class VehicleController : MonoBehaviour
{
    [field: SerializeField]
    public GameObject[] wheels  {get; private set;}

    [field: SerializeField, Range(0f, 40f)]
    public float maxSpeed {get; private set;}

    [field: SerializeField, Range(0f, 40f), Tooltip("Change in velocity per second")]
    public float acceleration {get; private set;}

    [field: SerializeField, Range(0f, 40f)]
    public float breakAcceleration {get; private set;}

    [field: SerializeField, Range(0f, 40f)]
    public float reverseAcceleration {get; private set;}

    [field: SerializeField, Range(0f, 0.03f)]
    public float friction { get; private set;}

    [field: SerializeField, Range(0f, 60f)]
    public float turnSpeed {get; private set;}

    [field: SerializeField, Range(0f, 60f)]
    public float wheelTurnAngle { get; private set;}

    [field: SerializeField, Range(0f, 40f), Header("Read only")]
    public float speed { get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float turnAmount = horizontalInput * EaseOutQuintic(Math.Abs(speed/maxSpeed));
        if (transform.position.x < -8)
            turnAmount = Math.Max(turnAmount, (-8-transform.position.x)/5);
        if (transform.position.x > 8)
            turnAmount = Math.Min(turnAmount, -(transform.position.x -8)/5);
        if (speed < 0)
            turnAmount = -turnAmount;
        transform.Rotate(Vector3.up * Time.deltaTime  * turnSpeed * turnAmount);

        if (verticalInput > 0)
            speed += Time.deltaTime * acceleration * verticalInput;
        if (speed > 0 && verticalInput < 0)
            speed += Time.deltaTime * breakAcceleration * verticalInput;
        if (speed <= 0 && verticalInput < 0)
            speed += Time.deltaTime * reverseAcceleration * verticalInput;
        speed *= 1-friction;
        speed = Math.Min(maxSpeed, speed);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        

        float wheelRadius = 1;
        for (int i=0;i<wheels.Length;i++)
            wheels[i].transform.Rotate(Vector3.right * (Time.deltaTime * speed / (wheelRadius * Mathf.PI*2)) * 360);
        for (int i=0;i<2;i++)
            wheels[i].transform.eulerAngles = new Vector3(0, horizontalInput * wheelTurnAngle + transform.rotation.eulerAngles.y, 0);

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
                speed *= 0.95f;
                break;
            case "Barrel":
            case "Crate":
                speed *= 0.9f;
                break;
            case "Spool":
            case "Rock":
                speed *= 0.8f;
                break;
        }
        
    }

}

