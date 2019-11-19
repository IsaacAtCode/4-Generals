using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    //UI Elements
    public Canvas menuCanvas;
    public GameObject mainPanel;
    public GameObject generalPanel;
    public GameObject settingsPanel;


    //Menu State
    enum MenuState { Main, GeneralSelect, Settings, Quiting }
    MenuState menuState = MenuState.Main;
    public int menuNum;

    private void Start()
    {
        mainPanel = GameObject.Find("Main");
        generalPanel = GameObject.Find("GeneralSelect");
        settingsPanel = GameObject.Find("Settings");

        OpenMenu(mainPanel);
    }



    public void ChangeMenu(int newMenu)
    {
        menuNum = newMenu;

        switch(menuNum)
        {
            case 0:
                menuState = MenuState.Main;
                OpenMenu(mainPanel);
                Debug.Log(menuState);
                break;
            case 1:
                menuState = MenuState.GeneralSelect;
                OpenMenu(generalPanel);

                Debug.Log(menuState);
                break;
            case 2:
                menuState = MenuState.Settings;
                OpenMenu(settingsPanel);

                Debug.Log(menuState);
                break;
            case 3:
                menuState = MenuState.Quiting;
                Debug.Log(menuState);
                QuitGame();
                break;
        }
    }

    private void HideAllMenus()
    {
        mainPanel.SetActive(false);
        generalPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void OpenMenu(GameObject newMenu)
    {
        HideAllMenus();
        newMenu.SetActive(true);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
