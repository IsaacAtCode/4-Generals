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
		[HideInInspector]
		public CardDeck deckHand;

		//Right Hand
		[HideInInspector]
		public CardHand selectionHand;

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

			ShowDeck();
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

		private bool OpenPalm(Hand hand)
		{
			if (hand.GrabStrength < 0.5)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private Facing HandDirection(Hand hand)
		{
			if (hand.PalmNormal.y > 0.5)
			{
				return Facing.Up;
			}
			else
			{
				return Facing.Down;
			}
		}

		private void ShowDeck()
		{
			if (OpenPalm(deckHand.hand) == true && HandDirection(deckHand.hand) == Facing.Up)
			{
				if (deckHand.playerDeck == null)
				{
					deckHand.ShowDeck();
				}
			}
			else if (!OpenPalm(deckHand.hand) || HandDirection(deckHand.hand) == Facing.Down)
			{
				deckHand.HideDeck();
			}

		}

		private void ShowHand()
		{
			if (OpenPalm(selectionHand.hand) == true && HandDirection(selectionHand.hand) == Facing.Up)
			{
				if (selectionHand.cardInHandGO == null)
				{
					selectionHand.SpawnCard(selectionHand.cardInHandInfo);
				}
			}
			else if (!OpenPalm(selectionHand.hand) || HandDirection(selectionHand.hand) == Facing.Down)
			{
				selectionHand.EmptyHand();
			}

		}


		//AddToDeck

		//AddtoHand

		//public void SwapCards()
		//{
		//	CardSO cardInHand = selectionHand.cardInHandInfo;

		//	selectionHand.EmptyHand();
		//	deckHand.RemoveCard(selectionHand.selectedCard.cardInfo);

		//	selectionHand.SpawnCard(selectionHand.selectedCard.cardInfo);
		//	deckHand.AddCard(cardInHand);
		//}
	}

	public enum Facing
	{
		Up,
		Down,
	}




}
