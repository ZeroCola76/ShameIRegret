using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class RoadCreator : MonoBehaviour
{
    public GameObject blockPrefab; // 길을 구성할 블록 프리팹
    public GameObject startPrefab; // 길을 구성할 블록 프리팹
    public GameObject attackPrefab; //공격하는 블록 프리팹
    public int pathLength = 20; // 생성할 길의 길이
    public float blockSpacing = 2f; // 블록 간의 거리
    public Vector3 startPosition = Vector3.zero; // 시작 위치

    private List<GameObject> generatedBlocks = new List<GameObject>();

    private const string poolKey = "RoadBlocks"; // 풀의 키 값


    private GameObject lastBlock; // 마지막 블록을 저장할 변수


    private void Start()
    {
        GeneratePath();

        PoolManager.Instance.CreatePool(poolKey, blockPrefab, 20);
    }

    private void GeneratePath()
    {
        Vector3 currentPosition = startPosition;

        int attackBlockCount = Random.Range(2,5); // 4에서 6개의 어택 블록을 생성
        int[] attackBlockIndices = new int[attackBlockCount];


        for (int i = 0; i < attackBlockCount; i++)
        {
            int index;
            do
            {
                index = Random.Range(1, pathLength - 1);
            }
            while (attackBlockIndices.Contains(index));
            attackBlockIndices[i] = index;
        }

        for (int i = 0; i < pathLength; i++)
        {
            GameObject newBlock;

            if (i == 0 || i == pathLength - 1)
            {
                // 첫 번째와 마지막 블록은 startPrefab 사용
                newBlock = Instantiate(startPrefab, currentPosition, Quaternion.identity);
            }
            else
            {
                // 나머지 블록은 오브젝트 풀에서 블록 가져오기
                newBlock = PoolManager.Instance.GetObject(poolKey, blockPrefab);
            }

            // 블록 위치 설정
            newBlock.transform.position = currentPosition;

            // 생성된 블록 리스트에 추가
            generatedBlocks.Add(newBlock);

            if (attackBlockIndices.Contains(i))
            {
                GenerateAttack(newBlock.transform.position);
            }

            // 다음 블록의 위치를 랜덤하게 결정
            currentPosition += new Vector3(Random.Range(-1, 2) * blockSpacing, 0, Random.Range(1, 2) * blockSpacing);

            // 마지막 블록을 업데이트
            lastBlock = newBlock;
        }

    }

    public void ClearPath()
    {
        foreach (GameObject block in generatedBlocks)
        {
            Destroy(block);
        }
        generatedBlocks.Clear();
    }

    private void GenerateAttack(Vector3 blockPosition)
    {
        Vector3 attackPosition = blockPosition + new Vector3(Random.Range(-10.0f, 10.0f) * blockSpacing, 0, Random.Range(-10.0f, 10.0f) * blockSpacing);

        // 공격 블록 생성
        Instantiate(attackPrefab, attackPosition, Quaternion.identity);
    }

}
