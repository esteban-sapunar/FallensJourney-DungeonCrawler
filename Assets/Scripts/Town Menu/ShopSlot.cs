using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    //Values
    public Item item;
    public int price;

    //UI
    public Image icon;
    public Text priceNumber;
    public GameObject pricePanel;
    public GameObject panel;
    

    //Basic Functions
    void Start(){
    	if(item != null){
	    	icon.sprite = item.icon;
	    	icon.enabled = true;
	    	pricePanel.SetActive(true);
	    	priceNumber.text = price.ToString();
    	}
    }

    //Methods
    public void OpenPanel(){
    	if(item != null){
            panel.SetActive(true);
            panel.GetComponent<ItemTradePanel>().OpenPanel(item,price,false,true);
        }
    }
}
