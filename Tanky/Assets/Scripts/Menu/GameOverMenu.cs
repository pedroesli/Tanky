using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        MusicManager.instance.Play("Menu_select");
        SceneManager.LoadScene("GameScene");
    }
}
