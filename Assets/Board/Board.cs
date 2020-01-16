using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jesus.Cards;

public class Board : MonoBehaviour
{
	public int totalDamage = 0;
	public int totalHealth = 0;

	[Header("Cards")]
    public List<BoardPiece> pieces;

    public List<CardSO> cardsOnBoard;

    public void PopulateBoard()
    {
        cardsOnBoard.Clear(); 

        totalDamage = 0;
        totalHealth = 0;

        foreach (BoardPiece item in pieces)
        {
            if (item.cardInfo)
            {
                item.SpawnCard(item.cardInfo);
                cardsOnBoard.Add(item.cardInfo);
                totalDamage += item.cardInfo.damage;
                totalHealth += item.cardInfo.health;
            }
        }
    }



    private void Start()
    {
        PopulateBoard();
    }
}
