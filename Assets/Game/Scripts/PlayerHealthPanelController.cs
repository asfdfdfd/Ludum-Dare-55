using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPanelController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HealthPanelController healthPanelController;

    private void Update()
    {
        healthPanelController.SetHealth(playerController.Health);
    }
}
