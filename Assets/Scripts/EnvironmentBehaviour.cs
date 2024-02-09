using UnityEngine;

public class EnvironmentBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public GameObject Player {get; private set;}

    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
    }
}
