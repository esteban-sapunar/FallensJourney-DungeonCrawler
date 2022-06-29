using UnityEngine;
using UnityEngine.UI;

public class VaultSlot : MonoBehaviour
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
    public virtual void OnVaultRemoveButton(){
        Vault.instance.Remove(item);
    }
    public void OpenItem(){
    	if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemPanelController>().OpenPanel(item,false,true);
        }
    }
    public void OpenTradeItem(){
        if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemTradePanel>().OpenPanel(item,item.value,true,false);
        }
    }
}
