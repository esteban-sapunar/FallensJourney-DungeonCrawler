using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{
	//Values
	Dictionary <string, int> playerData = new Dictionary<string, int>();
	Dictionary <string, int> equipData = new Dictionary<string, int>();
	Dictionary <string, int> itemsData = new Dictionary<string, int>();
	public List <Item> initialEquip = new List<Item>();
	public List <Item> initialItems = new List<Item>();
	public List <Item> rewardItems = new List<Item>();
	public Item selectedItem;
  public int stage;

	//Objects
	public GameObject player;
  public GameObject equipUI;
	public GameObject inventoryUI;
	public GameObject rewardPanel;
	public GameObject expPanel;
	public GameObject goldPanel;
	public GameObject itemPanel;
	public GameObject lootPanel;

	//Library
	public List <Item> allItems;
	public List <Card> allCards;

	//UI
	public Text expNumber;
	public Text goldNumber;
	public GameObject buttonWin;
	public GameObject buttonRetreat;
	public GameObject buttonDeath;
	public Transform lootSlotParent;
	LootSlot[] lootSlots;

    //Basic Functions
   	void Awake(){
   		//Load
   		//Player
   		playerData.Add("level",PlayerPrefs.GetInt("Level"));
   		playerData.Add("exp",PlayerPrefs.GetInt("Exp"));
   		playerData.Add("gold",PlayerPrefs.GetInt("Gold"));
      playerData.Add("points",PlayerPrefs.GetInt("Points"));
   		playerData.Add("strength",PlayerPrefs.GetInt("Strength"));
   		playerData.Add("resistance",PlayerPrefs.GetInt("Resistance"));
   		playerData.Add("vitality",PlayerPrefs.GetInt("Vitality"));
   		//Equip
   		equipData.Add("head",PlayerPrefs.GetInt("HeadSlot"));
   		equipData.Add("chest",PlayerPrefs.GetInt("ChestSlot"));
   		equipData.Add("legs",PlayerPrefs.GetInt("LegsSlot"));
   		equipData.Add("feet",PlayerPrefs.GetInt("FeetSlot"));
   		equipData.Add("weapon",PlayerPrefs.GetInt("WeaponSlot"));
   		equipData.Add("shield",PlayerPrefs.GetInt("ShieldSlot"));
   		//Items
   		itemsData.Add("slot1",PlayerPrefs.GetInt("ItemSlot1"));
   		itemsData.Add("slot2",PlayerPrefs.GetInt("ItemSlot2"));
   		itemsData.Add("slot3",PlayerPrefs.GetInt("ItemSlot3"));
   		itemsData.Add("slot4",PlayerPrefs.GetInt("ItemSlot4"));
   		itemsData.Add("slot5",PlayerPrefs.GetInt("ItemSlot5"));
   		itemsData.Add("slot6",PlayerPrefs.GetInt("ItemSlot6"));
   		itemsData.Add("slot7",PlayerPrefs.GetInt("ItemSlot7"));
   		itemsData.Add("slot8",PlayerPrefs.GetInt("ItemSlot8"));
   		itemsData.Add("slot9",PlayerPrefs.GetInt("ItemSlot9"));
   		itemsData.Add("slot10",PlayerPrefs.GetInt("ItemSlot10"));
   		itemsData.Add("slot11",PlayerPrefs.GetInt("ItemSlot11"));
   		itemsData.Add("slot12",PlayerPrefs.GetInt("ItemSlot12"));
   		itemsData.Add("slot13",PlayerPrefs.GetInt("ItemSlot13"));
   		itemsData.Add("slot14",PlayerPrefs.GetInt("ItemSlot14"));
   		itemsData.Add("slot15",PlayerPrefs.GetInt("ItemSlot15"));
   		itemsData.Add("slot16",PlayerPrefs.GetInt("ItemSlot16"));
   		itemsData.Add("reward",PlayerPrefs.GetInt("RewardItem"));
   		PlayerPrefs.SetInt("RewardItem", 0);


   		//Set
   		//Player
   		player.GetComponent<PlayerStats>().maxHealth = 50 +(playerData["vitality"]*5);
   		player.GetComponent<PlayerStats>().baseStamina = 25 +(playerData["resistance"]*2);
      player.GetComponent<PlayerStats>().strength = playerData["strength"];
   		gameObject.GetComponent<BattleController>().earnedExp = 0;
   		gameObject.GetComponent<BattleController>().earnedGold = 0;
   		player.GetComponent<PlayerStats>().InitData();
		//Equip
		player.GetComponent<EquipmentManager>().Init();
    equipUI.GetComponent<EquipmentUI>().Init();
   		foreach(KeyValuePair<string,int> equipId in equipData){
   			if(equipId.Value != 0){
   				player.GetComponent<EquipmentManager>().Equip((Equipment)allItems[equipId.Value-1]);
   				initialEquip.Add((Equipment)allItems[equipId.Value-1]);
   			}
   		}
   		inventoryUI.GetComponent<InventoryUI>().Init();
   		//Inventory
   		foreach(KeyValuePair<string,int> itemId in itemsData){
   			if(itemId.Value != 0){
   				gameObject.GetComponent<Inventory>().Add(allItems[itemId.Value-1]);
   				initialItems.Add(allItems[itemId.Value-1]);
   			}
   		}		
   		
   		//Cards
   		for(int i = 1; i<=30;i++){
   			if(PlayerPrefs.GetInt("DeskCard"+i.ToString()) > 0){
   				gameObject.GetComponent<DeskController>().AddCard(allCards[PlayerPrefs.GetInt("DeskCard"+i.ToString())-1]);
   			}   			
   		}

   	}

   	//Methods
   	public void OpenRewardPanel(string state){
   		RewardItems();
   		rewardPanel.SetActive(true);
   		switch(state){
   			case "death":
   				expPanel.SetActive(true);
   				buttonDeath.SetActive(true);
   				expNumber.text = gameObject.GetComponent<BattleController>().earnedExp.ToString();
   			break;
   			case "retreat":
   				expPanel.SetActive(true);
   				goldPanel.SetActive(true);
   				buttonRetreat.SetActive(true);
   				expNumber.text = gameObject.GetComponent<BattleController>().earnedExp.ToString();
   				goldNumber.text = gameObject.GetComponent<BattleController>().earnedGold.ToString();
   			break;
   			case "win":
   				expPanel.SetActive(true);
   				goldPanel.SetActive(true);
   				itemPanel.SetActive(true);
   				lootPanel.SetActive(true);
   				buttonWin.SetActive(true);
   				expNumber.text = gameObject.GetComponent<BattleController>().earnedExp.ToString();
   				goldNumber.text = gameObject.GetComponent<BattleController>().earnedGold.ToString();
   				//items
   				lootSlots = lootSlotParent.GetComponentsInChildren<LootSlot>();
   				for(int i =0; i < rewardItems.Count;i++){
   					lootSlots[i].SetItem(rewardItems[i]);
   				}
   			break;
   		}

   	}
   	public void RewardItems(){
   		List <Item> loot = new List<Item>();
      foreach(Item item in gameObject.GetComponent<Inventory>().items){
        if(item != null){
          loot.Add(item);
        }
      }
    	Equipment[] lastEquipment = player.GetComponent<EquipmentManager>().currentEquipment;
    	foreach(Item equip in lastEquipment){
    		if(equip != null){
    			loot.Add(equip);
    		}
    	}
    	foreach(Item equip in initialEquip){
    		if(equip != null){
    			loot.Remove(equip);
    		}
    	}
    	foreach(Item item in initialItems){
    		if(item != null){
    			loot.Remove(item);
    		}
    	}
    	rewardItems = loot;
   	}

   	public void SaveData(){
    	PlayerPrefs.SetInt("Exp", PlayerPrefs.GetInt("Exp")+gameObject.GetComponent<BattleController>().earnedExp);
    	PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold")+gameObject.GetComponent<BattleController>().earnedGold);
      List <Item> inventoryArray = gameObject.GetComponent<Inventory>().items;
      for(int i = 0;i < initialItems.Count;i++){
        if(initialItems[i] is Equipment){
          PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), allItems.IndexOf(initialItems[i])+1);
          Debug.Log(initialItems[i].name+" E");
        }
        else{
          Debug.Log(inventoryArray.IndexOf(initialItems[i]).ToString());
          if(inventoryArray.IndexOf(initialItems[i]) >= 0){
            PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), allItems.IndexOf(initialItems[i])+1);
            inventoryArray.Remove(initialItems[i]);
            Debug.Log(initialItems[i].name+" I");
          }
          else{
            PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), 0);
            Debug.Log(initialItems[i].name+" N");
          }
        }
      }
    	if(initialItems.Count < 16){
    		PlayerPrefs.SetInt("RewardItem", allItems.IndexOf(selectedItem)+1);
    	}
      if(PlayerPrefs.GetInt("Stage") == stage && PlayerPrefs.GetInt("Stage") < 1){
        PlayerPrefs.SetInt("Stage", stage+1);
      }
    	SceneManager.LoadScene("TownMenu");
   	}

   	public void SaveDataRetreat(){
    	PlayerPrefs.SetInt("Exp", PlayerPrefs.GetInt("Exp")+gameObject.GetComponent<BattleController>().earnedExp);
    	PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold")+gameObject.GetComponent<BattleController>().earnedGold);
      List <Item> inventoryArray = gameObject.GetComponent<Inventory>().items;
      for(int i = 0;i < initialItems.Count;i++){
        if(initialItems[i] is Equipment){
          PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), allItems.IndexOf(initialItems[i])+1);
          Debug.Log(initialItems[i].name+" E");
        }
        else{
          Debug.Log(inventoryArray.IndexOf(initialItems[i]).ToString());
          if(inventoryArray.IndexOf(initialItems[i]) >= 0){
            PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), allItems.IndexOf(initialItems[i])+1);
            inventoryArray.Remove(initialItems[i]);
            Debug.Log(initialItems[i].name+" I");
          }
          else{
            PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), 0);
            Debug.Log(initialItems[i].name+" N");
          }
        }
      }
    	SceneManager.LoadScene("TownMenu");
   	}

   	public void SaveDataDeath(){
    	PlayerPrefs.SetInt("Exp", PlayerPrefs.GetInt("Exp")+gameObject.GetComponent<BattleController>().earnedExp);
      List <Item> inventoryArray = gameObject.GetComponent<Inventory>().items;
      for(int i = 0;i < initialItems.Count;i++){
        if(initialItems[i] is Equipment){
          PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), allItems.IndexOf(initialItems[i])+1);
          Debug.Log(initialItems[i].name+" E");
        }
        else{
          Debug.Log(inventoryArray.IndexOf(initialItems[i]).ToString());
          if(inventoryArray.IndexOf(initialItems[i]) >= 0){
            PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), allItems.IndexOf(initialItems[i])+1);
            inventoryArray.Remove(initialItems[i]);
            Debug.Log(initialItems[i].name+" I");
          }
          else{
            PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), 0);
            Debug.Log(initialItems[i].name+" N");
          }
        }
      }
    	SceneManager.LoadScene("TownMenu");
   	}
   	public void OpenRetreatPanel(){
   		OpenRewardPanel("retreat");
   	}

}
