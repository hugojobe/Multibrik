using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
	public static GridSystem instance;

    [SerializeField]
	private bool LeftSide;

	public List<GameObject> allTiles;

	public int gridWidth;
	public int gridHeight;
	public GameObject[,] tileGrid;

    private void Awake() {
        instance = this;

		tileGrid = new GameObject[gridWidth, gridHeight];

		FindTiles();
	}

	private void FindTiles() {
		for(int y = 0; y < gridWidth; y++) {
			for(int x = 0; x < gridHeight; x++) {
				string tileName = $"Tile {x}_{y}";
				GameObject tile = allTiles.Where(t => t.name == tileName).FirstOrDefault();

				if (tile != null){
					tileGrid[y, x] = tile;
					tile.GetComponent<Tile>().tilePosition = new Vector2(x, y);
				} else
					Debug.LogError($"Tile {x}_{y} not found in the array of side {(LeftSide? "left" : "right")}.");
			}
		}
	}

	public GameObject GetTile(int y, int x) {
		if(x >= 0 && x <  gridWidth && y >= 0 && y < gridHeight) {
			return tileGrid[x, y];
		}
		return null;
	}
}
