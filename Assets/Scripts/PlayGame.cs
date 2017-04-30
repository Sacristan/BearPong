using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
      public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
        //Application.LoadLevel(level);
    }
}
