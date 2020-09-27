using UnityEngine;

public class GameOptionsManager : MonoBehaviour
{
    public static GameOptionsManager instance;

    public enum GameMode
    {
        Singleplayer,
        LocalMultiplayer
    }

    public enum Difficulty
    {
        Easy,
        Hard
    }

    public GameMode gameMode;
    public Difficulty difficulty;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        SelectionMenu.OnGameOptionsSelected += SetGameOptions;
    }

    void OnDisable()
    {
        SelectionMenu.OnGameOptionsSelected -= SetGameOptions;
    }

    void SetGameOptions(GameMode _gameMode, Difficulty _difficulty)
    {
        gameMode = _gameMode;
        difficulty = _difficulty;
    }
}