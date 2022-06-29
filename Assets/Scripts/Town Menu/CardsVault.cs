using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsVault : MonoBehaviour
{
//Delegate
	public delegate void OnCardVaultChanged(Card card,bool add);
	public OnCardVaultChanged OnCardVaultChangedCallback;

	//Values
	public List<Card> cards = new List<Card>();
	
	public static CardsVault instance;
	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of CardsVault found!");
			return;
		}
		instance = this;
	}


	//Methods
	public bool Add (Card newCard){
			cards.Add(newCard);
			if(OnCardVaultChangedCallback != null){
				OnCardVaultChangedCallback(newCard,true);
			}
			return true;		
	}

	public void Remove (Card card){
		cards.Remove(card);
		if(OnCardVaultChangedCallback != null){
			OnCardVaultChangedCallback(card,false);	
		}
	}

	public int CountCard(Card card){
		int count = 0;
		for(int i =0; i < cards.Count;i++){
			if(cards[i] == card){
				count+=1;
			}
		}
		return count;
	}
}
