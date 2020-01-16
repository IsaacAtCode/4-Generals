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
        int deckLayerMask;

        [Header("Board")]
        public BoardPiece selectedBoard = null;
        public Color highlightColor;
        Material originalMaterial, tempMaterial;
        public Renderer rend;
        int boardLayerMask;



        [Header("Card in Hand")]
		public GameObject cardInHandGO;
		public CardSO cardInHandInfo;
		public bool inHand;

		[Header("Other")]
		public GameObject blankCard;
		public GameObject mainCamera;
		public CardSO testSO;

        private void Start()
        {
            deckLayerMask = LayerMask.GetMask("Cards");
            boardLayerMask = LayerMask.GetMask("Board");
        }


        private void Update()
		{
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

		public void DeckSelection()
		{
			RaycastHit hit;
			Card currCard;

			if (Physics.Raycast(indexPos.position, indexPos.forward, out hit, Mathf.Infinity, deckLayerMask) && hit.transform.CompareTag("Card"))
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

        public void BoardSelection()
        {
            RaycastHit hit;

            Renderer currRend;

            Debug.DrawRay(indexPos.position, indexPos.forward * 60f, Color.blue);

            if (Physics.Raycast(indexPos.position, indexPos.forward, out hit, 60f, boardLayerMask) && hit.transform.CompareTag("Board"))
            {

                currRend = hit.collider.gameObject.GetComponent<Renderer>();

                if (currRend == rend)
                    return;

                if (currRend && currRend != rend)
                {
                    if (rend)
                    {
                        rend.sharedMaterial = originalMaterial;
                    }

                }

                if (currRend)
                {
                    rend = currRend;
                }
                else
                {
                    return;
                }

                originalMaterial = rend.sharedMaterial;

                tempMaterial = new Material(originalMaterial);
                rend.material = tempMaterial;
                rend.material.color = highlightColor;

                selectedBoard = rend.GetComponent<BoardPiece>();
            }
            else
            {
                if (rend)
                {
                    rend.sharedMaterial = originalMaterial;
                    rend = null;
                    selectedBoard = null;
                }
            }
        }
    }
}

