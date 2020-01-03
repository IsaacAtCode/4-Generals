using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

namespace Jesus.Cards
{
	public class CardHolder : MonoBehaviour
	{
		public Hand rightHand;


		public GameObject handMiddle;
		private bool handFull;
		public GameObject cardInHand;

		public CardSize cardSize = CardSize.Medium;

		public GameObject cardToSpawn;

		public Controller controller;

		public Frame frame;

		public Camera mainCamera;

		private void Start()
		{
			controller = new Controller();


			cardInHand = SpawnCard(handMiddle, cardToSpawn);

			 
			

		}

		private void Update()
		{
			GetHand();

			if (controller.IsConnected)
			{
				frame = controller.Frame();
				Frame previous = controller.Frame(1);
			}




			float handDistance = 3 /( Vector3.Distance(handMiddle.transform.position, mainCamera.transform.position)) ;
			float newDist = Mathf.Clamp(handDistance, 1f, 2f);
			cardInHand.transform.localScale = new Vector3(newDist,newDist,newDist);

		}


		private void GetHand()
		{

			foreach (Hand hand in frame.Hands)
			{
				if (hand.IsRight)
				{
					rightHand = hand;
				}
			}

			if (rightHand)
			{
				cardInHand.SetActive(false);
			}
			else
			{
				cardInHand.SetActive(true);

			}

		}



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

		private void CheckHandRotation()
		{
			//if (handMiddle.transform.position.x)
			//{

			//}
		}

		public void ExpandCard(float size)
		{
			cardInHand.transform.localScale *= size;
		}
	}

	public enum CardSize
	{
		Small,
		Medium,
		Large,
	}

}
