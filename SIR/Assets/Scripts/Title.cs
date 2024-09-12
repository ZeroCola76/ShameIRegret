using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;
using URPGlitch.Runtime.AnalogGlitch;
using UnityEngine.VFX;

public class Title : MonoBehaviour
{
    public Image fadeout;
    public Image fakeImg;
    public Image titleImg;
    public GameObject velvet;
    private bool isFake = false;
    public Volume volume; // Volume 컴포넌트를 참조
    private AnalogGlitchVolume glitch;
    public VisualEffect visualEffect;
    private void Start()
    {
        SoundManager.Instance.PlayMusic(0);

        fadeout.DOFade(0f, 2).SetEase(Ease.Linear);

        ///나중에 살릴것!
        if (volume.profile.TryGet(out glitch))
        {
            // 초기 값 설정
            glitch.verticalJump.value = 0f;
        }

        //visualEffect.gameObject.SetActive(false);
        MoveTrue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            CumstomSceneManager.Instance.LoadScene(2);
            //if (isFake)
            //{
            //    CumstomSceneManager.Instance.LoadScene(3);
            //}
            //else
            //{
            //    MoveTrue();
            //}
        }
    }

    public void MoveTrue()
    {
        fakeImg.rectTransform.DOAnchorPosY(-1100, 1.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            glitch.verticalJump.value = 0f;
            glitch.horizontalShake.value = 0f;
            visualEffect.Stop();
        }); ;
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(1);
        titleImg.rectTransform.DOAnchorPosX(0, 3f).SetEase(Ease.InOutQuad);
        velvet.transform.DOMoveY(-9.3f, 3f).SetEase(Ease.InOutQuad);
        glitch.verticalJump.value = 0.3f;
        glitch.horizontalShake.value = 0.2f;
        visualEffect.gameObject.SetActive(true);
        visualEffect.Play();
        isFake = true;
    }
}
