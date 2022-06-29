using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTradePanel : MonoBehaviour
{
    //Values
    Item item;
    public GameObject GM;
    public GameObject player;
    int cost = 0;
    bool onVault = false;
    //UI
    public Text name;
    public Image icon;
    public Text description;
    public Text priceNumber;
    public GameObject buyButton;
    public GameObject sellButton;
    public GameObject dropButton;
    public GameObject moveButton;
    //Mod UI
    public GameObject damageSingle;
    public GameObject armorSingle;
    public GameObject defenseSingle;
    public GameObject weightSingle;

    //Methods
    public void ClosePanel(){
    	damageSingle.SetActive(false);
        armorSingle.SetActive(false);
        defenseSingle.SetActive(false);
        weightSingle.SetActive(false);
        gameObject.SetActive(false);
        buyButton.SetActive(false);
        sellButton.SetActive(false);
        dropButton.SetActive(false);
        moveButton.SetActive(false);
    }

    public void OpenPanel(Item newItem,int price, bool fromVault,bool fromShop){
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
        cost = price;
        priceNumber.text = price.ToString();
        //Action Panel       
        if(fromShop){
        	buyButton.SetActive(true);
        }
        else{
        	sellButton.SetActive(true);
        	dropButton.SetActive(true);
        	moveButton.SetActive(true);
        }
        if(item is Equipment){
            Equipment equip = (Equipment)item;
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

    }
    public void BuyItem(){
    	if(player.GetComponent<TownPlayerStats>().gold >= cost){
    		player.GetComponent<TownPlayerStats>().PayGold(cost);
    		Vault.instance.Add(item);
        	ClosePanel();
    	}        
    }
    public void SellItem(){
    		player.GetComponent<TownPlayerStats>().EarnGold(cost);
    		if(onVault){
    			Vault.instance.Remove(item);
    		}
    		else{
    			Inventory.instance.Remove(item);
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
