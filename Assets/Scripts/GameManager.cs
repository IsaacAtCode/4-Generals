using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Rounds
    public int roundCount = 0;
    //Turns
    public int maxTurns = 3;
    public int currentTurns;

    private void Start()
    {
        currentTurns = maxTurns;
    }

    #region Rounds



    private void NextRound()
    {
        IncreaseMaxTurns(1);
        currentTurns = maxTurns;
    }


    #endregion 


    #region Turns
    public void IncreaseMaxTurns(int count)
    {
        maxTurns += count;
    }

    public void AddTurn(int count)
    {
        currentTurns += count;
    }

    public void RemoveTurn(int count)
    {
        currentTurns -= count;
    }
    #endregion



}

