using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostDummy : MonoBehaviour
{
    public Light directionalLight;
    // Start is called before the first frame update
    void Start()
    {
        //transform.DOMoveX(3, 10f);
        //transform.DOMoveX(3, 10f).SetEase(Ease.Linear);
        //transform.DOMoveX(3, 10f).SetEase(Ease.OutQuad);
        //transform.DOMoveX(3, 10f).SetEase(Ease.InQuad);
        //transform.DOMoveX(3, 3f).SetLoops(2, LoopType.Restart);
        //StartCoroutine(TestMove());
        ChangeLightColor();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(0.003f, 0f, 0f);

        //float t = Mathf.Clamp01(Time.time / 30);
        //transform.position = Vector3.Lerp(this.transform.position, new Vector3(3, 0, 0), t);

    }

    //()
    IEnumerator TestMove()
    {
        while (true) 
        {
            transform.position = transform.position + new Vector3(0.3f, 0f, 0f);
            Debug.Log("24년 코루틴 첫 실행, ");
            yield return new WaitForSeconds(1.0f);
        }
    }

    void ChangeLightColor()
    {
        //보간 하는 트윈 생성
        DOTween.To(() => directionalLight.color, x => directionalLight.color = x, Color.red, 10)
            .SetEase(Ease.Linear).OnComplete(() => Debug.Log("빛색깔이 바뀐다." + Color.red));
    }
}
