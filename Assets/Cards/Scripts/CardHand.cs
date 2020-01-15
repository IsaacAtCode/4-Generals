using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Jesus.Cards;

namespace Jesus.Hands
{
	//Scripts related to the right Hand
	public class CardHand : MonoBehaviour
	{
		public Hand hand;
		public GameObject otherHand;


		public GameObject frontAnchor;
		public GameObject backAnchor;
		public Transform indexPos;

		[Header("Deck")]
		public Card selectedCard = null;
		public float selectedScale = 1.25f;
		Vector3 originalSize, tempSize;

		[Header("Card in Hand")]
		public GameObject cardInHandGO;
		public CardSO cardInHandInfo;

		private int layer = 10;
		private int layermask = 1 << 10;

		[Header("Other")]
		public GameObject blankCard;
		public GameObject mainCamera;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				SpawnCard(selectedCard.cardInfo);
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				EmptyHand();
			}


			if (cardInHandGO)
			{
				Vector3 targetPos = mainCamera.transform.position;

				cardInHandGO.transform.LookAt(targetPos);
				cardInHandGO.transform.Rotate(-90, 0, 0);
			}

			SelectCardFromDeck();

		}

		public void SpawnCard(CardSO cardInfo)
		{
			if (!cardInHandGO)
			{
				cardInHandGO = Instantiate(blankCard, frontAnchor.transform.position, Quaternion.identity, frontAnchor.transform);
				cardInHandGO.name = cardInfo.name;
				cardInHandGO.tag = "Card";
				cardInHandGO.layer = 10;
				Card card = cardInHandGO.AddComponent<Card>();
				card.cardInfo = cardInfo;
				card.PopulateCard();

				cardInHandInfo = cardInfo;
			}
			else
			{
				Debug.Log("Hand Full");
			}
		}

		public void EmptyHand()
		{
			if (cardInHandGO)
			{
				Destroy(cardInHandGO);
			}
			else
			{
				Debug.Log("Nothing to destroy");
			}
			
		}

		public void SelectCardFromDeck()
		{
			RaycastHit hit;
			Card currCard;

			Debug.DrawRay(indexPos.position, indexPos.forward, Color.red);

			if (Physics.Raycast(indexPos.position, indexPos.forward, out hit, Mathf.Infinity) && hit.transform.CompareTag("Card"))
			{
				currCard = hit.transform.gameObject.GetComponent<Card>();

				if (currCard == selectedCard)
				{
					return;
				}

				if (currCard && currCard != selectedCard)
				{
					if (selectedCard)
					{
						selectedCard.transform.localScale = originalSize;
					}
				}

				if (currCard)
				{
					selectedCard = currCard;
				}
				else
				{
					return;
				}

				originalSize = selectedCard.transform.localScale;

				tempSize = new Vector3(selectedScale,selectedScale,selectedScale);
				selectedCard.transform.localScale = tempSize;
			}
			else
			{
				if (selectedCard)
				{
					selectedCard.transform.localScale = originalSize;
					selectedCard = null;
				}
			}


		}

	}
}

