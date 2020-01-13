﻿using System.Collections;
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
        private GameObject playerDeck; // Parent Object
        public int maxCards = 6;

        [Header("Hand")]
        public Hand hand;
        public GameObject anchor;

        [Header("Draw")]
        private List<CardSO> drawPile;


        [Header("Other")]
        public GameObject mainCamera;
        public GameObject blankCard;

        private bool isDirty;


        private void Start()
        {
            if (cards == null)
            {
                cards = CreateDeck();
            }

            drawPile = GetComponent<CardDraw>().pile;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                DrawCard();
            }

            if (isDirty)
            {
                RefreshDeck();
            }

            RotateCards();
        }

        #region Deck

        public List<CardSO> CreateDeck()
        {
            List<CardSO> newDeck = new List<CardSO>();

            return newDeck;
        }

        public void AddCard(CardSO card)
        {
            cards.Add(card);
            isDirty = true;
        }

        public void RemoveCard(CardSO card)
        {
            cards.Remove(card);
            isDirty = true;
        }

        public void SwapCard(CardSO cardInDeck, CardSO cardInHand)
        {
            RemoveCard(cardInDeck);
            AddCard(cardInHand);
            isDirty = true;
        }

        public void DrawCard()
        {
            if (cards.Count <= maxCards - 1)
            {
                CardSO drawnCard = drawPile[Random.Range(0, drawPile.Count)];
                drawPile.Remove(drawnCard);
                AddCard(drawnCard);
            }
            else
            {
                Debug.Log("Cannot draw any more");
            }
        }


        #endregion

        #region Game Objects

        public void ShowDeck()
        {
            anchor.SetActive(true);
            cardObjects = GenerateCards();
            SeperateCards();
        }

        public void HideDeck()
        {
            Destroy(playerDeck);
            anchor.SetActive(false);
        }

        public void RefreshDeck()
        {
            Destroy(playerDeck);
            cardObjects = GenerateCards();
            SeperateCards();
        }

        public List<GameObject> GenerateCards()
        {

            List<GameObject> cardGOs = new List<GameObject>();
            playerDeck = new GameObject("Deck");
            playerDeck.transform.parent = anchor.transform;
            playerDeck.transform.position = anchor.transform.position;

            Vector3 parentVector = new Vector3(playerDeck.transform.position.x, playerDeck.transform.position.y, playerDeck.transform.position.z);

            foreach (CardSO cardInfo in cards)
            {
                GameObject cardGO = Instantiate(blankCard, parentVector, Quaternion.identity, playerDeck.transform);
                cardGOs.Add(cardGO);
                cardGO.transform.localScale = new Vector3(0.75f, 0.75f * 0.1f, 0.75f);

                Card card = cardGO.AddComponent<Card>();
                card.PopulateCard(cardInfo);
                card.inDeck = true;
            }

            return cardGOs;
        }

        private void SeperateCards()
        {
            int cardsToSpawn = cards.Count;
            float widthOfAllCards = 0;

            foreach (GameObject card in cardObjects)
            {
                widthOfAllCards += card.transform.localScale.x;
            }

            float averageWidth = widthOfAllCards / cardObjects.Count;
            float padding = averageWidth * 0.25f;
            float totalWidth = widthOfAllCards + (cardObjects.Count) * padding;

            for (int i = 0; i < cards.Count; i++)
            {
                float placement = i * (totalWidth / cardObjects.Count) - totalWidth / 2;
                cardObjects[i].transform.position += new Vector3(placement, 0, 0);
            }
        }

        private void RotateCards()
        {
            foreach (GameObject card in cardObjects)
            {
                Vector3 targetPos = mainCamera.transform.position;


                card.transform.LookAt(targetPos);
                card.transform.Rotate(-90, 0, 0);
            }
        }




        private bool isDeckShown()
        {
            if (anchor.activeSelf)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion




    }
}
