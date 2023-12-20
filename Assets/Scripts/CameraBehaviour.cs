using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject target;

    [field: SerializeField, Range(5f, 30f)]
    public float distance {get; private set;}

    [field: SerializeField, Range(0f, 10f)]
    public float proportionalSpeed {get; private set;}

    [field: SerializeField, Range(0f, 10f)]
    public float reversingDistance { get; private set;}

    private Vector3 lastTargetLocation;

    private bool reversing = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetLocation = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z - distance);

        bool reversing = false;
        if (!reversing && lastTargetLocation != null && targetLocation.z < lastTargetLocation.z - 1*Time.deltaTime)
            reversing = true;
        if (reversing && targetLocation.z > lastTargetLocation.z + 1*Time.deltaTime)
            reversing = false;

        lastTargetLocation = targetLocation;
        Vector3 targetLocationWithReverse =  (targetLocation + new Vector3(0,0,reversing ? -reversingDistance : 0));
        
        float displacement = (transform.position - targetLocationWithReverse).magnitude;
        transform.position = Vector3.MoveTowards(transform.position, targetLocationWithReverse, displacement * Time.deltaTime*proportionalSpeed);
    }
}
