using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

namespace Jesus.Cards
{
    //Scripts related to the left Hand
    public class CardDeck : MonoBehaviour
    {
        [Header("Deck")]
        public List<CardSO> cards;
        public List<GameObject> cardObjects;

        [Header("Hand")]
        public Hand leftHand;
        public GameObject handMiddle_L;

        private void Start()
        {
            if (cards == null)
            {
                cards = CreateDeck();
            }
        }

        public List<CardSO> CreateDeck()
        {
            List<CardSO> newDeck = new List<CardSO>();

            return newDeck;
        }

        public void AddCard(CardSO card)
        {
            cards.Add(card);
        }

        public void RemoveCard(CardSO card)
        {
            cards.Remove(card);
        }

        public void SwapCard(CardSO cardInDeck, CardSO cardInHand)
        {
            RemoveCard(cardInDeck);
            AddCard(cardInHand);
        }






    }
}
