using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

enum PlayerBuildingState {
	None,
	Ghost
}

public class PlayerBuildingSystem : MonoBehaviour
{
	public int playerIndex;
	public GridSystem gridSystem;

	private PlayerBuildingState buildingState;
	private GameObject ghostBuilding;
	private Vector2Int ghostBuildingPosition;

    public int balance {
        get { return _balance; }
        set {
            _balance = value;
            OnBalanceSet(value);
        }
    }
    private int _balance;
	public TextMeshProUGUI balanceText;

    public InputActionAsset inputAction;
	private InputAction ghostJoystickAction;
	private Vector2 ghostMoveDirection;
    private bool ghostHasMoved = false;
	private int ghostBuildIndex = -1;

	public bool canGhostBeBuilt;

	public void OnShop1() {
		BuyShop(0);
	}

	public void OnShop2() {
		BuyShop(1);
	}

	public void OnShop3() {
		BuyShop(2);
	}

	public void OnShop4() {
		BuyShop(3);
	}

	void OnEnable()
    {
        ghostJoystickAction = GetComponent<PlayerInput>().actions.FindActionMap("Player").FindAction("MoveGhost");
        ghostJoystickAction.Enable();
    }

    void OnDisable()
    {
        ghostJoystickAction.Disable();
    }

	void Update()
    {
		HandleGhostBuildingMovement();
    }

	private void HandleGhostBuildingMovement() {
		if(buildingState == PlayerBuildingState.Ghost && ghostBuilding != null) {
            ghostMoveDirection = ghostJoystickAction.ReadValue<Vector2>();

			if (ghostMoveDirection.magnitude > 0.01f && !ghostHasMoved) {
				ghostHasMoved = true;
				MoveInDirection(ghostMoveDirection);
			}
			else if (ghostMoveDirection.magnitude <= 0.01f)
			{
				ghostHasMoved = false;
			}
		}
	}

	private void BuyShop(int itemIndex) {
		if(buildingState == PlayerBuildingState.Ghost && itemIndex == ghostBuildIndex){
			if(gridSystem.GetTile(ghostBuildingPosition.x, ghostBuildingPosition.y).GetComponent<Tile>().isEmpty == false || canGhostBeBuilt == false)
				return;

			BuildFinal(itemIndex);
			return;
		}

        int itemPrice = ShopManager.itemPrices[itemIndex];
		if(balance < itemPrice){
			return;
		}

		SpendMoney(itemPrice);
		BuildGhost(itemIndex);
	}

	private void GiveMoney(int amount) {
		balance += amount;
	}

	private void SpendMoney(int amount) {
		balance -= amount;
	}

	private void OnBalanceSet(int newBalance) {
		balanceText.text = newBalance.ToString();
    }

	private void BuildGhost(int index) {
		buildingState = PlayerBuildingState.Ghost;
		ghostBuildIndex = index;

        ghostBuilding = Instantiate(ShopManager.instance.itemGhosts[index]);
		ghostBuilding.transform.position = gridSystem.GetTile(3,2).transform.position;
		ghostBuildingPosition = new Vector2Int(3, 2);

		OnBuildGhostFeedbacks(index, ghostBuilding);
    }

	private void BuildFinal(int index) {
		Destroy(ghostBuilding);
        ghostBuildIndex = -1;
        buildingState = PlayerBuildingState.None;

        GameObject finalBuilding = Instantiate(ShopManager.instance.itemPrefabs[index]);
		GameObject finalTile = gridSystem.GetTile(ghostBuildingPosition.x, ghostBuildingPosition.y);
		finalBuilding.transform.position = finalTile.transform.position;
		finalBuilding.GetComponent<Block>().correspondingTile = finalTile.GetComponent<Tile>();
        finalTile.GetComponent<Tile>().isEmpty = false;

        OnBuildFinalFeedbacks(index, finalBuilding);
    }

    void MoveInDirection(Vector2 direction)
    {
        if(buildingState != PlayerBuildingState.Ghost)
            return;

        direction = Mathf.Abs(direction.x) > Mathf.Abs(direction.y)
            ? new Vector2(Mathf.Sign(direction.x), 0)
            : new Vector2(0, Mathf.Sign(direction.y));

        ghostBuildingPosition += new Vector2Int((int)direction.y, (int)direction.x);

		if (ghostBuildingPosition.y < 0) {
            ghostBuildingPosition.y = GridSystem.instance.gridWidth - 1;
        } else if (ghostBuildingPosition.y >= GridSystem.instance.gridWidth) {
            ghostBuildingPosition.y = 0;
        }

        if (ghostBuildingPosition.x < 0) {
            ghostBuildingPosition.x = GridSystem.instance.gridHeight - 1;
        } else if (ghostBuildingPosition.x >= GridSystem.instance.gridHeight) {
            ghostBuildingPosition.x = 0;
        }


        GameObject newTile = gridSystem.GetTile(ghostBuildingPosition.x, ghostBuildingPosition.y);
        ghostBuilding.transform.position = newTile.transform.position;
		OnGhostMovedOnTileFeedback(ghostBuilding, newTile.GetComponent<Tile>());
    }

	// Called when a ghost building is spawned. 
    // buildingIndex is the index of the building in the ShopManager.itemGhosts array.
    // buildingPrefab is the GameObject reference of the ghost that was spawned.
    private void OnBuildGhostFeedbacks(int buildingIndex, GameObject ghostPrefab) {

	}

    // Called when a building is placed. 
    // buildingIndex is the index of the building in the ShopManager.itemPrefabs array.
    // buildingPrefab is the GameObject reference of the building that was placed.
    private void OnBuildFinalFeedbacks(int buildingIndex, GameObject buildingPrefab) {

	}

    // Called when a ghost building is moved on a tile. 
    // ghostPrefab is the GameObject reference of the ghost that was moved.
    // newTile is the Tile component of the tile the ghost was moved to.
    private void OnGhostMovedOnTileFeedback(GameObject ghostPrefab, Tile newTile) {
		if(newTile.isEmpty){
			SetGhostNormalFeedback();
		} else {
			SetGhostBlockedFeedback();
		}
    }

    // Called when a ghost building cannot be placed.
    private void SetGhostBlockedFeedback() {

	}

    // Called when a ghost building can be placed.
    private void SetGhostNormalFeedback() {

	}
}