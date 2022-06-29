using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName ="Cards/Card")]
public class Card : ScriptableObject
{
	//values
	new public string name = "New Card";
	public Sprite icon = null;
	public string description;
	public int tier = 0;

	public CardTypes cardType;

	//Actions Values
	public int staminaCost = 0;
	public int damage = 0;
	public int defense = 0;
	public int healing = 0;
	public int percent = 0;
}
public enum CardTypes {Attack,Block,Healing,MixAttack,AttackPick,DefensePick,ShieldBash}
