using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	//Values
    Item item;
    public Image icon;

    //Methods
    public void AddItem(Item newItem){
    	item = newItem;
    	icon.sprite = item.icon;
    	icon.enabled = true;
    }
    public void ClearItem(){
    	item = null;
    	icon.sprite = null;
    	icon.enabled = false;
    }
    public virtual void OnRemoveButton(){
    	Inventory.instance.Remove(item);
    }
    public void UseItem(){
    	if(item != null){
    		item.Use();
    	}
    }
    public void OpenItem(){
    	if(item != null){
    		Debug.Log("Open "+item.name);
    	}
    }

}
