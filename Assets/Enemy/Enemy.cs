using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemies")]
	public List<EnemySO> enemyList;
    private List<EnemySO> enemies;
    public EnemySO enemyInfo;

    [Header("Cmponents")]
	public GameObject enemyBase;
	public Text nameText;
	public Image faceImage;

    public bool isAlive;

	private void Start()
	{
        enemies = new List<EnemySO>(enemyList);


        Spawn();
	}

    public void Spawn()
    {

        enemyInfo = enemies[Random.Range(0, enemies.Count)];

        PopulateEnemy(enemyInfo);

        isAlive = true;

    }




	private void PopulateEnemy(EnemySO stats)
	{
		enemyBase.GetComponent<Renderer>().material.color = stats.color;
		nameText.text = stats.name;
		faceImage.sprite = stats.face;
	}
	
}
