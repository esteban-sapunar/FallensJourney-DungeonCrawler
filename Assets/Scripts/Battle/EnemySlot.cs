using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlot : MonoBehaviour
{
    //Values
    public Enemy enemy;
    public int currentHealth;
    public int maxHealth;
    public EnemyAction currentAction;
    public int damageTaken = 0;
    public int blockAmount = 0;
    public GameObject BC;

    //UI
    public Text currentHealthNumber;
    public Text maxHealthNumber;
    public Slider healthSlider;
    public Animator anim;
    public GameObject damageEffect;
    public GameObject blockEffect;

	public Image enemyIcon;
	public Text armor;
	public Image actionIcon;
	public Text actionValue;
	public Sprite attack;
	public Sprite defense;
	public Sprite healing;

	public void SetEnemy(Enemy newEnemy){
		enemy = newEnemy;
		maxHealth = enemy.health;
		currentHealth = enemy.health;
		enemyIcon.sprite = enemy.sprite;
		armor.text = enemy.armor.ToString();
		maxHealthNumber.text = enemy.health.ToString();
		currentHealthNumber.text = enemy.health.ToString();
		healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        currentAction = enemy.actions[Random.Range(0,enemy.actions.Count)];
        actionValue.text = currentAction.value.ToString();
        switch ((int)currentAction.type){
        	case 0:
        		actionIcon.sprite = attack;
        	break;

        	case 1:
        		actionIcon.sprite = defense;
                blockAmount = currentAction.value;
        	break;

        	case 2:

        	break;
        }
	}

    public void TakeDamage(int dmg){
        int damage = dmg - enemy.armor;
        if(damage > 0){
            if((int)currentAction.type == 1){
                if(blockAmount >= damage){
                    blockAmount -= damage;
                    damage = 0;
                    StartCoroutine(BlockEffect());
                    anim.SetTrigger("Slash");
                }
                else{
                    damage -= blockAmount;
                    blockAmount = 0;
                    if(currentHealth <= damage){
		                currentHealth = 0;
		                currentHealthNumber.text = currentHealth.ToString();
		                healthSlider.value = currentHealth;
		                anim.SetTrigger("Slash");
		                StartCoroutine(DamageEffect());
		                Die();
		            }
		            else{
		                currentHealth -= damage;  
		                currentHealthNumber.text = currentHealth.ToString();
		                healthSlider.value = currentHealth;
		                anim.SetTrigger("Slash");
		                StartCoroutine(DamageEffect());
		            }
                }
                actionValue.text = blockAmount.ToString();
            }
            else{
            	if(currentHealth <= damage){
	                currentHealth = 0;
	                currentHealthNumber.text = currentHealth.ToString();
	                healthSlider.value = currentHealth;
	                anim.SetTrigger("Slash");
	                StartCoroutine(DamageEffect());
	                Die();
	            }
	            else{
	                currentHealth -= damage;  
	                currentHealthNumber.text = currentHealth.ToString();
	                healthSlider.value = currentHealth;
	                anim.SetTrigger("Slash");
	                StartCoroutine(DamageEffect());
	            }
            }
        }

    }

    public void Die(){
        damageEffect.SetActive(false);
        blockEffect.SetActive(false);
        BC.GetComponent<BattleController>().EnemyDied(enemy.exp,enemy.gold,enemy.dropPercent);
        enemy = null;
        gameObject.SetActive(false);
    }

    public void TakeDamageFromCard(){
        TakeDamage(damageTaken);
        Transform enemies = gameObject.transform.parent;
        BC.GetComponent<BattleController>().BlockCards(false);
        foreach(Transform child in enemies.transform){
            child.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        }        
    }

    public void EnemyTurn(){
        currentAction = enemy.actions[Random.Range(0,enemy.actions.Count)];
        actionValue.text = currentAction.value.ToString();
        switch ((int)currentAction.type){
            case 0:
                actionIcon.sprite = attack;
            break;

            case 1:
                actionIcon.sprite = defense;
                blockAmount = currentAction.value;
            break;

            case 2:

            break;
        }
    }

    IEnumerator DamageEffect(){
    	damageEffect.SetActive(true);
    	yield return new WaitForSeconds(1f);
    	damageEffect.SetActive(false);
    }

    IEnumerator BlockEffect(){
    	blockEffect.SetActive(true);
    	yield return new WaitForSeconds(1f);
    	blockEffect.SetActive(false);
    }
}
