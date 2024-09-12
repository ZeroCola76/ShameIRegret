using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bullet; // �Ѿ�
    private const string attackPoolKey = "attackBlock"; // Ǯ�� Ű ��

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Instance.CreatePool(attackPoolKey, bullet, 10);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 10f)); // 3�� ���
            GameObject a = PoolManager.Instance.GetObject(attackPoolKey, bullet, this.gameObject.transform);
            a.transform.position = this.gameObject.transform.position;
        }
    }
}
