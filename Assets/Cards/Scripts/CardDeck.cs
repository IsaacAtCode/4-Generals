using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Jesus.Cards;

namespace Jesus.Hands
{
	//Scripts related to the left Hand
	public class CardDeck : MonoBehaviour
	{
		[Header("Deck")]
		public List<CardSO> cards;
		public List<GameObject> cardObjects;
		private GameObject playerDeck; // Parent Object
		public int maxCards = 6;

		[Header("Hand")]
		public Hand hand;
        public GameObject handGO;
		public GameObject otherHand;
		public GameObject anchor;

		[Header("Draw")]
		public List<CardSO> commonCards;
		public List<CardSO> rareCards;
		public List<CardSO> superRareCards;
		public int count = 20;

		public List<CardSO> drawPile;


		[Header("Other")]
		public GameObject mainCamera;
		public GameObject blankCard;

		private bool isDirty;


		private void Start()
		{
			if (cards == null)
			{
				cards = CreateDeck();
			}

			drawPile = GenerateDrawPile();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.L))
			{
				DrawCard();
			}

			if (Input.GetKeyDown(KeyCode.K))
			{
				RemoveCard(0);
			}

			if (isDirty)
			{
				RefreshDeck();
			}

			RotateCards();
		}

		#region Deck

		public List<CardSO> CreateDeck()
		{
			List<CardSO> newDeck = new List<CardSO>();

			return newDeck;
		}

		public void AddCard(CardSO card)
		{
			cards.Add(card);
			isDirty = true;
		}

		public void RemoveCard(CardSO card)
		{
			if (cards.Count >= 1)
			{
				cards.Remove(card);
				isDirty = true;
			}
			else if (cards.Count <= 0)
			{
				Debug.Log("No Cards to remove");
			}
		}

		public void RemoveCard(int number)
		{
			if (cards.Count >= 1)
			{
				cards.RemoveAt(number);
				isDirty = true;
			}
			else if (cards.Count <= 0)
			{
				Debug.Log("No Cards to remove");
			}
		}

		public void SwapCard(CardSO cardInDeck, CardSO cardInHand)
		{
			RemoveCard(cardInDeck);
			AddCard(cardInHand);
			isDirty = true;
		}

		public void DrawCard()
		{
			if (drawPile.Count > 0)
			{
				if (cards.Count <= maxCards - 1)
				{
					CardSO drawnCard = drawPile[Random.Range(0, drawPile.Count)];
					drawPile.Remove(drawnCard);
					AddCard(drawnCard);
				}
				else
				{
					Debug.Log("Cannot draw any more");
				}
			}
			else
			{
				Debug.Log("Drawn All Cards");
			}
		}


		#endregion

		#region Game Objects

		public void ShowDeck()
		{
			anchor.SetActive(true);
			cardObjects = GenerateCards();
			SeperateCards();
		}

		public void HideDeck()
		{
			Destroy(playerDeck);
			anchor.SetActive(false);
		}

		public void RefreshDeck()
		{
			Destroy(playerDeck);
			cardObjects = GenerateCards();
			SeperateCards();
		}

		public List<GameObject> GenerateCards()
		{
			List<GameObject> cardGOs = new List<GameObject>();
			playerDeck = new GameObject("Deck");
			playerDeck.transform.parent = anchor.transform;
			playerDeck.transform.position = anchor.transform.position;

			Vector3 parentVector = new Vector3(playerDeck.transform.position.x, playerDeck.transform.position.y, playerDeck.transform.position.z);

			foreach (CardSO cardInfo in cards)
			{
				GameObject cardGO = Instantiate(blankCard, parentVector, Quaternion.identity, playerDeck.transform);
				cardGO.tag = "Card";
				cardGOs.Add(cardGO);
				cardGO.transform.localScale = new Vector3(0.75f, 0.75f * 0.1f, 0.75f);

				Card card = cardGO.AddComponent<Card>();
				card.cardInfo = cardInfo;
				card.PopulateCard();
				card.inDeck = true;
			}

			return cardGOs;
		}

		private void SeperateCards()
		{
			float widthOfAllCards = 0;

			foreach (GameObject card in cardObjects)
			{
				widthOfAllCards += card.transform.localScale.x;
			}

			float averageWidth = widthOfAllCards / cardObjects.Count;
			float padding = averageWidth * 0.25f;
			float totalWidth = widthOfAllCards + (cardObjects.Count) * padding;

			for (int i = 0; i < cards.Count; i++)
			{
				float placement = i * (totalWidth / cardObjects.Count) - totalWidth / 2;
				cardObjects[i].transform.position += new Vector3(placement, 0, 0);
			}
		}

		private void RotateCards()
		{
            Vector3 otherHandPos = new Vector3 (otherHand.transform.position.x, otherHand.transform.position.y, otherHand.transform.position.z);
            Vector3 camPos = mainCamera.transform.position;


            if (playerDeck !=  null)
            {
                if (otherHand.activeInHierarchy == true)
                {
                    playerDeck.transform.LookAt(otherHandPos);

                }
                else
                {
                    playerDeck.transform.LookAt(camPos);
                }  
            }


           

            foreach (GameObject card in cardObjects)
			{
				card.transform.LookAt(camPos);
				card.transform.Rotate(-90, 0, 0);
			}
		}

		public void ShrinkCards()
		{
			foreach (GameObject card in cardObjects)
			{
				card.GetComponent<Card>().ScaleCard(0.75f);
			}
		}


		private bool isDeckShown()
		{
			if (anchor.activeSelf)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region Draw Pile
		public List<CardSO> GenerateDrawPile()
		{
			List<CardSO> cards = new List<CardSO>();

			for (int i = 0; i < count; i++)
			{
				int randomNumber = Random.Range(0, 100);

				if (randomNumber < 60)
				{
					cards.Add(AddCard(Rarity.Common));
				}
				else if (randomNumber >= 60 && randomNumber < 90)
				{
					cards.Add(AddCard(Rarity.Rare));
				}
				else if (randomNumber >= 90)
				{
					cards.Add(AddCard(Rarity.SuperRare));
				}
			}


			return cards;
		}

		private CardSO AddCard(Rarity rarity)
		{
			CardSO cardToAdd;

			if (rarity == Rarity.SuperRare)
			{
				cardToAdd = superRareCards[Random.Range(0, superRareCards.Count)];
			}
			else if (rarity == Rarity.Rare)
			{
				cardToAdd = rareCards[Random.Range(0, rareCards.Count)];
			}
			else if (rarity == Rarity.Common)
			{
				cardToAdd = commonCards[Random.Range(0, commonCards.Count)];
			}
			else
			{
				cardToAdd = commonCards[Random.Range(0, commonCards.Count)];
			}
			return cardToAdd;
		}

		public CardSO RandomCard()
		{
			Rarity rarity = (Rarity)Random.Range(0, 2);
			return AddCard(rarity);
		}

		#endregion


	}
}
