using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownUIController : MonoBehaviour
{
    //Objects
    public GameObject playerPanel;
    public GameObject cardsPanel;
    public GameObject trainerPanel;
    public GameObject shopPanel;
    public GameObject GM;

    //Methods
    public void Exit(){
    	GM.GetComponent<TownDataController>().SaveData();
    	SceneManager.LoadScene("MainMenu");
    }
    public void Play(){
        if(TownDesk.instance.desk.Count == 30){
            GM.GetComponent<TownDataController>().SaveData();
            SceneManager.LoadScene("Catacomb");
        }
    }

    public void OpenPlayerPanel(){
    	playerPanel.SetActive(true);
    }
    public void OpenCardsPanel(){
        cardsPanel.SetActive(true);
    }
    public void OpenTrainerPanel(){
        trainerPanel.SetActive(true);
    }
    public void OpenShopPanel(){
        shopPanel.SetActive(true);
    }
}
