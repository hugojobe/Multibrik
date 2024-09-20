using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Ball Launch")]
    public BallLauncher ballLauncher;
    public float ballLaunchDelay;
    public float ballLaunchDelayModifier;

    [Header("Balance")]
    public int startBalance;
    public TextMeshProUGUI[] balanceTexts;

    public List<PlayerBuildingSystem> playerBuildingSystems;

    [Header("Starting Config")]
    public List<Tile> leftStartingTiles;
    public List<Tile> rightStartingTiles;
    public GameObject shipFigure;
    public GameObject basicBarrel;

    [Header("GameOver")]
    public GameObject gameOverCanvas;
    public TextMeshProUGUI gameOverText;

    public bool hasGameStarted;

    [Header("Explosive Barrels")]
    public GameObject explosiveBarrelPrefab;
    public Vector2 bottomLeftCorner;
    public Vector2 topRightCorner;


    private void Awake() {
        instance = this;
    }

    private void Start() {
        //StartGame();
    }

    public void StartGame() {
        Tile leftShipTile = leftStartingTiles[Random.Range(0, leftStartingTiles.Count)];
        Tile rightShipTile = rightStartingTiles[Random.Range(0, rightStartingTiles.Count)];

        SpawnStartingConfig(leftShipTile, true);
        SpawnStartingConfig(rightShipTile, false);

        float launchDelay = Random.Range(ballLaunchDelay - ballLaunchDelayModifier, ballLaunchDelay + ballLaunchDelayModifier);
        ballLauncher.LaunchBall(launchDelay);
        
        InvokeRepeating("GivePeriodicPlayerMoney", 5, 5);

        hasGameStarted = true;
    }

    public void GivePeriodicPlayerMoney() {
        foreach(PlayerBuildingSystem playerBuildingSystem in playerBuildingSystems) {
            playerBuildingSystem.balance += 1;
        }
    }

    public void SpawnStartingConfig(Tile shipTile, bool leftSide) {
        GameObject spawnedShip = Instantiate(shipFigure, shipTile.transform.position, Quaternion.identity);
        shipTile.isEmpty = false;
        spawnedShip.GetComponent<ShipBlock>().playerIndex = leftSide ? 0 : 1;

        Tile upTile = PlayersManager.instance.gridSystems[leftSide ? 0 : 1].GetTile((int)shipTile.tilePosition.x + 1, (int)shipTile.tilePosition.y).GetComponent<Tile>();
        Tile downTile = PlayersManager.instance.gridSystems[leftSide ? 0 : 1].GetTile((int)shipTile.tilePosition.x - 1, (int)shipTile.tilePosition.y).GetComponent<Tile>();
        Tile sideTile = PlayersManager.instance.gridSystems[leftSide ? 0 : 1].GetTile((int)shipTile.tilePosition.x, (int)shipTile.tilePosition.y + (leftSide? 1 : -1)).GetComponent<Tile>();

        GameObject upTileGO = Instantiate(basicBarrel, upTile.transform.position, Quaternion.identity);
        GameObject downTileGO = Instantiate(basicBarrel, downTile.transform.position, Quaternion.identity);
        GameObject sideTileGO = Instantiate(basicBarrel, sideTile.transform.position, Quaternion.identity);

        upTileGO.GetComponent<Block>().correspondingTile = upTile;
        downTileGO.GetComponent<Block>().correspondingTile = downTile;
        sideTileGO.GetComponent<Block>().correspondingTile = sideTile;

        upTile.isEmpty = false;
        downTile.isEmpty = false;
        sideTile.isEmpty = false;
    }

    public void GameOver(int losingPlayerIndex) {
        Time.timeScale = 0.25f;

        gameOverCanvas.SetActive(true);
        gameOverText.text = $"P{losingPlayerIndex + 1} A PERDU";
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Vector3 bottomLeft = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRight = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeft = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRight = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
