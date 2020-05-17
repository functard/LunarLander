using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Onclick_PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClick_QuitGame()
    {
        Application.Quit();
    }
}
