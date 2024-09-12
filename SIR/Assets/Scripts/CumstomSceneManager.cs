using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CumstomSceneManager : MonoBehaviour
{
    public static CumstomSceneManager Instance;
    private int targetSceneIndex = -1;
    private AsyncOperation asyncLoadIntermediate;
    private AsyncOperation asyncLoadTarget;
    private bool isIntermediateLoaded = false;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneIndex)
    {
        // 입력 받은 씬 인덱스가 유효한지 확인
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Invalid scene index: " + sceneIndex);
            return;
        }

        targetSceneIndex = sceneIndex;
        StartCoroutine(LoadIntermediateAndTargetScene());
    }

    private IEnumerator LoadIntermediateAndTargetScene()
    {
        SceneManager.LoadScene(1);


        yield return new WaitForSeconds(1f); 

        AsyncOperation asyncLoadTarget = SceneManager.LoadSceneAsync(targetSceneIndex);
        asyncLoadTarget.allowSceneActivation = false;

        Debug.Log("Loading target scene...");

        while (!asyncLoadTarget.isDone)
        {
            if (asyncLoadTarget.progress >= 0.9f)
            {
                asyncLoadTarget.allowSceneActivation = true;
                Debug.Log("Moved to target scene: " + targetSceneIndex);
            }

            yield return null;
        }

        targetSceneIndex = -1;
    }
}
