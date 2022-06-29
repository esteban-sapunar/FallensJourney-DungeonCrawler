using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSlot : MonoBehaviour
{
    //Values
    public Tile tile;
    public Vector2 pos = new Vector2();

    //Methods
    public void UseTile(){
    	MapController.instance.UseSlot((int)pos.x,(int)pos.y,tile);
    }

    public void SetTile(int x,int y, Tile newTile){
    	tile = newTile;
    	pos.x = x;
    	pos.y =y;
    }

}
