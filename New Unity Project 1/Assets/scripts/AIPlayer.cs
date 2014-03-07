//123
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : Player {

	public enum AIStates
	{
		StateWait, StateChase, StateAttack, StateRunAway
	}
	
	public AIStates currentStateAI;
	public float moveSpeed = 5.0f;
	public int visionRange = 10;
	int timesCount = 0;

	Player currentTarget = null;

	// Use this for initialization
	void Start () 
	{
		currentStateAI = AIStates.StateWait;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.instance.players[GameManager.instance.currentPlayerIndex].Equals(this))
		{
			changeColor(Color.red);
		}
		else
		{
			changeColor(Color.white);
		}
	}

	public override void TurnUpdate ()
	{
		switch(this.currentStateAI)
		{
			case AIStates.StateWait:
									AIStateWait ();
									break;
			case AIStates.StateChase:
									AIStateChase();
									break;
			case AIStates.StateAttack:
									AIStateAttack();
									break;
			case AIStates.StateRunAway:
									AIStateRunAway();
									break;
		}
	}
	
	public override void TurnOnGUI()
	{
		base.TurnOnGUI();
	}

	public void changeState(AIStates currentStateAI)
	{
		this.currentStateAI = currentStateAI;
	}

	//AI state functions
	#region Wait State
	public void AIStateWait()
	{
		List<Tile> inRangeTiles = TileHighlight.FindHighlights(GameManager.instance.map[(int)this.gridPosition.x][(int)this.gridPosition.y], visionRange, true);
		List<Vector2> playersPositions = new List<Vector2>();

		foreach(Tile tile in inRangeTiles)
		{
			foreach(Player player in GameManager.instance.players)
			{
				if(player.GetType() == typeof(UserPlayer) && tile.gridPosition.Equals(player.gridPosition))
				{
					playersPositions.Add(player.gridPosition);
				}
			}
			if(playersPositions.Count == GameManager.instance.players.Count) break;//stops the 'for' if all the players are already checked
		}

		if(playersPositions.Count == 1)//best performance
		{
			foreach(Player player in GameManager.instance.players)
			{
				if(player.gridPosition.Equals(playersPositions[0]))
				{
					currentTarget = player;
					changeState(AIStates.StateChase);//change to the Chase state
				}
			}
		}
		else if(playersPositions.Count > 1)//slow performance
		{
			float[] distances = new float[playersPositions.Count];

			for(int i = 0; i < distances.Length; i++)
			{
				distances[i] = (Vector2.Distance(this.gridPosition, playersPositions[i]));
			}

			for (int pivot = 0; pivot < distances.Length; pivot++)//sort the 'distances' array
			{
				for(int i = pivot + 1; i< distances.Length; i++)
				{
					if(distances[pivot] > distances[i])
					{
						float tempFloat = distances[pivot];
						distances[pivot] = distances[i];
						distances[i] = tempFloat;

						Vector2 tempVector2 = playersPositions[i];
						playersPositions[pivot] = playersPositions[i];
						playersPositions[i] = tempVector2;
					}              
				}
			}

			foreach(Player player in GameManager.instance.players)
			{
				if(player.gridPosition.Equals(playersPositions[0]))
				{
					currentTarget = player;
					changeState(AIStates.StateChase);//change to the Chase state
				}
			}
		}
		else
		{
			this.actionPoints = 0;
			StartCoroutine(DelayOfThink(1.5f));
		}
		Debug.Log ("players in range: "+playersPositions.Count);
	}
	#endregion

	#region Chase State
	public void AIStateChase()
	{
		if(currentTarget.gridPosition.x == this.gridPosition.x + 1 && currentTarget.gridPosition.y == this.gridPosition.y ||
		   currentTarget.gridPosition.x == this.gridPosition.x - 1 && currentTarget.gridPosition.y == this.gridPosition.y ||
		   currentTarget.gridPosition.x == this.gridPosition.x && currentTarget.gridPosition.y == this.gridPosition.y - 1 ||
		   currentTarget.gridPosition.x == this.gridPosition.x && currentTarget.gridPosition.y == this.gridPosition.y + 1)
		{
			actionPoints = 0;
			StartCoroutine(DelayOfThink(1.5f));
		}
		else
		{
			if(timesCount == 0)
			{
				GameManager.instance.highlightTilesAt(this.gridPosition, Color.blue, 5);
				int randomTile = Random.Range(0, GameManager.instance.map[(int)currentTarget.gridPosition.x][(int)currentTarget.gridPosition.y].neighbors.Count);
				Tile destTile = GameManager.instance.map[(int)currentTarget.gridPosition.x][(int)currentTarget.gridPosition.y].neighbors[randomTile];

				foreach(Tile t in TilePathFinder.FindPath(GameManager.instance.map[(int)this.gridPosition.x][(int)this.gridPosition.y], destTile, false))
				{
					if(positionQueue.Count < 5)
					{
						this.positionQueue.Add(GameManager.instance.map[(int)t.gridPosition.x][(int)t.gridPosition.y].transform.position + 1.5f * Vector3.up);
					}
					else break;
				}
				Debug.Log ("queue: "+this.positionQueue.Count);
				timesCount++;
			}
			
			if(timesCount > 0 && positionQueue.Count > 0)
			{
				movementAIPlayer();
			}

			if(this.actionPoints == 0)
			{
				timesCount = 0;
				GameManager.instance.removeTilesHightlights();
				base.TurnUpdate ();
			}
		}
	}
	#endregion

	#region Attack State
	public void AIStateAttack()
	{
	}
	#endregion

	#region RunAway State
	public void AIStateRunAway()
	{
	}
	#endregion

	private IEnumerator DelayOfThink(float time)
	{
		yield return new WaitForSeconds(time);
		GameManager.instance.removeTilesHightlights();
		base.TurnUpdate ();
	}
	
	private IEnumerator DelayOfMovement(float time)
	{
		yield return new WaitForSeconds(time);
	}

	private void movementAIPlayer()
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
				this.gridPosition.x = positionQueue[0].x + 10;
				this.gridPosition.y = -positionQueue[0].z + 10;
				positionQueue.RemoveAt(0);
				if(positionQueue.Count == 0)
				{
					actionPoints--;
					timesCount = 0;
					GameManager.instance.removeTilesHightlights();
				}
			}
		}
	}

}
