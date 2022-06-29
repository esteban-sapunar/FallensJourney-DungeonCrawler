using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatacombController : MonoBehaviour
{	
	//Values
	public Transform slotsParent;

	//Basic Functions
	void Awake(){
		for(int i =0; i < slotsParent.childCount;i++){
			GameObject slot = slotsParent.GetChild(i).gameObject;
			slot.SetActive(false);
			if(PlayerPrefs.GetInt("Stage") >= i){
				slot.SetActive(true);
			}
		}
	}
    //Methods
    public void Back(){
    	SceneManager.LoadScene("TownMenu");
    }
}
