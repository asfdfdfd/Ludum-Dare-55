using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishSceneController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Game/Scenes/MainMenuScene");
        }
    }
}
