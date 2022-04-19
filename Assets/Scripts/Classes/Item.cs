using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
	//Values
	new public string name = "New Item";
	public Sprite icon = null;

}
