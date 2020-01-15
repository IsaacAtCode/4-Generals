using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jesus.Cards
{
	[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card")]
	public class CardSO : ScriptableObject
	{
		public new string name;
		public string description;
		public Sprite image;

		public Rarity rarity;

		public int health;
		public int damage;

	}

	public enum Rarity
	{
		Common,
		Rare,
		SuperRare,
	}
}
