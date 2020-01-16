using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using Jesus.Hands;
using Jesus.Cards;
using UnityEngine.SceneManagement;

public class GameVoice : MonoBehaviour
{
    private KeywordRecognizer keywordrecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public CardHolder ch;
    public CameraMove cm;

    public BattleManager bm;

    public void Start()
    {



        //Card Draw
        actions.Add("draw one", DrawOne);
        actions.Add("draw one card", DrawOne);
        actions.Add("draw two", DrawTwo);
        actions.Add("draw two cards", DrawTwo);
        actions.Add("draw three", DrawThree);
        actions.Add("draw three cards", DrawThree);
        actions.Add("draw four", DrawFour);
        actions.Add("draw four cards", DrawFour);
        actions.Add("draw five", DrawFive);
        actions.Add("draw five cards", DrawFive);
        actions.Add("draw six", DrawSix);
        actions.Add("draw six cards", DrawSix);
        actions.Add("draw seven", DrawSeven);
        actions.Add("draw seven cards", DrawSeven);

        //Discard
        actions.Add("discard", Discard);
        actions.Add("destroy", Discard);
        actions.Add("destroy card", Discard);
        actions.Add("discard card", Discard);

        //select cards
        actions.Add("place", SelectCard);
        actions.Add("place down", SelectCard);
        actions.Add("select", SelectCard);
        actions.Add("choose", SelectCard);
        actions.Add("swap", SelectCard);
        actions.Add("switch", SelectCard);
        actions.Add("swap cards", SelectCard);
        actions.Add("switch cards", SelectCard);

        //return card to hand
        actions.Add("return", ReturnCard);
        actions.Add("return card", ReturnCard);


        //Camera movement
        actions.Add("deck", DeckView);
        actions.Add("cards", DeckView);
        actions.Add("view deck of cards", DeckView);
        actions.Add("view deck", DeckView);
        actions.Add("view cards", DeckView);

        actions.Add("view board", BoardView);
        actions.Add("board", BoardView);
        actions.Add("playing field", BoardView);

        actions.Add("enemy", EnemyView);
        actions.Add("boss", EnemyView);

        //fighting
        actions.Add("fight", StartFight);
        actions.Add("start fight", StartFight);


        //Menus
        actions.Add("pause", PauseGame);
        actions.Add("resume", ResumeGame);
        actions.Add("main menu", BackToMainMenu);


        keywordrecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordrecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordrecognizer.Start();
    }



    #region Draw

    private void DrawOne()
    {
        ch.DrawCards(1);
    }
    private void DrawTwo()
    {
        ch.DrawCards(2);
    }

    private void DrawThree()
    {
        ch.DrawCards(3);
    }

    private void DrawFour()
    {
        ch.DrawCards(4);
    }

    private void DrawFive()
    {
        ch.DrawCards(5);
    }

    private void DrawSix()
    {
        ch.DrawCards(6);
    }

    private void DrawSeven()
    {
        ch.DrawCards(7);
    }

    #endregion

    public void Discard()
    {
        ch.DiscardCard();
    }

    public void SelectCard()
    {
        ch.SelectCard();
    }

    public void ReturnCard()
    {
        ch.ReturnCard();
    }

    private void DeckView()
    {
        cm.ChangeFocus(CameraPosition.Deck);
    }

    private void BoardView()
    {
        cm.ChangeFocus(CameraPosition.BoardView);
    }

    private void EnemyView()
    {
        cm.ChangeFocus(CameraPosition.Enemy);
    }

    private void StartFight()
    {
        bm.bs = BattleState.Start;
    }

    #region  Menu

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion


    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

}
