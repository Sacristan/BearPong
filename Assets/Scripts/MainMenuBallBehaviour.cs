using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBallBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish") SceneManager.LoadScene(SceneNames.Game);
    }
}
