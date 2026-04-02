using UnityEngine;
using UnityEngine.SceneManagement;

public class start_title_button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start_Game()
    {
        SceneManager.LoadScene("Game");
    }
}
