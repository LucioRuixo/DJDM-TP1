using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager_MainMenu : MonoBehaviour
{
    public GameObject mainMenu, modeSelectionMenu, creditsMenu;
    public GameObject mainMenuFirstSelected;
    public GameObject modeSelectionMenuFirstSelected, modeSelectionMenuClosedSelected;
    public GameObject creditsMenuFirstSelected, creditsMenuClosedSelected;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
    }

    public void ToggleModeSelectionMenu(bool value)
    {
        modeSelectionMenu.SetActive(value);
        mainMenu.SetActive(!value);

        if (value)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(modeSelectionMenuFirstSelected);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(modeSelectionMenuClosedSelected);
        }
    }

    public void ToggleCreditsMenu(bool value)
    {
        creditsMenu.SetActive(value);
        mainMenu.SetActive(!value);

        if (value)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditsMenuFirstSelected);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditsMenuClosedSelected);
        }
    }
}