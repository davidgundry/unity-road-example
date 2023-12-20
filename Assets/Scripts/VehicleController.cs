using System;
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

    [field: SerializeField, Range(0f, 0.01f)]
    public float friction { get; private set;}

    [field: SerializeField, Range(0f, 60f)]
    public float turnSpeed {get; private set;}

    [field: SerializeField, Range(0f, 60f)]
    public float wheelTurnAngle { get; private set;}

    [field: SerializeField, Range(0f, 20f), Header("Read only")]
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
        if (speed < 0)
            turnAmount = -turnAmount;
        transform.Rotate(Vector3.up * Time.deltaTime  * turnSpeed * turnAmount);

        speed += Time.deltaTime * acceleration * verticalInput;
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

}

