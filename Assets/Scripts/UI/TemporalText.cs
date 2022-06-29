using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalText : MonoBehaviour
{
    public void SetText(){
    	gameObject.SetActive(true);
    	StartCoroutine(SetOffText());
    }
    IEnumerator SetOffText(){
    	yield return new WaitForSeconds(1.5f);
    	gameObject.SetActive(false);
    }
}
