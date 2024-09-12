using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bullet; // 총알
    private const string attackPoolKey = "attackBlock"; // 풀의 키 값

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
            yield return new WaitForSeconds(Random.Range(3f, 10f)); // 3초 대기
            GameObject a = PoolManager.Instance.GetObject(attackPoolKey, bullet, this.gameObject.transform);
            a.transform.position = this.gameObject.transform.position;
        }
    }
}
