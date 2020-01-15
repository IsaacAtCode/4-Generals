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

		public GameObject anchor;
		public Transform frontSpawn;
		public Transform backSpawn;
		public Transform indexPos;

		[Header("Deck")]
		public Card selectedCard = null;
		public float selectedScale = 1.25f;
		Vector3 originalSize, tempSize;

		[Header("Card in Hand")]
		public GameObject cardInHandGO;
		public CardSO cardInHandInfo;
		public bool inHand;

		private int layer = 10;
		private int layermask = 1 << 10;

		[Header("Other")]
		public GameObject blankCard;
		public GameObject mainCamera;
		public CardSO testSO;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				SelectCard(testSO);
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				ClearHand();
			}


			//if (cardInHandGO)
			//{
			//	Vector3 targetPos = mainCamera.transform.position;

			//	cardInHandGO.transform.LookAt(targetPos);
			//	cardInHandGO.transform.Rotate(-90, 0, 0);
			//}

			GetCardFromDeck();

			if (cardInHandGO)
			{
				MoveCard();
			}
		}

		public void SelectCard(CardSO cardInfo)
		{
			if (!cardInHandGO)
			{
				cardInHandGO = Instantiate(blankCard, anchor.transform.position, Quaternion.identity);
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

		private void MoveCard()
		{
			if (inHand)
			{
				cardInHandGO.transform.parent = frontSpawn;
				cardInHandGO.transform.localRotation = Quaternion.identity;
				cardInHandGO.transform.localPosition = Vector3.zero;
				cardInHandGO.transform.localScale = Vector3.one;
			}
			else
			{
				cardInHandGO.transform.parent = backSpawn;
				cardInHandGO.transform.localRotation = Quaternion.identity;
				cardInHandGO.transform.localPosition = Vector3.zero;
				cardInHandGO.transform.localScale = Vector3.one;

			}
		}



		public void HideHand()
		{
			if (cardInHandGO)
			{
				Destroy(cardInHandGO);
			}			
		}

		public void ClearHand()
		{
			Destroy(cardInHandGO);
			cardInHandInfo = null;
		}

		public void GetCardFromDeck()
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

