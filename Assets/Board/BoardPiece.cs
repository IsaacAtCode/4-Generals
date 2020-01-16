﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jesus.Cards;

public class BoardPiece : MonoBehaviour
{
    public int position;
    public CardSO cardInfo = null;
    public GameObject blankCard;
    public float offset = 2.5f;

    public GameObject cardObject;

    public void SpawnCard()
    {
        if (cardInfo)
        {
            Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);

            cardObject = Instantiate(blankCard, offsetPos, Quaternion.identity, transform);

            cardObject.transform.Rotate(180f, 180f, 0);
            cardObject.transform.localScale = new Vector3(0.1f, 1, 0.1f);

            Card card = cardObject.AddComponent<Card>();
            card.cardInfo = cardInfo;
            card.PopulateCard();
        }
        else
        {
            Debug.Log("Cannot spawn - no info");
        }
    }

    public void RemoveCard()
    {
        Destroy(cardObject);
        cardInfo = null;
    }

}
