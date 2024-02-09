using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MPHBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public VehicleController Player {get; private set;}
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Time.timeScale > 0)
            _text.text = Mathf.RoundToInt(Player.Speed).ToString();
    }
}
