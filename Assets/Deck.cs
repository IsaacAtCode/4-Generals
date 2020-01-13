using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jesus.Cards
{
	[ExecuteInEditMode]
	public class Deck : MonoBehaviour
	{
		public List<CardSO> cards;
		public bool createNewDeck = false;

		public int maxCards = 5;

		private void Start()
		{
			if (cards == null)
			{
				cards = CreateDeck();
			}
		}

		public List<CardSO> CreateDeck()
		{
			List<CardSO> newDeck = new List<CardSO>();

			return newDeck;
		}

		public void AddCard(CardSO card)
		{
			cards.Add(card);
		}

		public void RemoveCard(CardSO card)
		{
			cards.Remove(card);
		}
	}
}
