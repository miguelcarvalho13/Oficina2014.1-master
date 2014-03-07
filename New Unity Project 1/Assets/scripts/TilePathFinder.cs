using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilePathFinder : MonoBehaviour 
{
	public static List<Tile> FindPath(Tile originTile, Tile destinationTile, bool ignoresPlayers)
	{
		List<Tile> closed = new List<Tile>();
		List<TilePath> open = new List<TilePath>();
		
		TilePath originPath = new TilePath();
		originPath.addTile(originTile);
		
		open.Add(originPath);
		
		while(open.Count > 0)
		{
			TilePath current = open[0];
			open.Remove(open[0]);
			
			if(closed.Contains(current.lastTile))
			{
				continue;
			}
			if(current.lastTile == destinationTile)
			{
				current.listOfTiles.Remove(originTile);
				return current.listOfTiles;
			}

			closed.Add(current.lastTile);
			
			foreach(Tile t in current.lastTile.neighbors)
			{
				if(t.impassible) continue;

				TilePath newTilePath = new TilePath(current);
				newTilePath.addTile(t);
				open.Add(newTilePath);

				if(!ignoresPlayers)
				{
					foreach(Player player in GameManager.instance.players)
					{
						if(t.gridPosition.Equals(player.gridPosition)) open.RemoveAt(open.Count-1);
					}
				}
			}
		}
		return null;
	}
}