using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
	public new string name;

	public int damage;
	public int health;

	public Sprite face;

	public Color color;
}
