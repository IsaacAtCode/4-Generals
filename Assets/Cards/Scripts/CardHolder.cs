using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

namespace Jesus.Cards
{
    [RequireComponent(typeof(CardDeck))]
    [RequireComponent(typeof(CardHand))]
	public class CardHolder : MonoBehaviour
	{
        //Left Hand
        public CardDeck leftHand;

        //Right Hand
        public CardHand rightHand;

        [Header("Hand Detection")]
		public Controller controller;
		public Frame frame;

		[Header("Other")]
        public GameObject blankCard;
        public Camera mainCamera;

		private void Start()
		{
			controller = new Controller();

            leftHand = GetComponent<CardDeck>();
            rightHand = GetComponent<CardHand>();
		}

		private void Update()
		{
			GetHand();

            if (controller.IsConnected)
            {
                frame = controller.Frame();
                Frame previous = controller.Frame(1);
            }


		}


		private void GetHand()
		{
			foreach (Hand hand in frame.Hands)
			{
                if (hand.IsRight)
                {
                    rightHand.hand = hand;
                }
				if (hand.IsLeft)
				{
                    leftHand.hand = hand;
				}
			}
		}

	}

}
