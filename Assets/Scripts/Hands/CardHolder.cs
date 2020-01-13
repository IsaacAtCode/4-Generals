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
		public List<GameObject> cards;
		private GameObject playerDeck;
		public GameObject blankCard;
		public float widthOfHand;

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

			if (deck == null)
			{
				deck = CreateDeck();
			}

			ShowDeck();



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



			if (isDeckShown())
			{
			   LookAtPlayer();
			}





			if (Input.GetKeyDown(KeyCode.O))
			{
				if (isDeckShown())
				{
					HideDeck();
				}
				else if (!isDeckShown())
				{
					ShowDeck();
				}

			}


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
			return newDeck;
		}

		public void ShowDeck()
		{
			handMiddle_L.SetActive(true);
			cards = GenerateCards(deck);
			SeperateCards(widthOfHand);
		}

		public void HideDeck()
		{
			Destroy(playerDeck);
			handMiddle_L.SetActive(false);
		}

		public void RefreshDeck()
		{
			Destroy(playerDeck);
			cards = GenerateCards(deck);
			SeperateCards(widthOfHand);
		}


		public List<GameObject> GenerateCards(Deck deck)
		{

			List<GameObject> cardGOs = new List<GameObject>();
			playerDeck = new GameObject("Deck");
			playerDeck.transform.parent = handMiddle_L.transform;
			playerDeck.transform.position = handMiddle_L.transform.position;

			Vector3 parentVector = new Vector3(playerDeck.transform.position.x, playerDeck.transform.position.y, playerDeck.transform.position.z);


			foreach (CardSO cardInfo in deck.cards)
			{
				GameObject cardGO = Instantiate(blankCard, parentVector, Quaternion.identity, playerDeck.transform);
				cardGOs.Add(cardGO);
				cardGO.transform.localScale = new Vector3(0.75f, 0.75f * 0.1f, 0.75f);

				Card card = cardGO.AddComponent<Card>();
				card.PopulateCard(cardInfo);
			}

			return cardGOs;
		}

		//private void SeperateCards()
		//{

		//    int cardsToSpawn = cards.Count;
		//    float widthOfAllCards = 0;

		//    foreach (GameObject card in cards)
		//    {
		//        widthOfAllCards += card.transform.localScale.x;
		//    }

		//    float averageWidth = widthOfAllCards / cards.Count;
		//    float padding = averageWidth * 0.25f;
		//    float totalWidth = widthOfAllCards + (cards.Count) * padding;

		//    for (int i = 0; i < cards.Count; i++)
		//    {
		//        float placement = i * (totalWidth / cards.Count) - totalWidth / 2;
		//        cards[i].transform.position += new Vector3(placement, 0, 0);
		//    }
		//}

		private void SeperateCards(float maxWidth)
		{
			float widthOfCard = (4 * maxWidth) / (5 * cards.Count);

			foreach (GameObject card in cards)
			{
				card.transform.localScale = new Vector3(widthOfCard, widthOfCard * 0.1f, widthOfCard);
			}

			for (int i = 0; i < cards.Count; i++)
			{
				float placement = i * (maxWidth / cards.Count) - maxWidth / 2;
				cards[i].transform.position += new Vector3(placement, 0, 0);
			}
		}
	
		private void LookAtPlayer()
		{
			foreach (GameObject card in cards)
			{
				Vector3 camPos = mainCamera.transform.position;
				Vector3 lookVector = camPos - card.transform.position;
				lookVector.y = card.transform.position.y + 5;
				Quaternion rot = Quaternion.LookRotation(lookVector);
				card.transform.rotation = Quaternion.Slerp(card.transform.rotation, rot, 1);
			}
		}

		private bool isDeckShown()
		{
			if (handMiddle_L.activeSelf)
			{
				return true;
			}
			else
			{
				return false;
			}
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
