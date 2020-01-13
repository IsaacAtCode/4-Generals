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
        public GameObject anchor;
        public Transform indexPos;

        [Header("Card in Hand")]
        public GameObject cardInHandGO;
        public CardSO cardInHandInfo;

        [Header("Other")]
        public GameObject blankCard;
        public GameObject mainCamera;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                SpawnCard(GetComponent<CardHolder>().selectedCard.cardInfo);
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


    }
}
