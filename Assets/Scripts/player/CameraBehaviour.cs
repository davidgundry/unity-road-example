using System;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject target;

    [field: SerializeField, Range(5f, 30f)]
    public float Distance {get; private set;}

    [field: SerializeField, Range(0f, 10f)]
    public float ProportionalSpeed {get; private set;}

    [field: SerializeField, Range(0f, 10f)]
    public float ReversingDistance { get; private set;}

    private Vector3 _lastTargetLocation;

    void LateUpdate()
    {
        Vector3 targetLocation = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z - Distance);

        bool reversing = false;
        if (!reversing && _lastTargetLocation != null && targetLocation.z < _lastTargetLocation.z - 1*Time.deltaTime)
            reversing = true;
        if (reversing && targetLocation.z > _lastTargetLocation.z + 1*Time.deltaTime)
            reversing = false;

        _lastTargetLocation = targetLocation;
        Vector3 targetLocationWithReverse =  targetLocation + new Vector3(0,0,reversing ? -ReversingDistance : 0);
        
        float displacement = (transform.position - targetLocationWithReverse).magnitude;
        transform.position = Vector3.MoveTowards(transform.position, targetLocationWithReverse, displacement * Time.deltaTime*ProportionalSpeed);
    }
}
