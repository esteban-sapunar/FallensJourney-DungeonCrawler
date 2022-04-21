using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
	//Values
    public Transform equipmentsParent;
    EquipmentManager equipmentM;
    EquipSlot[] slots;

    //Base Functions
    void Start(){
    	equipmentM = EquipmentManager.instance;
    	equipmentM.OnEquipChangedCallback += UpdateEquipUI;
        slots = equipmentsParent.GetComponentsInChildren<EquipSlot>();
    }

    //Methods
    void UpdateEquipUI(Equipment newItem,Equipment oldItem){
    	for(int i =0; i < slots.Length;i++){
            if(equipmentM.currentEquipment[i] != slots[i].item){
                slots[i].AddEquipment(equipmentM.currentEquipment[i]);
            }
        }
    }
}

