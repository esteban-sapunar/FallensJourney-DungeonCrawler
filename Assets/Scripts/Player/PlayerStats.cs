using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	//Health
	public int maxHealth = 50;
	public int currentHealth {get; private set;}
    public int baseStamina = 25;
    public int maxStamina = 50;
    public int currentStamina {get; private set;}
	//Stats
    public Stat attack;
    public Stat armor;
    public Stat defense;
    public Stat weight;
    public int expGained {get; private set;}
    public int strength;
    //Battle Stats
    public int blockAmount;
    //Stats UI
    public Text attackUI;
    public Text armorUI;
    public Text defenseUI;
    public Text weightUI;
    public Slider staminaSlider;
    public Slider healthSlider;
    public Slider healthSliderMenu;
    public Text currentStaminaNumber;
    public Text maxStaminaNumber;
    public Text currentHealthNumber;
    public Text maxHealthNumber;
    public Text currentHealthNumberMenu;
    public Text maxHealthNumberMenu;
    public Text blockNumber;
    public Text armorNumber;
    //UI
    public GameObject damageEffect;
    public GameObject blockEffect;
    public GameObject healingEffect;
    //Delegate
    public delegate void OnPlayerDie();
    public OnPlayerDie OnPlayerDieCallback;

    #region Singleton
    public static PlayerStats instance;
    void Awake(){
        if(instance != null){
            Debug.LogWarning("More than one instace of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

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
        attack.AddModifier(strength);
        attackUI.text = attack.GetValue().ToString();
    }

    public void GainExp(int exp){
        expGained += exp;
    }
    public void Heal(int healing){
        if(maxHealth-currentHealth > healing){
            currentHealth += healing;
        }
        else{
            currentHealth = maxHealth;
        }
        StartCoroutine(HealingEffect());
        healthSlider.value = currentHealth;
        currentHealthNumber.text = currentHealth.ToString(); 
        healthSliderMenu.value = currentHealth;
        currentHealthNumberMenu.text = currentHealth.ToString(); 
    }
    public void UseStamina(int cost){
        currentStamina -= cost;
        staminaSlider.value = currentStamina;
        currentStaminaNumber.text = currentStamina.ToString();
    }
    public void ResetBlock(){
        blockAmount = 0;
        blockNumber.text = blockAmount.ToString();
    }
    public void ResetStamina(){
        currentStamina = maxStamina;
        staminaSlider.value = currentStamina;
        currentStaminaNumber.text = currentStamina.ToString();
    }
    public void Damage(int damage){
    	int finalDamage = damage - armor.GetValue();
    	if(finalDamage < 0){
    		finalDamage = 0;
    	}
        if(blockAmount > 0){
            if(blockAmount <= finalDamage){
                finalDamage -= blockAmount;
                blockAmount = 0;
            }
            else{
                blockAmount -= finalDamage;
                finalDamage = 0;
            }
        }
        if(finalDamage > 0){
            StartCoroutine(DamageEffect());
        }
        else{
            StartCoroutine(BlockEffect());
        }
        blockNumber.text = blockAmount.ToString();
    	currentHealth -= finalDamage;
        healthSlider.value = currentHealth;
        currentHealthNumber.text = currentHealth.ToString(); 
    	if(currentHealth <= 0){
    		currentHealth = 0;
    		Die();
    	}
    }

    public void Die(){
    	damageEffect.SetActive(false);
    	Debug.Log(transform.name+ " died.");
        OnPlayerDieCallback.Invoke();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        maxHealthNumber.text = maxHealth.ToString();
        currentHealthNumber.text = maxHealth.ToString();
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
        armorNumber.text = armor.GetValue().ToString();
        defenseUI.text = defense.GetValue().ToString();
        weightUI.text = weight.GetValue().ToString();
        maxStamina = baseStamina - (int)Mathf.Round(weight.GetValue()/2f);
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        maxStaminaNumber.text = maxStamina.ToString();
        currentStaminaNumber.text = maxStamina.ToString();
    }

    IEnumerator DamageEffect(){
        damageEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        damageEffect.SetActive(false);
    }
    IEnumerator BlockEffect(){
        blockEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        blockEffect.SetActive(false);
    }
    IEnumerator HealingEffect(){
        healingEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        healingEffect.SetActive(false);
    }

}
