using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using Jesus.Hands;
using Jesus.Cards;
using UnityEngine.SceneManagement;

public class MenuVoice : MonoBehaviour
{
    private KeywordRecognizer keywordrecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public MenuManager mm;
    public GeneralSelector gs;

    public static bool GameIsPaused = false;


    void Start()
    {
        mm = GetComponent<MenuManager>();

        mm.ChangeMenu(0);

        actions.Add("menu", GoToMain);
        actions.Add("main menu", GoToMain);

        actions.Add("generals", GoToGeneral);
        actions.Add("general select", GoToGeneral);

        actions.Add("play game", PlayGame);
        actions.Add("play", PlayGame);
        actions.Add("start", PlayGame);

        actions.Add("next", Next);
        actions.Add("back", Back);


        actions.Add("one", General1);
        actions.Add("isaac", General1);
        actions.Add("big guy", General1);
        actions.Add("muscle", General1);
        actions.Add("muscles", General1);
        actions.Add("muscle man", General1);


        actions.Add("two", General2);
        actions.Add("nathan", General2);
        actions.Add("ginger", General2);
        actions.Add("ron", General2);
        actions.Add("ron weasley", General2);
        actions.Add("weasley", General2);


        keywordrecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordrecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordrecognizer.Start();

    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    

    #region Menu

    private void GoToMain()
    {
        mm.ChangeMenu(0);
    }

    private void GoToGeneral()
    {
        mm.ChangeMenu(1);
    }

    private void Back()
    {
        if (mm.menuNum == 0)
        {
            return;
        }
        else if (mm.menuNum == 1)
        {
            GoToMain();
        }
    }

    private void Next()
    {
        if (mm.menuNum == 0)
        {
            GoToGeneral();
        }
        else if (mm.menuNum == 1)
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        if (mm.menuNum == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (mm.menuNum == 0)
        {
            GoToGeneral();
        }
    }

    public void QuitGame()
    {
        mm.ChangeMenu(2);
    }





    #endregion


    public void General1()
    {
        gs.PickGeneral(1);
    }

    public void General2()
    {
        gs.PickGeneral(2);

    }
}
