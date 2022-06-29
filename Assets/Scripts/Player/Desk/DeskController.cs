using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskController : MonoBehaviour
{
	//Values
	Desk gameDesk = new Desk();

	//Methods
	public Desk GetBaseDesk(){
		return gameDesk;
	}

	public void AddCard(Card newCard){
		gameDesk.AddCard(newCard);
	}
}
