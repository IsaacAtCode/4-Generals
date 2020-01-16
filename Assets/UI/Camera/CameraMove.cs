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
        ChangeFocus();
	}

	private void ChangeFocus()
	{
        if (camPos == CameraPosition.Deck)
        {
            GoToDeck();
        }
        else if (camPos == CameraPosition.BattleGrid)
        {
            GoToBattleGrid();
        }
        else if (camPos == CameraPosition.Enemy)
        {
            GoToEnemy();
        }

	}

	public void GoToDeck()
	{
		StartCoroutine(LerpToPosition(moveTime, Deck));
		camPos = CameraPosition.Deck;
	}

	public void GoToBattleGrid()
	{
		StartCoroutine(LerpToPosition(moveTime, BattleGrid));
		camPos = CameraPosition.BattleGrid;
	}

	public void GoToEnemy()
	{
		StartCoroutine(LerpToPosition(moveTime, Enemy));
		camPos = CameraPosition.Enemy;
	}

	IEnumerator LerpToPosition(float lerpSpeed, Transform newPosition)
	{

		float t = 0.0f;
		Vector3 startingPos = transform.position;
		Quaternion startingRot = transform.rotation;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);

			transform.position = Vector3.Lerp(startingPos, newPosition.position, t);
			transform.rotation = Quaternion.Lerp(startingRot, newPosition.rotation, t);

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
