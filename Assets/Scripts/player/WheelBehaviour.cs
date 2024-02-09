using System;
using UnityEngine;

[RequireComponent(typeof(VehicleController))]
public class WheelBehaviour : MonoBehaviour
{    
    [field: SerializeField]
    public GameObject[] Wheels  {get; private set;}
    [field: SerializeField, Range(0f, 60f)]
    public float WheelTurnAngle { get; private set;}

    private VehicleController _vehicleController;

    private readonly float wheelRadius = 1;

    void Start()
    {
        _vehicleController = GetComponent<VehicleController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        for (int i=0;i<Wheels.Length;i++)
            Wheels[i].transform.Rotate(Vector3.right * (Time.deltaTime * this._vehicleController.Speed / (wheelRadius * Mathf.PI*2)) * 360);
        for (int i=0;i<2;i++)
            Wheels[i].transform.eulerAngles = new Vector3(0, horizontalInput * WheelTurnAngle + transform.rotation.eulerAngles.y, 0);
    }
}
