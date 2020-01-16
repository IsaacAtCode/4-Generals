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

	public CardSO testCard;

    public void PopulateBoard()
    {
        cardsOnBoard.Clear();

        foreach (BoardPiece item in pieces)
        {
            item.SpawnCard();
            cardsOnBoard.Add(item.cardInfo);
        }

        totalDamage = AttackDamage();
        totalHealth = HealthBlock();
    }



	public int AttackDamage()
	{
		int damage = 0;

		foreach (CardSO card in cardsOnBoard)
		{
			damage += card.damage;
		}

		return damage;
	}

	public int HealthBlock()
	{
		int health = 0;

		foreach (CardSO card in cardsOnBoard)
		{
			health += card.health;
		}

		return health;
	}

}
