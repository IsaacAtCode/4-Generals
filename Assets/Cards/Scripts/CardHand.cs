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
		public Transform indexPos;

		[Header("Deck")]
		public Card selectedCard;
		public Color highlightColor;
		Material originalMaterial, tempMaterial;
		Renderer rend = null;

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
			if (Input.GetKeyDown(KeyCode.O))
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
				cardInHandGO = Instantiate(blankCard, anchor.transform.position, Quaternion.identity, anchor.transform);
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
			Renderer currRend;

			Debug.DrawRay(indexPos.position, indexPos.forward, Color.red);

			if (Physics.Raycast(indexPos.position, indexPos.forward, out hit, Mathf.Infinity) && hit.transform.CompareTag("Card"))
			{

				Debug.Log(hit.transform.gameObject.name);

				currRend = hit.transform.GetComponentInChildren<Renderer>();

				if (currRend == rend)
				{
					return;
				}

				if (currRend && currRend != rend)
				{
					if (rend)
					{
						rend.sharedMaterial = originalMaterial;
					}
				}

				if (currRend)
					rend = currRend;
				else
					return;

				originalMaterial = rend.sharedMaterial;

				tempMaterial = new Material(originalMaterial);
				rend.material = tempMaterial;
				rend.material.color = highlightColor;
			}
			else
			{
				if (rend)
				{
					rend.sharedMaterial = originalMaterial;
					rend = null;
				}
			}

		}
	}
}

