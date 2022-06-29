using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName ="Game/Enemy")]
public class Enemy : ScriptableObject
{
	//Values
	new public string name = "New Enemy";
	public Sprite sprite = null;
	public string description;
	public int tier = 0;

	public int health = 0;
	public int armor = 0;
	public int exp = 0;
	public int gold = 0;
	public int dropPercent = 0;

	public List<EnemyAction> actions = new List<EnemyAction>();
}
