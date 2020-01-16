using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jesus.Cards;

public class Board : MonoBehaviour
{
	public int totalDamage = 0;
	public int totalHealth = 0;

    public int currDamage;
    public int currHealth;

	[Header("Cards")]
    public List<BoardPiece> pieces;

    public List<CardSO> cardsOnBoard;

	public CardSO testCard;

    public void PopulateBoard()
    {
        cardsOnBoard.Clear();

        totalDamage = 0;
        totalHealth = 0;

        foreach (BoardPiece item in pieces)
        {
            item.SpawnCard();
            cardsOnBoard.Add(item.cardInfo);

        }

        CalculateHealthAndDamage();
    }

    public void CalculateHealthAndDamage()
    {
        totalDamage = 0;
        totalHealth = 0;

        if (cardsOnBoard.Count > 0)
        {
            foreach (CardSO item in cardsOnBoard)
            {
                totalDamage += item.damage;
                totalHealth += item.health;
            } 
        }
    }

    private void Start()
    {
        PopulateBoard();
    }
}
