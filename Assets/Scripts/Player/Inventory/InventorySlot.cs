using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	//Values
    Item item;
    public Image icon;
    public GameObject panel;

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
    public void OpenItem(){
    	if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemPanelController>().OpenPanel(item,false,false);
        }
    }
    public void OpenTradeItem(){
        if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemTradePanel>().OpenPanel(item,item.value,false,false);
        }
    }

}
