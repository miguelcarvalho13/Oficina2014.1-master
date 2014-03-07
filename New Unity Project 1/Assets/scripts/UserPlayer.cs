using UnityEngine;
using System.Collections;

public class UserPlayer : Player {

	public float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//video 2 inicio
		if(GameManager.instance.players[GameManager.instance.currentPlayerIndex] == this)
		{
			changeColor(Color.green);
		}
		else
		{
			changeColor(Color.white);
		}

		if(HP <= 0)
		{
			transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
			changeColor(Color.red);
		}
		//video 2 final
	}

	public override void TurnUpdate ()
	{
		if(positionQueue.Count > 0)
		{
			if(Vector3.Distance(positionQueue[0], transform.position) > 0.1f)
			{
				transform.position += (positionQueue[0] - transform.position).normalized * moveSpeed * Time.deltaTime;

				if(transform.position.x < positionQueue[0].x && transform.position.z == positionQueue[0].z) changeFace(FaceSelection.FaceRight);
				if(transform.position.x > positionQueue[0].x && transform.position.z == positionQueue[0].z) changeFace(FaceSelection.FaceLeft);
				if(transform.position.x == positionQueue[0].x && transform.position.z < positionQueue[0].z) changeFace(FaceSelection.FaceUp);
				if(transform.position.x == positionQueue[0].x && transform.position.z > positionQueue[0].z) changeFace(FaceSelection.FaceDown);

				if(Vector3.Distance(positionQueue[0], transform.position) <= 0.1f)
				{
					transform.position = positionQueue[0];
					positionQueue.RemoveAt(0);
					if(positionQueue.Count == 0)
					{
						actionPoints--;//video 2
					}
				}
			}
		}

		base.TurnUpdate ();
	}

	// video 2 inicio
	public override void TurnOnGUI()
	{

		float buttonHeight = 50;
		float buttonWidth = 150;

		Rect buttonRect = new Rect(0, Screen.height - buttonHeight*3, buttonWidth, buttonHeight);

		if(GUI.Button(buttonRect, "Mover") && this.positionQueue.Count == 0)
		{
			if(!moving)
			{
				GameManager.instance.removeTilesHightlights();
				moving = true;
				attacking = false;
				Debug.Log("movement: "+ movementPerActionPoint);
				GameManager.instance.highlightTilesAt(gridPosition, Color.blue, 10);
			}
			else
			{
				moving = false;
				attacking = false;
				GameManager.instance.removeTilesHightlights();
			}
		}

		/*buttonRect = new Rect(0, Screen.height - buttonHeight*2, buttonWidth, buttonHeight);

		if(GUI.Button(buttonRect, "Attack") && this.positionQueue.Count == 0)
		{
			if(!attacking)
			{
				GameManager.instance.removeTilesHightlights();
				moving = false;
				attacking = true;
				GameManager.instance.highlightTilesAt(gridPosition, Color.red, attackRange);
			}
			else
			{
				moving = false;
				attacking = false;
				GameManager.instance.removeTilesHightlights();
			}

		}*/

		buttonRect = new Rect(0, Screen.height - buttonHeight*2, buttonWidth, buttonHeight);

		if(GUI.Button(buttonRect, "Esperar") && this.positionQueue.Count == 0)
		{
			GameManager.instance.removeTilesHightlights();
			actionPoints = 2;

			moving = false;
			attacking = false;

			GameManager.instance.nextTurn();
		}
		base.TurnOnGUI();
	}
	//video 2 final
}
