using UnityEngine;

public class PauseController : MonoBehaviour
{
    [field: SerializeField]
    public bool paused {get; private set;}

    public GameObject pausedScreen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
            paused = !paused;
        pausedScreen.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }
}
