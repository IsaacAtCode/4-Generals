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
		public Image buffImage;

		[Header("Statistics")]
		public int currentHealth;
		public int currentDamage;

		public bool inDeck;

		private void Start()
		{
			GetComponents();
		}

		private void GetComponents()
		{
			if (transform.GetChild(0).childCount > 0)
			{
				nameText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
				healthText = transform.GetChild(0).GetChild(1).GetComponent<Text>();
				damageText = transform.GetChild(0).GetChild(2).GetComponent<Text>();
				cardImage = transform.GetChild(0).GetChild(3).GetComponent<Image>();
				buffImage = transform.GetChild(0).GetChild(4).GetComponent<Image>();
			}
			
		}

		public void PopulateCard()
		{
			GetComponents();

			nameText.text = cardInfo.name;
			cardImage.sprite = cardInfo.image;
			healthText.text = cardInfo.health.ToString();
			damageText.text = cardInfo.damage.ToString();
			//buffImage = 

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
			Material mat = this.GetComponent<Renderer>().material;


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
}

public enum CardSize
{
	Small,
	Medium,
	Large,
}
