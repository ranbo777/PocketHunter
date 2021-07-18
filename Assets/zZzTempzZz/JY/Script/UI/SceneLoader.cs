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

   
   
  



    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("PlayScene");
        op.allowSceneActivation = false;

        
        while(!op.isDone)
        {
            yield return null;
            if (progressBar.value < 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f, Time.deltaTime);

            }
            else if (op.progress >= 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime);

            }


            if (progressBar.value >= 0.95f)
            {
                loadtext.text = "�����̽� �ٸ� ������ ������ �����ϼ���";

            }

             if (Input.GetKeyDown(KeyCode.Space) && progressBar.value >= 1f && op.progress >= 0.9f)
                    {
                        op.allowSceneActivation = true;
                      
                    }
                    

           
                

           
        }
    }
    

}
