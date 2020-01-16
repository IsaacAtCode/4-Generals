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

    public static bool GameIsPaused = false;


    void Start()
    {
        mm = GetComponent<MenuManager>();

        keywordrecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordrecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordrecognizer.Start();

        mm.mainPanel = GameObject.Find("Main");
        mm.generalPanel = GameObject.Find("GeneralSelect");
        mm.settingsPanel = GameObject.Find("Settings");

        mm.mainPanel.SetActive(true);


        actions.Add("play game", PlayGame);
        actions.Add("play", PlayGame);

    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    

    #region Menu
    private void HideAllMenus()
    {
        mm.mainPanel.SetActive(false);
        mm.generalPanel.SetActive(false);
        mm.settingsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }




    #endregion

}
