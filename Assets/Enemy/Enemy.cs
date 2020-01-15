using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public List<EnemySO> enemies;

	public GameObject enemyBase;
	public Text nameText;
	public Image faceImage;

	private void Start()
	{
		PopulateEnemy(enemies[Random.Range(0, enemies.Count)]);
	}



	private void PopulateEnemy(EnemySO stats)
	{
		enemyBase.GetComponent<Renderer>().material.color = stats.color;
		nameText.text = stats.name;
		faceImage.sprite = stats.face;

	}
	
}
