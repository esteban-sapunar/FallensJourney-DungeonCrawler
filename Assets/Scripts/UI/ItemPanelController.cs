using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelController : MonoBehaviour
{
	//Values
    Item item;
    bool onVault = false;
    public bool onTown = false;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;
    public GameObject moveButton;
    //Mod UI
    public GameObject damageSingle;
    public GameObject armorSingle;
    public GameObject defenseSingle;
    public GameObject weightSingle;

    //Methods
    public void ClosePanel(){
        if(onTown == false){
            useButton.SetActive(false);
        }
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    	gameObject.SetActive(false);
        damageSingle.SetActive(false);
        armorSingle.SetActive(false);
        defenseSingle.SetActive(false);
        weightSingle.SetActive(false);
        if(onTown){
            moveButton.SetActive(false);
        }
    }
    public void OpenPanel(Item newItem, bool fromEquip, bool fromVault){
        if(fromVault){
            onVault = true;
        }
        else{
            onVault = false;
        }
    	item = newItem;
    	name.text = item.name;
    	icon.sprite = item.icon;
    	description.text = item.description;
    	if(item is Equipment){
            Equipment equip = (Equipment)item;
            if(fromEquip){
                unequipButton.SetActive(true);
            }
            else{
                if(onVault == false){
                    equipButton.SetActive(true);
                }

                dropButton.SetActive(true);

                if(onTown){
                    moveButton.SetActive(true);
                }
            }
            //mods
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
        else{
            dropButton.SetActive(true);

            if(onTown){
                moveButton.SetActive(true);
            }
            else{
               useButton.SetActive(true); 
            }
        }
    }
    public void UseItem(){
        item.Use();
        ClosePanel();
    }
    public void UnEquipItem(){
        if(item is Equipment){
            item.UnEquip();
        }
        ClosePanel();
    }
    public void DropItem(){
        if(onVault){
            Vault.instance.Remove(item);
        }
        else{
            Inventory.instance.Remove(item);
        }
        ClosePanel();
    }
    public void MoveItem(){
        if(onVault){
            Inventory.instance.Add(item);
            Vault.instance.Remove(item);
        }
        else{
            Vault.instance.Add(item);
            Inventory.instance.Remove(item);
        }
        ClosePanel();
    }
}
