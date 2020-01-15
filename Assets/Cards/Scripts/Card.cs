using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jesus.Cards
{
	public class Card : MonoBehaviour
	{
		public CardSO cardInfo;

		[Header("Components")]
		public Text nameText;
		public Image cardImage;
		public Text healthText;
		public Text damageText;

		[Header("Statistics")]
		public int currentHealth;
		public int currentDamage;

		public bool inDeck;

		private void Start()
		{
			GetComponents();

			currentHealth = cardInfo.health;
			currentDamage = cardInfo.damage;
		}

		private void GetComponents()
		{
			if (transform.GetChild(1).childCount > 0)
			{
				nameText = transform.GetChild(1).GetChild(0).GetComponent<Text>();
				healthText = transform.GetChild(1).GetChild(1).GetComponent<Text>();
				damageText = transform.GetChild(1).GetChild(2).GetComponent<Text>();
				cardImage = transform.GetChild(1).GetChild(3).GetComponent<Image>();
			}
			
		}

		public void PopulateCard()
		{
			GetComponents();

			nameText.text = cardInfo.name;
			cardImage.sprite = cardInfo.image;
			healthText.text = cardInfo.health.ToString();
			damageText.text = cardInfo.damage.ToString();

			ChangeName(cardInfo.name);
			ChangeRarity(cardInfo.rarity);
		}

		private void EmptyCard()
		{
			nameText.text = "";
			cardImage.sprite = null;
			healthText.text = "";
			damageText.text = "";
		}

		public void ScaleCard(float size)
		{
			transform.localScale = new Vector3(size, size * 0.1f, size);
		}

		public void TakeDamage(int damage)
		{
			currentHealth -= damage;

			if (currentHealth <= 0)
			{
				DestroyCard();
			}
		}

		public void DestroyCard()
		{
			Destroy(this.gameObject);
		}

		private void ChangeName(string newName)
		{
			name = newName;
		}

		private void ChangeRarity(Rarity rarity)
		{
			Renderer rend = transform.GetChild(0).GetComponent<Renderer>();
			Material[] mats = rend.materials;

			if (rarity == Rarity.Common)
			{
				mats[2].color = Color.grey;
			}
			else if (rarity == Rarity.Rare)
			{
				mats[2].color = Color.cyan;
			}
			else if (rarity == Rarity.SuperRare)
			{
				mats[2].color = Color.yellow;
			}


			rend.materials = mats;

		}
	}
}

public enum CardSize
{
	Small,
	Medium,
	Large,
}
