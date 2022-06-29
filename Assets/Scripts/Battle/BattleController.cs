using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
	//Singleton
	public static BattleController instance;

	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of Battle Controller found!");
			return;
		}
		instance = this;
	}

	//Desks
	Desk baseDesk;
	Desk activeDesk;
	Desk discardDesk;

	//Player
	public GameObject player;
	int currentExp = 0;
	int currentGold = 0;
	public int earnedExp = 0;
	public int earnedGold = 0;
	int dropPercent = 0;
	//Enemies
	public List <Enemy> enemies;
	public List <Enemy> bosses;

	//Card Slots
	public List <GameObject> cardSlots;

	//Enemy Slots
	public List <GameObject> enemySlots;

	//Items for level
	public List <Item> levelItems;

	//Values
	bool onBattle = false;
	bool onPlayerTurn = false;
	bool onFinalBattle = false;

	//UI
	public Text deskCount;
	public Text discardCount;
	public GameObject actionsPanel;
	public Text turnText;
	public Slider healthSlider;
    public Text currentHealthNumber;
    public Text maxHealthNumber;

    public Text expNumber;
    public Text goldNumber;

	//Base Functions
	void Start(){
		healthSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;
        healthSlider.value = player.GetComponent<PlayerStats>().maxHealth;
        maxHealthNumber.text = player.GetComponent<PlayerStats>().maxHealth.ToString();
        currentHealthNumber.text = player.GetComponent<PlayerStats>().maxHealth.ToString();
		baseDesk = gameObject.GetComponent<DeskController>().GetBaseDesk();
		activeDesk = baseDesk;
		discardDesk = new Desk();
		player.GetComponent<PlayerStats>().OnPlayerDieCallback += LostBattle;
		expNumber.text = earnedExp.ToString();
        goldNumber.text = earnedGold.ToString();
	}

	//Methods
	public bool OnBattle(){
		return onBattle;
	}

	public bool OnPlayerTurn(){
		return onPlayerTurn;
	}

	public void DiscardCard(Card discardCard){
		List<Card> discardcards = discardDesk.GetCards();
		discardDesk.AddCard(discardCard);
		discardCount.text = discardcards.Count.ToString();
	}

	public void DrawCard(GameObject slot){
		List<Card> activecards = activeDesk.GetCards();
		List<Card> discardcards = discardDesk.GetCards();
		if(activecards.Count > 0){
			Card randCard = activecards[Random.Range(0,activecards.Count)];
			activeDesk.RemoveCard(randCard);
			deskCount.text = activecards.Count.ToString();
			slot.GetComponent<CardSlot>().SetCard(randCard);
		}
		else{
			foreach(Card card in discardcards){
				activeDesk.AddCard(card);
			}
			discardDesk.RemoveAllCard();
			discardcards = discardDesk.GetCards();
			discardCount.text = discardcards.Count.ToString();
			activecards = activeDesk.GetCards();
			Card randCard = activecards[Random.Range(0,activecards.Count)];
			activeDesk.RemoveCard(randCard);
			deskCount.text = activecards.Count.ToString();
			slot.GetComponent<CardSlot>().SetCard(randCard);
		}
	}
	public void UpdateCardsNumbers(){
		for(int i =0; i < cardSlots.Count;i++){
			if(cardSlots[i].active){
				cardSlots[i].GetComponent<CardSlot>().UpdateCard();
			}
		}
	}

	public void StartBattle(){
		onBattle = true;
		actionsPanel.SetActive(true);
		//Generate enemies
		SetEnemies();
		//Start Turn
		StartPlayerTurn();
	}
	public void StartFinalBattle(){
		onBattle = true;
		onFinalBattle = true;
		actionsPanel.SetActive(true);
		//Generate enemies
		SetFinalEnemies();
		//Start Turn
		StartPlayerTurn();
	}

	public void DropItem(int drop){
		dropPercent += drop;
		int dropratio = (int)Mathf.Round(dropPercent/3f);
		int randomN = Random.Range(0,100);
		if(randomN <= dropratio){
			Inventory.instance.Add(levelItems[Random.Range(0,levelItems.Count)]);
		}
		dropPercent += 0;
	}

	public void EndBattle(){
		onBattle = false;
		DropItem(0);
		for(int i =0; i < cardSlots.Count;i++){
			if(cardSlots[i].active){
				activeDesk.AddCard(cardSlots[i].GetComponent<CardSlot>().GetCard());
				cardSlots[i].GetComponent<CardSlot>().UnsetCard();
				cardSlots[i].SetActive(false);
			}
		}
		List<Card> discardcards = discardDesk.GetCards();
		foreach(Card card in discardcards){
			activeDesk.AddCard(card);
		}
		discardDesk.RemoveAllCard();
		discardcards = discardDesk.GetCards();
		List<Card> activecards = activeDesk.GetCards();
		deskCount.text = activecards.Count.ToString();
		discardCount.text = discardcards.Count.ToString();
        healthSlider.value = player.GetComponent<PlayerStats>().currentHealth;
        currentHealthNumber.text = player.GetComponent<PlayerStats>().currentHealth.ToString();
        earnedExp += currentExp;
        earnedGold += currentGold;
        currentExp = 0;
        currentGold = 0;
        dropPercent = 0;
        expNumber.text = earnedExp.ToString();
        goldNumber.text = earnedGold.ToString();
        if(onFinalBattle == true){
        	gameObject.GetComponent<DataController>().OpenRewardPanel("win");
        }
		actionsPanel.SetActive(false);	
	}

	public void LostBattle(){
		onBattle = false;
		for(int i =0; i < cardSlots.Count;i++){
			if(cardSlots[i].active){
				activeDesk.AddCard(cardSlots[i].GetComponent<CardSlot>().GetCard());
				cardSlots[i].GetComponent<CardSlot>().UnsetCard();
				cardSlots[i].SetActive(false);
			}
		}
		List<Card> discardcards = discardDesk.GetCards();
		foreach(Card card in discardcards){
			activeDesk.AddCard(card);
		}
		discardDesk.RemoveAllCard();
		discardcards = discardDesk.GetCards();
		List<Card> activecards = activeDesk.GetCards();
		deskCount.text = activecards.Count.ToString();
		discardCount.text = discardcards.Count.ToString();
		player.GetComponent<PlayerStats>().Heal(player.GetComponent<PlayerStats>().maxHealth);
        healthSlider.value = player.GetComponent<PlayerStats>().currentHealth;
        currentHealthNumber.text = player.GetComponent<PlayerStats>().currentHealth.ToString();
        currentExp = 0;
        currentGold = 0;
        dropPercent = 0;
        gameObject.GetComponent<DataController>().OpenRewardPanel("death");
	}
	public void SetEnemies(){
		foreach(GameObject slot in enemySlots){
			slot.SetActive(true);
			slot.GetComponent<EnemySlot>().SetEnemy(enemies[Random.Range(0,enemies.Count)]);
		}
	}
	public void SetFinalEnemies(){
		foreach(GameObject slot in enemySlots){
			slot.SetActive(true);
			slot.GetComponent<EnemySlot>().SetEnemy(enemies[Random.Range(0,enemies.Count)]);
		}
		enemySlots[1].GetComponent<EnemySlot>().SetEnemy(bosses[Random.Range(0,bosses.Count)]);
	}
	public void EnemyDied(int exp,int gold,int dropPerc){
		currentExp += exp;
		currentGold += gold;
		dropPercent += dropPerc;
		Debug.Log("enemy count: "+CountEnemies());
		if(CountEnemies() <= 1){
			EndBattle();
		}
	}
	public void StartPlayerTurn(){
		onPlayerTurn = true;
		player.GetComponent<PlayerStats>().ResetStamina();
		player.GetComponent<PlayerStats>().ResetBlock();
		for(int i =0; i < cardSlots.Count;i++){
			if(!cardSlots[i].active){
				cardSlots[i].SetActive(true);
				DrawCard(cardSlots[i]);
			}
			cardSlots[i].GetComponent<Button>().interactable = true;
		}
		turnText.GetComponent<TemporalText>().SetText();
	}
	public void EndPlayerTurn(){
		onPlayerTurn = false;
		foreach(GameObject slot in enemySlots){
			if(slot.active){
				slot.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
			}
		}
		BlockCards(true);
		StartCoroutine(EnemiesTurns());
	}

	public void AttackOneEnemy(int dmg){
		foreach(GameObject slot in enemySlots){
			if(slot.active){
				slot.GetComponent<EnemySlot>().damageTaken = dmg;
				slot.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
			}
		}
		BlockCards(true);
	}
	public int CountEnemies(){
		int count =0;
		for(int i =0; i < enemySlots.Count;i++){
			if(enemySlots[i].active){
				count +=1;
			}
		}
		return count;
	}
	public void BlockCards(bool isBlock){
		if(isBlock){
			for(int i =0; i < cardSlots.Count;i++){
				if(cardSlots[i].active){
					cardSlots[i].GetComponent<Button>().interactable = false;
				}
			}
		}
		else{
			for(int i =0; i < cardSlots.Count;i++){
				if(cardSlots[i].active){
					cardSlots[i].GetComponent<Button>().interactable = true;
				}
			}
		}
	}

	//inumerators
	IEnumerator EnemiesTurns(){
		for(int i =0; i < enemySlots.Count;i++){
			if(enemySlots[i].active){
				yield return new WaitForSeconds(1f);
    			switch ((int)enemySlots[i].GetComponent<EnemySlot>().currentAction.type){
		            case 0:
		                player.GetComponent<PlayerStats>().Damage(enemySlots[i].GetComponent<EnemySlot>().currentAction.value);
		            break;

		            case 1:

		            break;

		            case 2:

		            break;
		        }
		        enemySlots[i].GetComponent<EnemySlot>().EnemyTurn();
			}
		}
		yield return new WaitForSeconds(1f);
		StartPlayerTurn();
    }

}
