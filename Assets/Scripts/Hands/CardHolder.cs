using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
	public GameObject handMiddle;
	private bool handFull;
	public GameObject cardInHand;

	public float yOffset;
	public float zOffset;

	public GameObject cardToSpawn;



	private void Start()
	{
		cardInHand = SpawnCard(handMiddle, cardToSpawn);
	}

	public GameObject SpawnCard(GameObject hand, GameObject card)
	{
		Vector3 Offset = new Vector3(0, -yOffset, -zOffset) + hand.transform.position;

		GameObject newCard =  Instantiate(card, Offset, Quaternion.identity);

		newCard.transform.parent = hand.transform;

		return newCard;

	}

	public void EmptyHand()
	{

	}

	public void ExpandCard()
	{

	}

}
