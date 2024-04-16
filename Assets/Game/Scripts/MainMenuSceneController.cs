using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectImage0;

    [SerializeField]
    private GameObject gameObjectImage1;

    [SerializeField]
    private GameObject gameObjectImage2;

    [SerializeField]
    private GameObject gameObjectImage3;   

    private int currentIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentIndex++;

            if (currentIndex == 1)
            {
                gameObjectImage0.SetActive(false);
                gameObjectImage1.SetActive(true);
            } 
            else if (currentIndex == 2)
            {
                gameObjectImage1.SetActive(false);
                gameObjectImage2.SetActive(true);
            }
            else if (currentIndex == 3)
            {
                gameObjectImage2.SetActive(false);
                gameObjectImage3.SetActive(true);
            }            
            else if (currentIndex == 4)
            {
                SceneManager.LoadScene("Game/Scenes/GameplayScene");
            }
        }
    }
}
