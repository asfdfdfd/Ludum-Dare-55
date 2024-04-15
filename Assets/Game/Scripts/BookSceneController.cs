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

    public void ShowNext() 
    {
        panelIndex++;

        if (panelIndex == panels.Count)
        {
            panelIndex = 0;
        }

        foreach(GameObject gob in panels)
        {
            gob.SetActive(false);
        }

        panels[panelIndex].SetActive(true);
    }

    public void ShowPrev() 
    {
        panelIndex--;

        if (panelIndex < 0)
        {
            panelIndex = panels.Count - 1;
        }

        foreach(GameObject gob in panels)
        {
            gob.SetActive(false);
        }

        panels[panelIndex].SetActive(true);     
    }    
}
