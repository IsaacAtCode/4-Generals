using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jesus.Cards
{
	public class Card : MonoBehaviour
	{
		//public CardSO cardInfo;

		[Header("Components")]
		public GameObject cardBase;

		public Text nameText;
		public Image cardImage;
		public Text healthText;
		public Text damageText;
		public Image buffImage;

        private void GetComponents()
        {
            cardBase = this.gameObject;

            nameText = cardBase.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            healthText = cardBase.transform.GetChild(0).GetChild(1).GetComponent<Text>();
            damageText = cardBase.transform.GetChild(0).GetChild(2).GetComponent<Text>();
            cardImage = cardBase.transform.GetChild(0).GetChild(3).GetComponent<Image>();
            buffImage = cardBase.transform.GetChild(0).GetChild(4).GetComponent<Image>();
                
        }




		public void PopulateCard(CardSO cardInfo)
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

        private void ChangeName(string newName)
        {
            name = newName;
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
}
