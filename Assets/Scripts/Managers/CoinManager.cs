using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int maxCoinInPile;

    public List<GameObject> pileBaseSpawn;

    public List<CoinPile> coinPiles = new List<CoinPile>();

    public GameObject coinPrefab;

    public PlayerBuildingSystem playerBuildingSystem;

    private void Start() {
        for (int i = 0; i < pileBaseSpawn.Count; i++) {
            CoinPile pile = new CoinPile(this);
            coinPiles.Add(pile);
        }

        for(int i = 0; i < GameManager.instance.startBalance; i++) {
            AddCoinToPile();
        }
    }

    public void AddCoinToPile() {
        bool addedToPile = false;

        bool isNewPile;

        int pileIndex = 0;
        foreach(CoinPile pile in coinPiles) {
            if(pile.coinsGoInPile.Count < maxCoinInPile) {
                isNewPile = pile.coinsGoInPile.Count == 0;

                pile.SpawnCoinOnPile(coinPrefab, isNewPile, pileIndex);
                addedToPile = true;
                break;
            }

            pileIndex++;
        }

        if(!addedToPile) {
            Debug.Log("All coin piles are full. Cannot add coin.");
        }

        
    }

    public void RemoveCoinFromPile() {
        if(playerBuildingSystem.balance > (maxCoinInPile * coinPiles.Count)) {
            return;
        }

        CoinPile lastNonEmptyPile = null;

        for(int i = coinPiles.Count - 1; i >= 0; i--) {
            if(coinPiles[i].coinsGoInPile.Count > 0) {
                lastNonEmptyPile = coinPiles[i];
                break;
            }
        }

        if(lastNonEmptyPile != null) {
            GameObject lastCoin = lastNonEmptyPile.coinsGoInPile[lastNonEmptyPile.coinsGoInPile.Count - 1];

            lastNonEmptyPile.coinsGoInPile.RemoveAt(lastNonEmptyPile.coinsGoInPile.Count - 1);
            Destroy(lastCoin);
        }
    }
}

public class CoinPile {
    public CoinManager coinManager;

    public List<GameObject> coinsGoInPile;

    private float coinThickness = 0.256f;

    public CoinPile(CoinManager coinManager) {
        coinsGoInPile = new List<GameObject>();
        coinThickness = 0.256f;
        this.coinManager = coinManager;
    }

    public void SpawnCoinOnPile(GameObject coinPrefab, bool newPile, int pileIndex) {
        Vector3 spawnPosition;
        if(newPile) {
            spawnPosition = coinManager.pileBaseSpawn[pileIndex].transform.position;
        } else {
            spawnPosition = coinsGoInPile[coinsGoInPile.Count - 1].transform.position + new Vector3(0, coinThickness, 0);
        }

        GameObject newCoin = Object.Instantiate(coinManager.coinPrefab, spawnPosition, Quaternion.identity);
        coinsGoInPile.Add(newCoin);
    }
}
