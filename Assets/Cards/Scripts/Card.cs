﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	public CardSO cardInfo;

	[Header("Components")]
	public GameObject cardBase;

	public Text nameText;
	public Image cardImage;
	public Text healthText;
	public Text damageText;
	public Image buffImage;

	private void Start()
	{
		ChangeName(cardInfo.cardName);
		PopulateCard();
		ChangeRarity(cardInfo.rarity);
	}



	private void ChangeName(string newName)
	{
		name = newName;
	}

	private void PopulateCard()
	{
		nameText.text = cardInfo.cardName;
		cardImage.sprite = cardInfo.cardPicture;
		healthText.text = cardInfo.health.ToString();
		damageText.text = cardInfo.damage.ToString();
		//buffImage = 
	}

	private void EmptyCard()
	{
		nameText.text = "";
		cardImage.sprite = null;
		healthText.text = "";
		damageText.text = "";
	}

	private void ChangeRarity(Rarity rarity)
	{
		Material mat = cardBase.GetComponent<Renderer>().material;


		if (rarity == Rarity.Common)
		{
			mat.color = Color.grey;
		}
		else if (rarity == Rarity.Rare)
		{
			mat.color = Color.cyan;

		}
		else if (rarity == Rarity.SuperRare)
		{
			mat.color = Color.yellow;

		}
	}






}