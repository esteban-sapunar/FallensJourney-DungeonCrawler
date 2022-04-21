using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	//Health
	public int maxHealth = 30;
	public int currentHealth {get; private set;}
    public int maxStamina = 50;
    public int currentStamina {get; private set;}
	//Stats
    public Stat attack;
    public Stat armor;
    public Stat defense;
    public Stat weight;
    //Stats UI
    public Text attackUI;
    public Text armorUI;
    public Text defenseUI;
    public Text weightUI;

    //Base Functions
    void Awake(){
    	currentHealth = maxHealth;
    }
    void Start(){
        EquipmentManager.instance.OnEquipChangedCallback += OnEquipHasChanged;
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

    //Equipment
    void OnEquipHasChanged (Equipment newItem,Equipment oldItem){
        if(newItem != null){
            attack.AddModifier(newItem.attackModifier);
            armor.AddModifier(newItem.armorModifier);
            defense.AddModifier(newItem.defenseModifier);
            weight.AddModifier(newItem.weightModifier);
        }
        if(oldItem != null){
            attack.RemoveModifier(oldItem.attackModifier);
            armor.RemoveModifier(oldItem.armorModifier);
            defense.RemoveModifier(oldItem.defenseModifier);
            weight.RemoveModifier(oldItem.weightModifier);
        }
        attackUI.text = attack.GetValue().ToString();
        armorUI.text = armor.GetValue().ToString();
        defenseUI.text = defense.GetValue().ToString();
        weightUI.text = weight.GetValue().ToString();
    }

}
