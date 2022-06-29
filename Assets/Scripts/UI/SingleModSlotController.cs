using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleModSlotController : MonoBehaviour
{
    //Values
    public Text name;
    public Text number;

    //Methods
    public void SetMod(int modNumber){
    	number.text = modNumber.ToString();
    }

}
