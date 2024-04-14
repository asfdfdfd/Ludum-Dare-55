using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    public void OnStartGameButtonPressed()
    {
        SceneManager.LoadScene("Game/Scenes/GameplayScene");
    }
}
