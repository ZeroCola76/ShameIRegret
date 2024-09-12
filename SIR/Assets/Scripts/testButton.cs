using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testButton : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayMusic(0);
    }

    public void testMain()
    {
        //SceneManager.LoadScene(2);
        CumstomSceneManager.Instance.LoadScene(3);
    }
}
