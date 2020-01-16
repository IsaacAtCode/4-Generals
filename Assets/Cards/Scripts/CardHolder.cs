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

        //Board
        public Board board;

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

            if (cm.camPos == CameraPosition.Deck)
            {
                selectionHand.DeckSelection();
            }
            else if (cm.camPos == CameraPosition.BoardView)
            {
                selectionHand.BoardSelection();
            }




		}

        //Select Card
        public void SelectCard() //Goes into hand
        {
            if (cm.camPos == CameraPosition.Deck)
            {
                if (selectionHand.cardInHandInfo) //If card is in the hand, swap instead
                {
                    SwapCardToDeck(selectionHand.cardInHandInfo, selectionHand.selectedCard.cardInfo);
                }
                else
                {
                    selectionHand.ClearHand();
                    deckHand.RemoveCard(selectionHand.selectedCard.cardInfo);
                    selectionHand.SelectCard(selectionHand.selectedCard.cardInfo);
                }
            }
            else if (cm.camPos == CameraPosition.BoardView)
            {
                if (selectionHand.cardInHandInfo) //If card is in the hand, swap instead
                {
                    SwapCardToBoard(selectionHand.cardInHandInfo, selectionHand.selectedBoard.cardInfo);

                    board.PopulateBoard();
                }
                else if (!selectionHand.cardInHandInfo)
                {
                    selectionHand.ClearHand();
                    //deckHand.RemoveCard(selectionHand.selectedCard.cardInfo);
                    selectionHand.SelectCard(selectionHand.selectedBoard.cardInfo);

                    board.PopulateBoard();
                }
            }

        }


            //Swap Card
        public void SwapCardToDeck(CardSO hand, CardSO deck)
        {
            selectionHand.ClearHand();

            deckHand.RemoveCard(deck);

            selectionHand.SelectCard(deck);

            deckHand.AddCard(hand);
        }

        public void SwapCardToBoard(CardSO hand, CardSO board)
        {
            selectionHand.ClearHand();

            selectionHand.selectedBoard.RemoveCard();

            selectionHand.SelectCard(board);

            deckHand.AddCard(hand);
        }

        public void ReturnCard()
        {
            deckHand.AddCard(selectionHand.cardInHandInfo);
            selectionHand.ClearHand();
        }

        public void DrawCards(int count)
        {
            deckHand.DrawCards(count);
        }

        public void DiscardCard()
        {
            if (selectionHand.cardInHandInfo)
            {
                selectionHand.ClearHand();
            }
            else if (!selectionHand.cardInHandInfo)
            {
                if (cm.camPos == CameraPosition.Deck)
                {
                    deckHand.RemoveCard(selectionHand.selectedCard.cardInfo);

                }
                else if (cm.camPos == CameraPosition.BoardView)
                {
                    selectionHand.selectedBoard.RemoveCard();
                }
            }
            else
            {
                Debug.Log("No card selected");
            }
        }

        //Discard Card






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
