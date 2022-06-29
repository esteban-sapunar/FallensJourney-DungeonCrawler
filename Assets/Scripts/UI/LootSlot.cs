using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootSlot : MonoBehaviour
{
    //Values
    Item item;
    public GameObject DC;

    //UI
    public Image icon;
    public Text name;
    public Image iconBig;
    public Image iconSelect;
    public Text description;
    public GameObject damageSingle;
    public GameObject armorSingle;
    public GameObject defenseSingle;
    public GameObject weightSingle;

    //Methods
    public void SetItem(Item newItem){
    	item = newItem;
    	icon.sprite = item.icon;
    	icon.enabled = true;
    	gameObject.GetComponent<Button>().interactable = true;
    }
    public void SelectItem(){
    	DC.GetComponent<DataController>().selectedItem = item;
    	name.text = item.name;
    	iconBig.enabled = true;
    	iconBig.sprite = item.icon;
    	iconSelect.enabled = true;
    	iconSelect.sprite = item.icon;
    	description.text = item.description;
    	//mods
        damageSingle.SetActive(false);
        armorSingle.SetActive(false);
        defenseSingle.SetActive(false);
        weightSingle.SetActive(false);
    	if(item is Equipment){
            Equipment equip = (Equipment)item;
            if(equip.attackModifier > 0){
                damageSingle.SetActive(true);
                damageSingle.GetComponent<SingleModSlotController>().SetMod(equip.attackModifier);
            }
            if(equip.armorModifier > 0){
                armorSingle.SetActive(true);
                armorSingle.GetComponent<SingleModSlotController>().SetMod(equip.armorModifier);
            }
            if(equip.defenseModifier > 0){
                defenseSingle.SetActive(true);
                defenseSingle.GetComponent<SingleModSlotController>().SetMod(equip.defenseModifier);
            }
            if(equip.weightModifier > 0){
                weightSingle.SetActive(true);
                weightSingle.GetComponent<SingleModSlotController>().SetMod(equip.weightModifier);
            }
        }
    }
}
