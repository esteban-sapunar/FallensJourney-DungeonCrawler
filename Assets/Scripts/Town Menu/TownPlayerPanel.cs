using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownPlayerPanel : MonoBehaviour
{
	public GameObject GM;
	public GameObject player;
    public void Close(){
    	gameObject.SetActive(false);
    }
    public void LevelUp(){
    	if(player.GetComponent<TownPlayerStats>().exp >= player.GetComponent<TownPlayerStats>().expNextLevel){
    		player.GetComponent<TownPlayerStats>().LevelUp();
    	}
    }
    public void StrengthUp(){
    	if(player.GetComponent<TownPlayerStats>().points > 0){
    		player.GetComponent<TownPlayerStats>().StrengthUp();
    	}
    }
    public void ResistanceUp(){
    	if(player.GetComponent<TownPlayerStats>().points > 0){
    		player.GetComponent<TownPlayerStats>().ResistanceUp();
    	}
    }
    public void VitalityUp(){
    	if(player.GetComponent<TownPlayerStats>().points > 0){
    		player.GetComponent<TownPlayerStats>().VitalityUp();
    	}
    }
}
