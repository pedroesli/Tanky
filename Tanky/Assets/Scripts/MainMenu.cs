using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        MusicManager.instance.Play("Menu_select");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        MusicManager.instance.Play("Menu_select");
        Application.Quit();
    }
    public void SetActive(GameObject obj)
    {
        MusicManager.instance.Play("Menu_select");
        obj.SetActive(true);
    }
}
