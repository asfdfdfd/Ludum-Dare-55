using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectBook;

    [SerializeField]
    private GameObject gameObjectPanel1;

    [SerializeField]
    private GameObject gameObjectPanel2;    

    [SerializeField]
    private GameObject gameObjectPanel3;    

    private int panelIndex = 0;

    private readonly List<GameObject> panels = new List<GameObject>();

    private void Awake() 
    {
        panels.Add(gameObjectPanel1);
        panels.Add(gameObjectPanel2);
        panels.Add(gameObjectPanel3);
    }

    public void OnBookCloseButtonClick()
    {
        gameObjectBook.SetActive(false);
    }

    public void ShowPanel1() 
    {
        gameObjectPanel1.SetActive(true);
        gameObjectPanel2.SetActive(false);
        gameObjectPanel3.SetActive(false);
    }

    public void ShowPanel2() 
    {
        gameObjectPanel1.SetActive(false);
        gameObjectPanel2.SetActive(true);
        gameObjectPanel3.SetActive(false);        
    }    

    public void ShowPanel3() 
    {
        gameObjectPanel1.SetActive(false);
        gameObjectPanel2.SetActive(false);
        gameObjectPanel3.SetActive(true);        
    }
}
