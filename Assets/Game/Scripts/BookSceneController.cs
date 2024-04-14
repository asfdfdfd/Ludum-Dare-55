using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectBook;

    public void OnBookCloseButtonClick()
    {
        gameObjectBook.SetActive(false);
    }
}
