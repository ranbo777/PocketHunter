using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{
    static string nextScene;
    public Text loadtext;

    [SerializeField]
    public Slider progressBar;

   
   
    public static void Load(Scene scene)
    {

        SceneManager.LoadScene("PlayScene");

    }



    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("PlayScene");
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;
            if(op.progress < 1f)
            {
                progressBar.value = op.progress;

            }    
            else
            {

                timer += Time.deltaTime;
                progressBar.value = Mathf.Lerp(0.9f, 1f, timer);
                

                if (progressBar.value >= 0.9f)
                {
                    loadtext.text = "스페이스 바를 눌러서 게임을 시작하세요";
                    if(Input.GetKeyDown(KeyCode.Space) && progressBar.value >= 1f && progressBar.value >= 0.9f)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                    

                }
                

            }
        }
    }
    
}
