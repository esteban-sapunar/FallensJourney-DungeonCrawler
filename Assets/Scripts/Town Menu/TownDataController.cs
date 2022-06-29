using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDataController : MonoBehaviour
{
  //Values
	Dictionary <string, int> playerData = new Dictionary<string, int>();
	Dictionary <string, int> equipData = new Dictionary<string, int>();
	Dictionary <string, int> itemsData = new Dictionary<string, int>();
  Dictionary <string, int> vaultData = new Dictionary<string, int>();
  Dictionary <string, int> deskData = new Dictionary<string, int>();

	//Objects
	public GameObject player;
  public GameObject equipUI;
	public GameObject inventoryUI;
  public GameObject vaultUI;
  public GameObject deskUI;
  public GameObject cardsVaultUI;
  public GameObject cardsVaultTrainerUI;
  public GameObject inventoryShopUI;
  public GameObject vaultShopUI;
  public GameObject playerPanel;
  public GameObject cardsPanel;
  public GameObject trainerPanel;
  public GameObject shopPanel;
  public Transform cardsvaultParent;

	//Library
	public List <Item> allItems;
	public List <Card> allCards;

	//UI

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


      //Vault
      for(int i=1;i<=40;i++){
        vaultData.Add("slot"+i.ToString(),PlayerPrefs.GetInt("ItemVault"+i.ToString()));
      }
      //Desk
      for(int i=1;i<=30;i++){
        deskData.Add("slot"+i.ToString(),PlayerPrefs.GetInt("DeskCard"+i.ToString()));
      }


   		//Set
   		//Player
   		player.GetComponent<TownPlayerStats>().maxHealth = 50 +(playerData["vitality"]*5);
   		player.GetComponent<TownPlayerStats>().baseStamina = 25 +(playerData["resistance"]*2);
   		player.GetComponent<TownPlayerStats>().exp = playerData["exp"];
   		player.GetComponent<TownPlayerStats>().gold = playerData["gold"];
	    player.GetComponent<TownPlayerStats>().level = playerData["level"];
	    player.GetComponent<TownPlayerStats>().points = playerData["points"];
	    player.GetComponent<TownPlayerStats>().strength = playerData["strength"];
	    player.GetComponent<TownPlayerStats>().resistance = playerData["resistance"];
	    player.GetComponent<TownPlayerStats>().vitality = playerData["vitality"];
   		player.GetComponent<TownPlayerStats>().InitData();
  		//Equip
  		player.GetComponent<EquipmentManager>().Init();
      equipUI.GetComponent<EquipmentUI>().Init();
   		foreach(KeyValuePair<string,int> equipId in equipData){
   			if(equipId.Value != 0){
   				player.GetComponent<EquipmentManager>().Equip((Equipment)allItems[equipId.Value-1]);
   			}
   		}
   		inventoryUI.GetComponent<InventoryUI>().Init();
      vaultUI.GetComponent<VaultUI>().Init();
      deskUI.GetComponent<TownDeskUI>().Init();
      cardsVaultUI.GetComponent<CardsVaultUI>().Init();
      cardsVaultTrainerUI.GetComponent<CardsVaultUI>().Init();
      inventoryShopUI.GetComponent<InventoryUI>().Init();
      vaultShopUI.GetComponent<VaultUI>().Init();
   		//Inventory
   		foreach(KeyValuePair<string,int> itemId in itemsData){
   			if(itemId.Value != 0){
   				gameObject.GetComponent<Inventory>().Add(allItems[itemId.Value-1]);
   			}
   		}
   		for(int i = 0;i < gameObject.GetComponent<Inventory>().items.Count;i++){
   			if(gameObject.GetComponent<Inventory>().items[i]){
   				PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), allItems.IndexOf(gameObject.GetComponent<Inventory>().items[i])+1);
   			}
   		}
      //Vault
      foreach(KeyValuePair<string,int> itemId in vaultData){
        if(itemId.Value != 0){
          gameObject.GetComponent<Vault>().Add(allItems[itemId.Value-1]);
        }
      }
      //Desk
      foreach(KeyValuePair<string,int> cardId in deskData){
        if(cardId.Value != 0){
          gameObject.GetComponent<TownDesk>().Add(allCards[cardId.Value-1]);
        }
      }
      //CardsVault
        string CardsVaultString = PlayerPrefs.GetString("CardsVaultInfo");
        string[] arrayCardsTypes =  CardsVaultString.Split(char.Parse("|"));
        for(int i=0; i<arrayCardsTypes.Length;i++){
          string[] auxArray = arrayCardsTypes[i].Split(char.Parse(","));
          int type = int.Parse(auxArray[0]);
          int number = int.Parse(auxArray[1]);
          for(int j = 0; j<number; j++){
            gameObject.GetComponent<CardsVault>().Add(allCards[type-1]);
          }
        }

   		playerPanel.SetActive(false);
      cardsPanel.SetActive(false);
      trainerPanel.SetActive(false);
      shopPanel.SetActive(false);
   		//Cards
   		/*for(int i = 1; i<=30;i++){
   			Debug.Log("DeskCard"+i.ToString());
   			if(PlayerPrefs.GetInt("DeskCard"+i.ToString()) > 0){
   				gameObject.GetComponent<DeskController>().AddCard(allCards[PlayerPrefs.GetInt("DeskCard"+i.ToString())-1]);
   			}   			
   		}*/

   	}

   	public void SaveData(){
   		PlayerPrefs.SetInt("Level", player.GetComponent<TownPlayerStats>().level);
    	PlayerPrefs.SetInt("Exp", player.GetComponent<TownPlayerStats>().exp);
    	PlayerPrefs.SetInt("Gold", player.GetComponent<TownPlayerStats>().gold);
    	PlayerPrefs.SetInt("Strength", player.GetComponent<TownPlayerStats>().strength);
    	PlayerPrefs.SetInt("Resistance", player.GetComponent<TownPlayerStats>().resistance);
    	PlayerPrefs.SetInt("Vitality", player.GetComponent<TownPlayerStats>().vitality);
    	PlayerPrefs.SetInt("Points", player.GetComponent<TownPlayerStats>().points);

    	//Equip
      PlayerPrefs.SetInt("HeadSlot", allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[0])+1);
      PlayerPrefs.SetInt("ChestSlot", allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[1])+1);
      PlayerPrefs.SetInt("LegsSlot", allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[2])+1);
      PlayerPrefs.SetInt("FeetSlot", allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[3])+1);
      PlayerPrefs.SetInt("WeaponSlot", allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[4])+1);
      PlayerPrefs.SetInt("ShieldSlot", allItems.IndexOf(player.GetComponent<EquipmentManager>().currentEquipment[5])+1);

      for(int i = 0; i<16;i++){
      	PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), 0);
      }
    	for(int i = 0;i < gameObject.GetComponent<Inventory>().items.Count;i++){
   			if(gameObject.GetComponent<Inventory>().items[i]){
   				PlayerPrefs.SetInt("ItemSlot"+(i+1).ToString(), allItems.IndexOf(gameObject.GetComponent<Inventory>().items[i])+1);
   			}
   		}
      for(int i = 0; i<40;i++){
        PlayerPrefs.SetInt("ItemVault"+(i+1).ToString(), 0);
      }
      for(int i = 0;i < gameObject.GetComponent<Vault>().items.Count;i++){
        if(gameObject.GetComponent<Vault>().items[i]){
          PlayerPrefs.SetInt("ItemVault"+(i+1).ToString(), allItems.IndexOf(gameObject.GetComponent<Vault>().items[i])+1);
        }
      }
      //Desk
      for(int i=1;i<=30;i++){
        PlayerPrefs.SetInt("DeskCard"+i.ToString(),allCards.IndexOf(gameObject.GetComponent<TownDesk>().desk[i-1])+1);
      }
      //Cards Vault
      CardVaultSlot[] slots;
      slots = cardsvaultParent.GetComponentsInChildren<CardVaultSlot>();
      string cardsVaultInfo ="";
      for(int i=0;i< slots.Length;i++){
        if(slots[i].cards.Count > 0){
          string auxString = (allCards.IndexOf(slots[i].cards[0])+1).ToString()+","+slots[i].cards.Count.ToString();
          cardsVaultInfo += auxString+"|";
        }
      }
      if(cardsVaultInfo.Length > 0){
        cardsVaultInfo = cardsVaultInfo.Substring(0,cardsVaultInfo.Length-1);
        PlayerPrefs.SetString("CardsVaultInfo",cardsVaultInfo);
      }
   	}
}
