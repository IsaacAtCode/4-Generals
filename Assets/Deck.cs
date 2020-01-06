using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jesus.Cards
{
    public class Deck : MonoBehaviour
    {
        public List<CardSO> cards;

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
    }
}
