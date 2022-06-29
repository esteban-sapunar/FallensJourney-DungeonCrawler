using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyAction
{
	//Values
	public int value;
	public EnemyActionType type;
}
public enum EnemyActionType {Attack,Defend,Healing}
