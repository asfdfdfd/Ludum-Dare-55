using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectShop;

    public void OnCloseButtonClick()
    {
        gameObjectShop.SetActive(false);
    }
}
