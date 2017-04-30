using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayGameLiene : MonoBehaviour {

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
        //Application.LoadLevel(level);
    }
}
