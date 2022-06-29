using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDesk : MonoBehaviour
{
    //Delegate
	public delegate void OnCardChanged();
	public OnCardChanged OnCardChangedCallback;

	//Values
	public int space = 30;
	public List<Card> desk = new List<Card>();
	
	public static TownDesk instance;
	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of TownDesk found!");
			return;
		}
		instance = this;
	}


	//Methods
	public bool Add (Card newCard){
		if(desk.Count < space){
			desk.Add(newCard);
			if(OnCardChangedCallback != null){
				OnCardChangedCallback.Invoke();
			}
			return true;
		}
		else{
			Debug.Log("Not Enough Space");
			return false;
		}
		
	}

	public void Remove (Card card){
		desk.Remove(card);
		if(OnCardChangedCallback != null){
			OnCardChangedCallback.Invoke();	
		}
	}
}
