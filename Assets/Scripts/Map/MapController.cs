using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
	//Singleton
	public static MapController instance;

    //Map UI
    public GameObject MapPanel;
    List <GameObject> Tiles =new List<GameObject>();

    //Map
    public Tile nullTile;
    public Tile currentTile;
    public Tile pathTile;
    public Tile bossTile;
    public List <Tile> initialTiles;
    public Tile [,] mapArray = new Tile[15,7];

    //Base Functions

    void Awake(){
    	if(instance != null){
			Debug.LogWarning("More than one instace of Map Controller found!");
			return;
		}
		instance = this;

		for(int i =0; i < MapPanel.transform.childCount ;i++){
			Tiles.Add(MapPanel.transform.GetChild(i).gameObject);
		}

    	for(int i =0; i < 7;i++){
    		for(int j =0; j < 15;j++){
    			SetTile(j,i,nullTile);
    		}
    	}
    	GenerateMap();
    }

    //Methods

    void SetTile(int x,int y, Tile newTile){
    	mapArray[x,y] = newTile;
    	int index = (y*15)+x;
    	GameObject tileSlot = MapPanel.transform.GetChild(index).gameObject;
    	tileSlot.GetComponent<Image>().sprite = newTile.sprite;
    	tileSlot.GetComponent<TileSlot>().SetTile(x,y,newTile);
    }

    void GenerateMap(){
    	SetTile(0,3,currentTile);
    	SetTile(1,3,pathTile);
    	UseSlot(0,3,currentTile);
    	int x = 1;
    	int y = 3;
    	for(int i =0; i < 15;i++){
    		Tile tile = initialTiles[Random.Range(0,initialTiles.Count)];
			int dir = Random.Range(0,3);
			switch (dir) {
				case 0:
					if(y <= 5){
						if((int)mapArray[x,y+1].tileType == 0 && (int)mapArray[x-1,y+1].tileType == 0){
							y+=1;
							SetTile(x,y,tile);
						}
						else{
							if(x < 14){
								x+=1;
								SetTile(x,y,tile);
							}
						}
					}
					else{
						if(x < 14){
							x+=1;
							SetTile(x,y,tile);
						}
					}
				break;
				case 1:
					if(x < 14){
						x+=1;
						SetTile(x,y,tile);
					}
				break;
				case 2:
					if(y > 0){
						if((int)mapArray[x,y-1].tileType == 0 && (int)mapArray[x-1,y-1].tileType == 0){
							y-=1;
							SetTile(x,y,tile);
						}
						else{
							if(x < 14){
								x+=1;
								SetTile(x,y,tile);
							}
						}
					}
					else{
						if(x < 14){
							x+=1;
							SetTile(x,y,tile);
						}
					}
				break;
			}
		}
		SetTile(x,y,bossTile);
    }

    public void UseSlot(int x,int y, Tile usedTile){
    	SetTile(x,y,currentTile);
    	foreach(GameObject tile in Tiles){
    		tile.GetComponent<Button>().interactable = false;
    	}
    	if(x < 14){
	    	if((int)mapArray[x+1,y].tileType != 0){
	    		MapPanel.transform.GetChild((y*15)+x+1).gameObject.GetComponent<Button>().interactable = true;
	    	}
	    	if((int)mapArray[x+1,y].tileType == 1){
	    		SetTile(x+1,y,pathTile);
	    	}	
    	}
    	if(x > 0){ 	
	    	if((int)mapArray[x-1,y].tileType != 0){
	    		MapPanel.transform.GetChild((y*15)+x-1).gameObject.GetComponent<Button>().interactable = true;
	    	}
	    	if((int)mapArray[x-1,y].tileType == 1){
	    		SetTile(x-1,y,pathTile);
	    	}
    	}
    	if(y < 6){ 
	    	if((int)mapArray[x,y+1].tileType != 0){
	    		MapPanel.transform.GetChild(((y+1)*15)+x).gameObject.GetComponent<Button>().interactable = true;
	    	}
	    	if((int)mapArray[x,y+1].tileType == 1){
	    		SetTile(x,y+1,pathTile);
	    	}
   	 	}
   	 	if(y > 0){ 
	    	if((int)mapArray[x,y-1].tileType != 0){
	    		MapPanel.transform.GetChild(((y-1)*15)+x).gameObject.GetComponent<Button>().interactable = true;
	    	}
	    	if((int)mapArray[x,y-1].tileType == 1){
	    		SetTile(x,y-1,pathTile);
	    	}
    	}
    	switch ((int)usedTile.tileType) {
    		case 3:
    			BattleController.instance.StartBattle();
    		break;
    		case 4:
    			BattleController.instance.DropItem(300);
    		break;
    		case 5:
    			BattleController.instance.StartFinalBattle();
    		break;
    		default:
    		break;
    	}
    }
}
