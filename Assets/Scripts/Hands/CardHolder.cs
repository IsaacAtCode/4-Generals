using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

namespace Jesus.Cards
{
	public class CardHolder : MonoBehaviour
	{
        [Header("Left Hand - Deck")]
        public Hand leftHand; //Deck hand
        public GameObject handMiddle_L;


        [Header("Right Hand - Selection")]
		public Hand rightHand;
		public GameObject handMiddle_R;
		private bool handFull_R;
		public GameObject cardInHand_R;
		public CardSize cardSize_R = CardSize.Medium;

        [Header("Deck")]
        public Deck deck;
        private List<GameObject> cards;
        public GameObject blankCard;

		public GameObject cardToSpawn;


        [Header("Hand Detection")]
        public Controller controller;
		public Frame frame;

        [Header("Other")]
		public Camera mainCamera;

		private void Start()
		{
			controller = new Controller();


			cardInHand_R = SpawnCard(handMiddle_R, cardToSpawn);

            deck = CreateDeck();

		}

		private void Update()
		{
			GetHand();

			if (controller.IsConnected)
			{
				frame = controller.Frame();
				Frame previous = controller.Frame(1);
			}

            float handDistance = 3 / (Vector3.Distance(handMiddle_R.transform.position, mainCamera.transform.position));
            float newDist = Mathf.Clamp(handDistance, 0.5f, 2f);
            cardInHand_R.transform.localScale = new Vector3(newDist, newDist, newDist);
        }


		private void GetHand()
		{

			foreach (Hand hand in frame.Hands)
			{
				if (hand.IsRight)
				{
					rightHand = hand;
				}
                if (hand.IsLeft)
                {
                    leftHand = hand;
                }
			}
		}

        #region Deck

        private Deck CreateDeck()
        {
            Deck newDeck = new Deck();

            RefreshDeck();

            return newDeck;
        }


        public void RefreshDeck()
        {
            //CLearDeck

            PopulateDeck(deck);
        }


        private void PopulateDeck(Deck deck)
        {
            foreach (CardSO cardInfo in deck.cards)
            {
                CreateCard(cardInfo);
            }
        }

        private void CreateCard(CardSO cardInfo)
        {
            GameObject cardGO = Instantiate(blankCard);
            Card card = cardGO.AddComponent<Card>();
            card.PopulateCard(cardInfo);

        }






        #endregion

        #region Right Hand

        public GameObject SpawnCard(GameObject anchor, GameObject card)
		{
			Vector3 Offset = anchor.transform.position;

			GameObject newCard = Instantiate(card, Offset, anchor.transform.rotation);

			newCard.transform.parent = anchor.transform;

			return newCard;
		}

		public void EmptyHand()
		{

		}

        public void ExpandCard(float size)
        {
            //float handDistance = size / (Vector3.Distance(handMiddle_R.transform.position, mainCamera.transform.position));
            //float newDist = Mathf.Clamp(handDistance, 1f, 2f);
            //cardInHand_R.transform.localScale = new Vector3(newDist, newDist, newDist);
        }

        #endregion

        #region Hand Checks

        private void CheckHandRotation()
		{
			//if (leftHandMiddle.transform.position.x)
			//{

			//}
		}

        #endregion

    }

    public enum CardSize
	{
		Small,
		Medium,
		Large,
	}

}
