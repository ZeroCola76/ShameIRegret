using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public GameObject[] images;

    void Start()
    {
        // ���� Ȱ��ȭ�� ������ ȣ��Ǵ� �޼���
        ActivateRandomImage();
    }

    // �̹��� �� �ϳ��� �������� Ȱ��ȭ�ϴ� �޼���
    void ActivateRandomImage()
    {
        // ��� �̹����� ��Ȱ��ȭ
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }

        // 0���� �迭�� ����-1���� ���� �ε����� ����
        int randomIndex = Random.Range(0, images.Length);

        // �������� ���õ� �̹��� Ȱ��ȭ
        images[randomIndex].SetActive(true);
    }
}
