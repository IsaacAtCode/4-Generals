using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card")]
public class CardSO : ScriptableObject
{
	public string cardName;
	public Sprite cardPicture;

	public Rarity rarity;

	public int health;
	public int damage;

	public Buff buff;
}

public enum Buff
{
	NoBuff,
	HealthBoost,
	DamageBoost,
	Retaliate,
}

public enum Rarity
{
	Common,
	Rare,
	SuperRare,
}
