using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    GameOptionsManager.GameMode selectedGameMode = GameOptionsManager.GameMode.Singleplayer;
    GameOptionsManager.Difficulty selectedDifficulty = GameOptionsManager.Difficulty.Easy;

    public Toggle singleplayerToggle;
    public Toggle easyToggle;

    public static event Action<GameOptionsManager.GameMode, GameOptionsManager.Difficulty> OnGameOptionsSelected;

    public void ToggleGameMode(bool value)
    {
        selectedGameMode = singleplayerToggle.isOn ? GameOptionsManager.GameMode.Singleplayer : GameOptionsManager.GameMode.LocalMultiplayer;
    }

    public void ToggleDifficulty(bool value)
    {
        selectedDifficulty = easyToggle.isOn ? GameOptionsManager.Difficulty.Easy : GameOptionsManager.Difficulty.Hard;
    }

    public void Play()
    {
        if (OnGameOptionsSelected != null)
            OnGameOptionsSelected(selectedGameMode, selectedDifficulty);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}