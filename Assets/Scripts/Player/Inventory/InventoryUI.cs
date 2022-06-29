using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	//Values
    public Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;

    //Base Functions
    public void Init(){
    	inventory = Inventory.instance;
    	inventory.OnItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    //Methods
    void UpdateUI(){
    	for(int i =0; i < slots.Length;i++){
            if(i < inventory.items.Count){
                slots[i].AddItem(inventory.items[i]);
            }
            else{
                slots[i].ClearItem();

            }
        }
    }
}
