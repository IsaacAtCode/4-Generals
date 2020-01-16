using System.Collections;
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

    public void SpawnCard(CardSO cardStats)
    {
        if (!cardObject)
        {

            Vector3 offsetPos = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);

            cardObject = Instantiate(blankCard, offsetPos, Quaternion.identity, transform);

            cardObject.transform.Rotate(180f, 180f, 0);
            cardObject.transform.localScale = new Vector3(0.1f, 1, 0.1f);

            Card card = cardObject.AddComponent<Card>();
            card.cardInfo = cardStats;
            cardInfo = cardStats;
            card.PopulateCard();

            Debug.Log("Cannot spawn - no info");

        }
        else
        {
            Debug.Log("Cannot spawn - card in front");
        }
    }

    public void RemoveCard()
    {
        Destroy(cardObject);
        cardObject = null;
        cardInfo = null;
    }

}
