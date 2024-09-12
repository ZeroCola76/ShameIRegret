using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public GameObject[] images;

    void Start()
    {
        // 씬이 활성화될 때마다 호출되는 메서드
        ActivateRandomImage();
    }

    // 이미지 중 하나를 랜덤으로 활성화하는 메서드
    void ActivateRandomImage()
    {
        // 모든 이미지를 비활성화
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }

        // 0부터 배열의 길이-1까지 랜덤 인덱스를 생성
        int randomIndex = Random.Range(0, images.Length);

        // 랜덤으로 선택된 이미지 활성화
        images[randomIndex].SetActive(true);
    }
}
