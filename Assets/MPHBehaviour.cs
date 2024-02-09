using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MPHBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public VehicleController player {get; private set;}

    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
            text.text = Mathf.RoundToInt(player.speed).ToString();
    }
}
