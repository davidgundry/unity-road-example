using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class PauseController : MonoBehaviour
{

    [field: SerializeField]
    public bool paused {get; private set;}

    public GameObject pausedScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
            paused = !paused;
        pausedScreen.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }
}
