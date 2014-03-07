using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;

	public List<Tile> neighbors = new List<Tile>();

	public int movementCost = 1;
	public bool impassible = false;

	// Use this for initialization
	void Start () 
	{
		generateNeighbors();
	}

	void generateNeighbors()
	{
		neighbors = new List<Tile>();
		//up
		if(gridPosition.y > 0)
		{
			Vector2 n = new Vector2(gridPosition.x, gridPosition.y - 1);
			neighbors.Add (GameManager.instance.map[(int)n.x][(int)n.y]);
		}
		//down
		if(gridPosition.y < GameManager.instance.map.Count - 1)
		{
			Vector2 n = new Vector2(gridPosition.x , gridPosition.y + 1);
			neighbors.Add (GameManager.instance.map[(int)n.x][(int)n.y]);
		}
		//left
		if(gridPosition.x > 0)
		{
			Vector2 n = new Vector2(gridPosition.x - 1, gridPosition.y);
			neighbors.Add (GameManager.instance.map[(int)n.x][(int)n.y]);
		}
		//right
		if(gridPosition.x < GameManager.instance.map.Count - 1)
		{
			Vector2 n = new Vector2(gridPosition.x + 1, gridPosition.y);
			neighbors.Add (GameManager.instance.map[(int)n.x][(int)n.y]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter()
	{
		// video 2 inicio
		/*
		if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].moving) 
		{
			changeColor(Color.blue);
		} 
		else if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].attacking) 
		{
			changeColor(Color.red);
		}
		*/
		//video 2 final

		//changeColor (Color.blue);
		//print (gridPosition+" e "+new Vector3(transform.position.x, transform.position.y, transform.position.z));
	}

	void OnMouseExit(){
		//changeColor(Color.white);
	}

	void OnMouseDown()
	{
		//video 2 inicio
		if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].moving) 
		{
			GameManager.instance.moveCurrentPlayer(this);
		} 
		else if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].attacking) 
		{
			GameManager.instance.attackCurrentPlayer(this);
		}
		else//(Input.GetMouseButton(1))
		{
			impassible = impassible ? false : true;
			if(impassible) changeColor(Color.yellow);
			else changeColor(Color.white);
		}
		//video2 final

		//GameManager.instance.moveCurrentPlayer(this);
	}

	public void changeColor(Color cor)
	{
		transform.renderer.material.color = cor;
	}
}
