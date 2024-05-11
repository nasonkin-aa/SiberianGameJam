using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    [Scene]
    private string gameScene;
    
    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
