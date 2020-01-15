﻿using System.Collections;
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

		[HideInInspector]
		public CameraMove cm;

		public Controller controller;
		public Frame frame;

		private void Start()
		{
			controller = new Controller();

			cm = GetComponent<CameraMove>();
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
			ShowHand();
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
			if (hand.GrabStrength < 0.6f)
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
			if (hand.PalmNormal.y > 0.5 || hand.PalmNormal.z > 0f)
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
			if (HandDirection(selectionHand.hand) == Facing.Up && OpenPalm(selectionHand.hand))
			{
				selectionHand.inHand = true;
			}
			else if (HandDirection(selectionHand.hand) == Facing.Down)
			{
				selectionHand.inHand = false;
			}
			else if (!OpenPalm(selectionHand.hand))
			{
				selectionHand.inHand = false;
			}



			if (selectionHand.cardInHandInfo && !selectionHand.cardInHandGO) //If there was a card in the hand, but not the game object
			{
					selectionHand.SelectCard(selectionHand.cardInHandInfo);
			}

		}




	}

	public enum Facing
	{
		Up,
		Down,
	}

}
