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



    public int menuNum;

    private void Start()
    {
        OpenMenu(mainPanel);
    }

    public void ChangeMenu(int newMenu)
    {
        menuNum = newMenu;

        switch(menuNum)
        {
            case 0:
                OpenMenu(mainPanel);
                break;
            case 1:
                OpenMenu(generalPanel);
                break;
            case 2:
                QuitGame();
                break;
        }
    }

    private void OpenMenu(GameObject newMenu)
    {
        HideAllMenus();
        newMenu.SetActive(true);
    }

    private void HideAllMenus()
    {
        mainPanel.SetActive(false);
        generalPanel.SetActive(false);
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

