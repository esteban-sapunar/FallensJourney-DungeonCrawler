using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownPlayerStats : MonoBehaviour
{
	//Health
	public int maxHealth = 50;
	public int currentHealth;
    public int baseStamina = 25;
    public int maxStamina = 50;
    public int currentStamina;
	//Stats
    public Stat attack;
    public Stat armor;
    public Stat defense;
    public Stat weight;
    public int level;
    public int exp;
    public int expNextLevel;
    public int gold;

    public int strength;
    public int resistance;
    public int vitality;
    public int points; 
    //Battle Stats
    public int blockAmount;
    //Stats UI
    public Text attackUI;
    public Text armorUI;
    public Text defenseUI;
    public Text weightUI;
    public Slider staminaSlider;
    public Slider healthSlider;
    public Text currentStaminaNumber;
    public Text maxStaminaNumber;
    public Text currentHealthNumber;
    public Text maxHealthNumber;

    public Text expNumber;
    public Text goldNumber;
    public Text trainerGoldNumber;
    public Text shopGoldNumber;
    public Text levelNumber;
    public Text nextLevelNumber;
    public Text pointsNumber;
    public Text strengthNumber;
    public Text resistanceNumber;
    public Text vitalityNumber;
    //Delegate
    public delegate void OnPlayerDie();
    public OnPlayerDie OnPlayerDieCallback;

    //Methods
    public void InitData(){
        EquipmentManager.instance.OnEquipChangedCallback += OnEquipHasChanged;
        currentHealth = maxHealth;
        maxStamina = baseStamina;
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        maxStaminaNumber.text = maxStamina.ToString();
        currentStaminaNumber.text = maxStamina.ToString();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        maxHealthNumber.text = maxHealth.ToString();
        currentHealthNumber.text = maxHealth.ToString();
        expNextLevel = 10*level +10*(level-1)*(level-1);
        levelNumber.text = level.ToString();
        expNumber.text = exp.ToString();
        nextLevelNumber.text = expNextLevel.ToString();
        goldNumber.text = gold.ToString();
        trainerGoldNumber.text = gold.ToString();
        shopGoldNumber.text = gold.ToString();
        pointsNumber.text = points.ToString();
        strengthNumber.text = strength.ToString();
        resistanceNumber.text = resistance.ToString();
        vitalityNumber.text = vitality.ToString();
        attack.AddModifier(strength);
    }

    public int GetStatValue(string type){
        switch (type){
            case "attack":
                return attack.GetValue();
                break;
            case "defense":
                return defense.GetValue();
                break;
            case "armor":
                return armor.GetValue();
                break;
            case "weight":
                return weight.GetValue();
                break;
            default:
                return 0;
                break;
        }
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
        maxStamina = baseStamina - (int)Mathf.Round(weight.GetValue()/2f);
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        maxStaminaNumber.text = maxStamina.ToString();
        currentStaminaNumber.text = maxStamina.ToString();
    }

    //Stats
    public void LevelUp(){
    	level +=1;
		exp -= expNextLevel;
		points +=1;
		expNextLevel = 10*level +10*(level-1)*(level-1);
        levelNumber.text = level.ToString();
        expNumber.text = exp.ToString();
        nextLevelNumber.text = expNextLevel.ToString();
        pointsNumber.text = points.ToString();
    }

    public void StrengthUp(){
    	attack.RemoveModifier(strength);
    	strength += 1;
    	points -= 1;
    	attack.AddModifier(strength);
    	strengthNumber.text = strength.ToString();
    	pointsNumber.text = points.ToString();
    	attackUI.text = attack.GetValue().ToString();
    }
    public void ResistanceUp(){
    	resistance += 1;
    	points -= 1;
    	baseStamina = 25 +resistance*2;
    	resistanceNumber.text = resistance.ToString();
    	pointsNumber.text = points.ToString();
    	maxStamina = baseStamina - (int)Mathf.Round(weight.GetValue()/2f);
    	currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        maxStaminaNumber.text = maxStamina.ToString();
        currentStaminaNumber.text = maxStamina.ToString();
    }
    public void VitalityUp(){
    	vitality += 1;
    	points -= 1;
    	maxHealth = 50 + vitality*5;
    	vitalityNumber.text = vitality.ToString();
    	pointsNumber.text = points.ToString();
    	healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        maxHealthNumber.text = maxHealth.ToString();
        currentHealthNumber.text = maxHealth.ToString();
    }
    public void PayGold(int price){
        if(price <= gold){
            gold -= price;
            goldNumber.text = gold.ToString();
            trainerGoldNumber.text = gold.ToString();
            shopGoldNumber.text = gold.ToString();
        }
    }
    public void EarnGold(int earnings){
            gold += earnings;
            goldNumber.text = gold.ToString();
            trainerGoldNumber.text = gold.ToString();
            shopGoldNumber.text = gold.ToString();
    }
}

