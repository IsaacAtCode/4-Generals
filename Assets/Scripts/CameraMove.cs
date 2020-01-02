using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform BattleGrid;
    public Transform Enemy;
    public Transform Deck;

    public CameraPosition camPos;

    public float moveTime;

    private void Start()
    {
        transform.position = BattleGrid.position;
        transform.rotation = BattleGrid.rotation;
        camPos = CameraPosition.BattleGrid;
    }

    private void Update()
    {
        ChangeFocus();
    }

    private void ChangeFocus()
    {
        if (camPos == CameraPosition.BattleGrid)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(LerpToPosition(moveTime, Enemy));
                camPos = CameraPosition.Enemy;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(LerpToPosition(moveTime, Deck));
                camPos = CameraPosition.Deck;
            }
        }
        else if (camPos == CameraPosition.Enemy)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Cannot look more forward");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(LerpToPosition(moveTime, BattleGrid));
                camPos = CameraPosition.BattleGrid;
            }
        }
        else if (camPos == CameraPosition.Deck)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(LerpToPosition(moveTime, BattleGrid));
                camPos = CameraPosition.BattleGrid;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Cannot look more backward");
            }
        }

    }

    IEnumerator LerpToPosition(float lerpSpeed, Transform newPosition)
    {

        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);

            transform.position = Vector3.Lerp(startingPos, newPosition.position, t);
            yield return 0;
        }

    }
}
public enum CameraPosition
{
    BattleGrid,
    Enemy,
    Deck,
}
