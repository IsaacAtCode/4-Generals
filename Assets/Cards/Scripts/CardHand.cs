using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

namespace Jesus.Cards
{
    //Scripts related to the right Hand
    public class CardHand : MonoBehaviour
    {
        public Hand hand;
        public GameObject anchor;
        private bool isHandFull;

        [Header("Other")]
        public GameObject blankCard;

        private void RightHandRayCast()
        {

        }


        public void SelectCardFromDeck()
        {
            if (isHandFull)
            {
                //Swap Card
            }
            else if (!isHandFull)
            {
                //SelectCard
            }
        }


        public void SpawnCard(CardSO cardInfo)
        {
            if (!isHandFull)
            {
                GameObject cardGO = Instantiate(blankCard, anchor.transform.position, Quaternion.identity, anchor.transform);
                cardGO.name = cardInfo.name;
                Card card = cardGO.AddComponent<Card>();
                card.PopulateCard(cardInfo);
                card.inDeck = true;

                isHandFull = true;
            }
            else
            {
                Debug.Log("Hand Full");
            }
        }

        public void EmptyHand()
        {

        }

    }
}
