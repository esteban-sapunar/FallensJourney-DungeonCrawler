using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName ="Game/Map/Tile")]
public class Tile : ScriptableObject
{
	//Values
	new public string name = "New Tile";
	public Sprite sprite = null;
	public TileType tileType;

}
public enum TileType {Null,Current,Path,Enemy,Chest,Boss}
