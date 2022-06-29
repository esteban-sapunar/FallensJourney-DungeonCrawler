using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame(){
    	//Data
        //Player
    	PlayerPrefs.SetInt("Level", 1);
    	PlayerPrefs.SetInt("Exp", 0);
    	PlayerPrefs.SetInt("Gold", 0);
    	PlayerPrefs.SetInt("Strength", 0);
    	PlayerPrefs.SetInt("Resistance", 0);
    	PlayerPrefs.SetInt("Vitality", 0);
        PlayerPrefs.SetInt("Points", 0);
        PlayerPrefs.SetInt("Stage", 0);
        //Equip
        PlayerPrefs.SetInt("HeadSlot", 0);
        PlayerPrefs.SetInt("ChestSlot", 3);
        PlayerPrefs.SetInt("LegsSlot", 4);
        PlayerPrefs.SetInt("FeetSlot", 5);
        PlayerPrefs.SetInt("WeaponSlot", 1);
        PlayerPrefs.SetInt("ShieldSlot", 0);
        //Items
        PlayerPrefs.SetInt("ItemSlot1", 21);
        PlayerPrefs.SetInt("ItemSlot2", 0);
        PlayerPrefs.SetInt("ItemSlot3", 0);
        PlayerPrefs.SetInt("ItemSlot4", 0);
        PlayerPrefs.SetInt("ItemSlot5", 0);
        PlayerPrefs.SetInt("ItemSlot6", 0);
        PlayerPrefs.SetInt("ItemSlot7", 0);
        PlayerPrefs.SetInt("ItemSlot8", 0);
        PlayerPrefs.SetInt("ItemSlot9", 0);
        PlayerPrefs.SetInt("ItemSlot10", 0);
        PlayerPrefs.SetInt("ItemSlot11", 0);
        PlayerPrefs.SetInt("ItemSlot12", 0);
        PlayerPrefs.SetInt("ItemSlot13", 0);
        PlayerPrefs.SetInt("ItemSlot14", 0);
        PlayerPrefs.SetInt("ItemSlot15", 0);
        PlayerPrefs.SetInt("ItemSlot16", 0);
        //Reward Items
        PlayerPrefs.SetInt("RewardItem", 0);
        //Vault
        PlayerPrefs.SetInt("ItemVault1", 0);
        PlayerPrefs.SetInt("ItemVault2", 0);
        PlayerPrefs.SetInt("ItemVault3", 0);
        PlayerPrefs.SetInt("ItemVault4", 0);
        PlayerPrefs.SetInt("ItemVault5", 0);
        PlayerPrefs.SetInt("ItemVault6", 0);
        PlayerPrefs.SetInt("ItemVault7", 0);
        PlayerPrefs.SetInt("ItemVault8", 0);
        PlayerPrefs.SetInt("ItemVault9", 0);
        PlayerPrefs.SetInt("ItemVault10", 0);
        PlayerPrefs.SetInt("ItemVault11", 0);
        PlayerPrefs.SetInt("ItemVault12", 0);
        PlayerPrefs.SetInt("ItemVault13", 0);
        PlayerPrefs.SetInt("ItemVault14", 0);
        PlayerPrefs.SetInt("ItemVault15", 0);
        PlayerPrefs.SetInt("ItemVault16", 0);
        PlayerPrefs.SetInt("ItemVault17", 0);
        PlayerPrefs.SetInt("ItemVault18", 0);
        PlayerPrefs.SetInt("ItemVault19", 0);
        PlayerPrefs.SetInt("ItemVault20", 0);
        PlayerPrefs.SetInt("ItemVault21", 0);
        PlayerPrefs.SetInt("ItemVault22", 0);
        PlayerPrefs.SetInt("ItemVault23", 0);
        PlayerPrefs.SetInt("ItemVault24", 0);
        PlayerPrefs.SetInt("ItemVault25", 0);
        PlayerPrefs.SetInt("ItemVault26", 0);
        PlayerPrefs.SetInt("ItemVault27", 0);
        PlayerPrefs.SetInt("ItemVault28", 0);
        PlayerPrefs.SetInt("ItemVault29", 0);
        PlayerPrefs.SetInt("ItemVault30", 0);
        PlayerPrefs.SetInt("ItemVault31", 0);
        PlayerPrefs.SetInt("ItemVault32", 0);
        PlayerPrefs.SetInt("ItemVault33", 0);
        PlayerPrefs.SetInt("ItemVault34", 0);
        PlayerPrefs.SetInt("ItemVault35", 0);
        PlayerPrefs.SetInt("ItemVault36", 0);
        PlayerPrefs.SetInt("ItemVault37", 0);
        PlayerPrefs.SetInt("ItemVault38", 0);
        PlayerPrefs.SetInt("ItemVault39", 0);
        PlayerPrefs.SetInt("ItemVault40", 0);
        //Desk
        PlayerPrefs.SetInt("DeskCard1", 1);
        PlayerPrefs.SetInt("DeskCard2", 1);
        PlayerPrefs.SetInt("DeskCard3", 1);
        PlayerPrefs.SetInt("DeskCard4", 1);
        PlayerPrefs.SetInt("DeskCard5", 1);
        PlayerPrefs.SetInt("DeskCard6", 1);
        PlayerPrefs.SetInt("DeskCard7", 1);
        PlayerPrefs.SetInt("DeskCard8", 1);
        PlayerPrefs.SetInt("DeskCard9", 1);
        PlayerPrefs.SetInt("DeskCard10", 1);
        PlayerPrefs.SetInt("DeskCard11", 1);
        PlayerPrefs.SetInt("DeskCard12", 1);
        PlayerPrefs.SetInt("DeskCard13", 4);
        PlayerPrefs.SetInt("DeskCard14", 4);
        PlayerPrefs.SetInt("DeskCard15", 4);
        PlayerPrefs.SetInt("DeskCard16", 2);
        PlayerPrefs.SetInt("DeskCard17", 2);
        PlayerPrefs.SetInt("DeskCard18", 2);
        PlayerPrefs.SetInt("DeskCard19", 2);
        PlayerPrefs.SetInt("DeskCard20", 2);
        PlayerPrefs.SetInt("DeskCard21", 2);
        PlayerPrefs.SetInt("DeskCard22", 2);
        PlayerPrefs.SetInt("DeskCard23", 2);
        PlayerPrefs.SetInt("DeskCard24", 3);
        PlayerPrefs.SetInt("DeskCard25", 3);
        PlayerPrefs.SetInt("DeskCard26", 3);
        PlayerPrefs.SetInt("DeskCard27", 3);
        PlayerPrefs.SetInt("DeskCard28", 1);
        PlayerPrefs.SetInt("DeskCard29", 1);
        PlayerPrefs.SetInt("DeskCard30", 1);

        //CardsVault
        PlayerPrefs.SetString("CardsVaultInfo","1,0|2,0|3,0|4,0");


    	//Change Scene
    	SceneManager.LoadScene("TownMenu");
    }

    public void Continue(){
    	SceneManager.LoadScene("TownMenu");
    }

    public void Exit(){
    	Application.Quit();
    }
}
