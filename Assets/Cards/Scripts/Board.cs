using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jesus.Cards;

public class Board : MonoBehaviour
{
	public int totalDamage = 0;
	public int totalHealth = 0;

	[Header("Cards")]
	public List<CardSO> cardsOnBoard;
	public GameObject[] cardObjects =  new GameObject[6];
	public GameObject blankCard;

	public CardSO testCard;

	[Header("Transforms")]
	public Transform generalTransform;
	public List<Transform> boardTransforms;

	private void Update()
	{

	}






	public void SpawnCard(CardSO cardInfo, int location)
	{
		if (cardObjects[location] == null)
		{
			GameObject cardGO = Instantiate(blankCard, boardTransforms[location].position, Quaternion.identity);
			cardObjects[location] = cardGO;

			cardGO.transform.Rotate(180f, 180f, 0);
			cardGO.transform.localScale = new Vector3(10f, 5, 10f);



			Card card = cardGO.AddComponent<Card>();
			card.cardInfo = cardInfo;
			card.PopulateCard();
		}
		//else if (cardObjects[location] != null && Card in hand)
		//{
		//	//Swap Cards
		//}

		else
		{
			Debug.Log("Already filled");
		}
		
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
