using TMPro;
using UnityEngine;
using Zork;

public class GameManager : MonoBehaviour
{

    void Awake()
    {
        TextAsset gameJsonAsset = Resources.Load<TextAsset>(ZorkGameFileAssetName);

        Game.Start(gameJsonAsset.text, InputService, OutputService);

        LocationText.text = Game.Instance.Player.Location.ToString();
        // ScoreText.text = Game.Instance.Player.Score.ToString();
        // MovesText.text = Game.Instance.Player.Moves.ToString();
    }

    void Update()
    {
        
    }

    [SerializeField]
    private string ZorkGameFileAssetName = "Zork";

    [SerializeField]
    private UnityInputService InputService;

    [SerializeField]
    private UnityOutputService OutputService;

    [SerializeField]
    private TextMeshProUGUI LocationText;

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private TextMeshProUGUI MovesText;
}
