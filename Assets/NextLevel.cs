using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public void LoadNextLevel()
    {
        // Check if the current scene is "Level1" before loading "Level2".
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            // Load "Level2" if the current scene is "Level1."
            SceneManager.LoadScene("Level 2");
        }

        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            // Load "Level2" if the current scene is "Level1."
            SceneManager.LoadScene("Level 3");
        }
    }
}
