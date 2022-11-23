using UnityEngine;
using Zork.Common;
using TMPro;
using Newtonsoft.Json;
using System;

public class GameManager : MonoBehaviour
{
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


    private void Awake()
    {
        TextAsset gameJson = Resources.Load<TextAsset>("GameJson");
        _game = JsonConvert.DeserializeObject<Game>(gameJson.text);
        _game.Player.LocationChanged += Player_LocationChanged;
        _game.Player.MovesChanged += Player_MovesChanged;
        _game.Player.ChangeScore += Player_ChangeScore;
        _game.Run(InputService, OutputService);
    }

    private void Player_MovesChanged(object sender, int moves)
    {
        MovesText.text = "Moves:" + _game.Player.Moves.ToString();
    }

    private void Player_ChangeScore(object sender, int score)
    {
        ScoreText.text = "Score:" + _game.Player.Score.ToString();
    }

    public void Player_LocationChanged(object sender, Room location)
    {
        LocationText.text = location.Name;
    }

    private void Start()
    {
        InputService.SetFocus();
        LocationText.text = _game.Player.CurrentRoom.Name;
    }
    
     void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            InputService.ProcessInput();
            InputService.SetFocus();
        }

        if (_game.IsRunning == false)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    private Game _game;
}