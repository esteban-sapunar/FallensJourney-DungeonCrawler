using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSlot : MonoBehaviour
{
    //Values
    public string stage;

    //Methods
    public void EnterStage(){
    	SceneManager.LoadScene(stage);
    }
}
