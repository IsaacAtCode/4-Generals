using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Board board;

    public Enemy enemy;

    private int boardHealth;
    private int boardDamage;

    private int enemyHealth;
    private int enemyDamage;


    [Header("UI")]
    public CameraMove cm;

    public Text enemyHealthText;
    public Text enemyDamageText;

    public GameObject boardCanvas;
    public Text boardHealthText;
    public Text boardDamageText;

    public Text resultsText;

    [Header("Battle")]
    public bool startBattle = false;
    float timeBetweenAttacks = 2f;
    public BattleState bs = BattleState.Idle;


    private void Start()
    {
        ClearText();
        resultsText.text = "";
    }

    private void ClearText()
    {
        enemyHealthText.text = "";
        enemyDamageText.text = "";

        boardHealthText.text = "";
        boardDamageText.text = "";
    }


    private void GetHealthAndDamage()
    {
        enemyHealth = enemy.enemyInfo.health;
        enemyDamage = enemy.enemyInfo.damage;

        boardDamage = board.totalDamage;
        boardHealth = board.totalHealth;

    }

    private void OnGUI()
    {
        enemyHealthText.text = enemyHealth.ToString();
        enemyDamageText.text = enemyDamage.ToString();

        boardHealthText.text = boardHealth.ToString();
        boardDamageText.text = boardDamage.ToString();

        
    }

    private void Update()
    {
        if (cm.camPos == CameraPosition.Deck)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }


        if (bs == BattleState.Start)
        {
            StartBattle();
            ShowUI();
        }
        if (bs == BattleState.Win)
        {
            resultsText.text = "Win";
        }
        else if (bs == BattleState.Lose)
        {
            resultsText.text = "Lose";
        }
    }

    private void HideUI()
    {
        boardCanvas.SetActive(false);
    }

    private void ShowUI()
    {
        boardCanvas.SetActive(true);
    }


    public void StartBattle()
    {
        GetHealthAndDamage();

        startBattle = true;

        StartCoroutine(OneTurn());
        
    }

    IEnumerator OneTurn()
    {
        if (bs == BattleState.Battle)
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            BoardAttack();
            if (enemyHealth > 0)
            {
                yield return new WaitForSeconds(timeBetweenAttacks);
                EnemyAttack();

                if (enemyHealth <= 0)
                {
                    bs = BattleState.Win;
                }

                if (boardHealth <= 0 && enemyHealth > 0)
                {
                    bs = BattleState.Lose;
                }
            }
        } 
    }


    private void BoardAttack()
    {
        resultsText.text = "Board attacking";
        enemyHealth -= boardDamage;
    }
    private void EnemyAttack()
    {
        resultsText.text = "Enemy Attacking";
        boardHealth -= enemyDamage;
    }
}

public enum BattleState
{
    Idle,
    Start,
    Battle,
    Win,
    Lose,
}