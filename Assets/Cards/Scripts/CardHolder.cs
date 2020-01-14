using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using Jesus.Cards;

namespace Jesus.Hands
{
	[RequireComponent(typeof(CardDeck))]
	[RequireComponent(typeof(CardHand))]
	public class CardHolder : MonoBehaviour
	{
		//Left Hand
		public CardDeck deckHand;

		//Right Hand
		public CardHand selectionHand;
		public Card selectedCard;

		public Controller controller;
		public Frame frame;

		private void Start()
		{
			controller = new Controller();

			deckHand = GetComponent<CardDeck>();
			selectionHand = GetComponent<CardHand>();

			deckHand.otherHand = selectionHand.anchor;
			selectionHand.otherHand = deckHand.anchor;
		}

		private void Update()
		{
			GetHand();

			if (controller.IsConnected)
			{
				frame = controller.Frame();
				Frame previous = controller.Frame(1);
			}

			if (Input.GetKeyDown(KeyCode.H) && selectionHand.cardInHandGO != null)
			{
				SwapCards(selectedCard.cardInfo);
			}			 
		}


		private void GetHand()
		{
			foreach (Hand hand in frame.Hands)
			{
				if (hand.IsLeft)
				{
					deckHand.hand = hand;
				}
				if (hand.IsRight)
				{
					selectionHand.hand = hand;
				}
			}
		}

		//AddToDeck

		//AddtoHand

		public void SwapCards(CardSO cardInDeck)
		{
			CardSO cardInHand = selectionHand.cardInHandInfo;

			selectionHand.EmptyHand();
			deckHand.RemoveCard(cardInDeck);

			selectionHand.SpawnCard(cardInDeck);
			deckHand.AddCard(cardInHand);
		}

		




	}

}
