using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class RoadCreator : MonoBehaviour
{
    public GameObject blockPrefab; // ���� ������ ��� ������
    public GameObject startPrefab; // ���� ������ ��� ������
    public GameObject attackPrefab; //�����ϴ� ��� ������
    public int pathLength = 20; // ������ ���� ����
    public float blockSpacing = 2f; // ��� ���� �Ÿ�
    public Vector3 startPosition = Vector3.zero; // ���� ��ġ

    private List<GameObject> generatedBlocks = new List<GameObject>();

    private const string poolKey = "RoadBlocks"; // Ǯ�� Ű ��


    private GameObject lastBlock; // ������ ����� ������ ����


    private void Start()
    {
        GeneratePath();

        PoolManager.Instance.CreatePool(poolKey, blockPrefab, 20);
    }

    private void GeneratePath()
    {
        Vector3 currentPosition = startPosition;

        int attackBlockCount = Random.Range(2,5); // 4���� 6���� ���� ����� ����
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
                // ù ��°�� ������ ����� startPrefab ���
                newBlock = Instantiate(startPrefab, currentPosition, Quaternion.identity);
            }
            else
            {
                // ������ ����� ������Ʈ Ǯ���� ��� ��������
                newBlock = PoolManager.Instance.GetObject(poolKey, blockPrefab);
            }

            // ��� ��ġ ����
            newBlock.transform.position = currentPosition;

            // ������ ��� ����Ʈ�� �߰�
            generatedBlocks.Add(newBlock);

            if (attackBlockIndices.Contains(i))
            {
                GenerateAttack(newBlock.transform.position);
            }

            // ���� ����� ��ġ�� �����ϰ� ����
            currentPosition += new Vector3(Random.Range(-1, 2) * blockSpacing, 0, Random.Range(1, 2) * blockSpacing);

            // ������ ����� ������Ʈ
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

        // ���� ��� ����
        Instantiate(attackPrefab, attackPosition, Quaternion.identity);
    }

}
