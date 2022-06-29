using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk
{
	//Values
	public int cardsLimit = 30;
	List<Card> cards = new List<Card>();

	//Methods
	public void AddCard(Card newCard){
		if(cards.Count <cardsLimit){
			cards.Add(newCard);
		}
	}

	public void RemoveCard(Card oldCard){
		if(cards.Count > 0){
			cards.Remove(oldCard);
		}
	}

	public List<Card> GetCards(){
		return cards;
	}

	public void RemoveAllCard(){
		if(cards.Count > 0){
			cards = new List<Card>();
		}
	}
}
