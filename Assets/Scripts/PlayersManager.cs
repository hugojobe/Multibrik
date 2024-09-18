using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Color = UnityEngine.Color;

public class PlayersManager : MonoBehaviour
{
	public static PlayersManager instance;

	private List<GameObject> playerPrefabs = new List<GameObject>();

	public TextMeshProUGUI[] connectionStatus;

	public GridSystem[] gridSystems;

    private void Awake() {
        instance = this;
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
		playerPrefabs.Add(playerInput.gameObject);
		playerInput.gameObject.GetComponent<PlayerBuildingSystem>().gridSystem = gridSystems[playerInput.playerIndex];

		ColorUtility.TryParseHtmlString("#70ED40", out Color color);
		connectionStatus[playerInput.playerIndex].color = color;

		if(playerPrefabs.Count == 2) {
			OnGameReady();
		}
	}

	public void OnPlayerLeft(PlayerInput playerInput){
		ColorUtility.TryParseHtmlString("#595959", out Color color);
		connectionStatus[playerInput.playerIndex].color = color;
		Debug.Break();
		Debug.LogError($"Player {playerInput.playerIndex} disconnected");

		connectionStatus[2].color = color;
	}

	private void OnGameReady() {
		ColorUtility.TryParseHtmlString("#70ED40", out Color color);
		connectionStatus[2].color = color;
	}
}