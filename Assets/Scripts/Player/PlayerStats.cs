using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	//Health
	public int maxHealth = 30;
	public int currentHealth {get; private set;}
	//Stats
    public Stat damage;
    public Stat armor;

    //Base Functions
    void Awake(){
    	currentHealth = maxHealth;
    }
    void Update(){
    	if(Input.GetKeyDown(KeyCode.T)){
    		Damage(5);
    	}
    }

    //Methods
    public void Damage (int damage){
    	int finalDamage = damage - armor.GetValue();
    	if(finalDamage < 0){
    		finalDamage = 0;
    	}
    	Debug.Log(transform.name+ " takes "+finalDamage+" damage.");
    	currentHealth -= finalDamage;
    	if(currentHealth <= 0){
    		currentHealth = 0;
    		Die();
    	}
    }

    public void Die(){
    	//death
    	Debug.Log(transform.name+ " died.");
    }

}
