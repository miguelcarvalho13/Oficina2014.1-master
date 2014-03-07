using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum FaceSelection
	{
		FaceUp, FaceDown, FaceLeft, FaceRight
	}

	public FaceSelection faceSelection;

	public Vector3 moveDestination;

	public int movementPerActionPoint = 2;
	public int attackRange = 1;
	// video 2 inicio
	public string playerName = "";

	public Vector2 gridPosition = Vector2.zero;

	public bool moving = false;
	public bool attacking = false;

	public int HP = 25;
	public float attackChance = 0.75f;
	public float defenseReduction = 0.15f;
	public int damageBase = 5;
	public float damageRollSides = 6;

	public int actionPoints = 2;
	//video 2 final
	public List<Vector3> positionQueue = new List<Vector3>();

	void Awake(){
		moveDestination = transform.position;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual void TurnUpdate()
	{
		//video 2 inicio
		if(actionPoints <= 0)
		{
			actionPoints = 2;
			
			moving = false;
			attacking = false;
			
			GameManager.instance.nextTurn();
		}
		//video 2 final
	}
	public virtual void TurnOnGUI(){

	}

	public void changeColor(Color cor)
	{
		transform.renderer.material.color = cor;
	}

	public void changeFace(FaceSelection faceSelection)
	{
		this.faceSelection = faceSelection;

		switch(this.faceSelection)
		{
			case FaceSelection.FaceUp:	
										transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
										break;
			case FaceSelection.FaceDown:
										transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
										break;
			case FaceSelection.FaceLeft:
										transform.rotation = Quaternion.Euler(new Vector3(0,270,0));
										break;
			case FaceSelection.FaceRight:
										transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
										break;
		}
	}
}
