using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    static string nextScene;
    public void OnClickNewGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void OnClickLoad(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("PlayScene");

    }
    public void OnClickOption()
    { }



    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif


    }
}
