using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

enum PlayerBuildingState {
	None,
	Ghost
}

public class PlayerBuildingSystem : MonoBehaviour
{
	public GridSystem gridSystem;

	private PlayerBuildingState buildingState;
	private GameObject ghostBuilding;

	public int balance;

	public InputActionAsset inputAction;
	private InputAction ghostJoystickAction;
	private Vector2 ghostMoveDirection;
    private bool ghostHasMoved = false;

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
        ghostJoystickAction = inputAction.FindActionMap("Player").FindAction("MoveGhost");
        ghostJoystickAction.Enable();
    }

    void OnDisable()
    {
        ghostJoystickAction.Disable();
    }

	void Update()
    {
        ghostMoveDirection = ghostJoystickAction.ReadValue<Vector2>();

        if (ghostMoveDirection.magnitude > 0.1f && !ghostHasMoved) {
            ghostHasMoved = true;
            MoveInDirection(ghostMoveDirection);
        }
        else if (ghostMoveDirection.magnitude <= 0.1f)
        {
            ghostHasMoved = false;
        }
    }

	private void BuyShop(int itemIndex) {
		if(buildingState == PlayerBuildingState.Ghost)
			BuildFinal(itemIndex);


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

	private void BuildGhost(int index) {
		ghostBuilding = Instantiate(ShopManager.instance.itemGhosts[index]);
		//ghostBuilding.transform.position = 
	}

	private void BuildFinal(int index) {

    }

    void MoveInDirection(Vector2 direction)
    {
        Debug.Log($"Moving in direction: {direction}");
    }
}
