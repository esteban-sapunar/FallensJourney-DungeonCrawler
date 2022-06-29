using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
	//Values
    public Transform equipmentsParent;
    EquipmentManager equipmentM;
    EquipSlot[] slots;

    //Methods
    public void Init(){
    	equipmentM = EquipmentManager.instance;
    	equipmentM.OnEquipChangedCallback += UpdateEquipUI;
        slots = equipmentsParent.GetComponentsInChildren<EquipSlot>();
    }
    void UpdateEquipUI(Equipment newItem,Equipment oldItem){
    	for(int i =0; i < slots.Length;i++){
            if(equipmentM.currentEquipment[i] != slots[i].item){
                if(equipmentM.currentEquipment[i] == null){
                    slots[i].ClearEquipment();
                }
                else{
                    slots[i].AddEquipment(equipmentM.currentEquipment[i]);
                }
                
            }
        }
    }
}

