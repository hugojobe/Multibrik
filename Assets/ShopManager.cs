using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public static int[] itemPrices = {
        3, 5, 4, 6
    };

    public GameObject[] itemGhosts;

    private void Awake() {
        instance = this;
    }
}
