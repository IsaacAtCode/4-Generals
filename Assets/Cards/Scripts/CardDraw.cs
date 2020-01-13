using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jesus.Cards
{
    public class CardDraw : MonoBehaviour
    {
        public List<CardSO> commonCards;
        public List<CardSO> rareCards;
        public List<CardSO> superRareCards;
        public int count = 20;

        private int commonCount = 0;
        private int rateCount = 0;
        private int superRareCount = 0;


        public List<CardSO> pile;

        private void Start()
        {
            pile = GenerateDrawPile();
        }


        private List<CardSO> GenerateDrawPile()
        {
            List<CardSO> cards = new List<CardSO>();

            for (int i = 0; i < count; i++)
            {
                int randomNumber = Random.Range(0, 100);

                if (randomNumber < 60)
                {
                    cards.Add(AddCard(Rarity.Common));
                }
                else if (randomNumber >= 60 && randomNumber < 90)
                {
                    cards.Add(AddCard(Rarity.Rare));
                }
                else if (randomNumber >= 90)
                {
                    cards.Add(AddCard(Rarity.SuperRare));
                }
            }


            return cards;
        }

        private CardSO AddCard(Rarity rarity)
        {
            CardSO cardToAdd;

            if (rarity == Rarity.SuperRare)
            {
                cardToAdd = superRareCards[Random.Range(0, superRareCards.Count)];
            }
            else if (rarity == Rarity.Rare)
            {
                cardToAdd = rareCards[Random.Range(0, rareCards.Count)];
            }
            else if (rarity == Rarity.Common)
            {
                cardToAdd = commonCards[Random.Range(0, commonCards.Count)];
            }
            else
            {
                cardToAdd = commonCards[Random.Range(0, commonCards.Count)];
            }
            return cardToAdd;
        }
    }


}
