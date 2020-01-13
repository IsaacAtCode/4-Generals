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
        private int layer = 10;
        private int layermask = 1 << 10;

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


            SelectCardFromDeck();
             
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

        public void SelectCardFromDeck()
        {
            //RaycastHit hit;


            //Debug.DrawRay(selectionHand.indexPos.position, selectionHand.indexPos.forward, Color.red);

            //if (Physics.Raycast(selectionHand.indexPos.position, selectionHand.indexPos.forward, out hit, Mathf.Infinity, ~layermask) && hit.transform.tag == "Card")
            //{
            //    Debug.Log(hit.transform.tag);

            //    selectedCard = hit.transform.gameObject.GetComponent<Card>();
            //    if (selectedCard.inDeck)
            //    {
            //        selectedCard.ScaleCard(1f);
            //    }
            //}

        }




    }

}
