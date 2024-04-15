using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectLoseController : MonoBehaviour
{
    [SerializeField]
    private Jukebox jukebox;

    [SerializeField]
    private GameObject gameObjectSummon;

    [SerializeField]
    private GameObject gameObjectThis;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObjectThis.SetActive(false);
            gameObjectSummon.SetActive(true);

            jukebox.PlaySummonMusic();
        }
    }
}
