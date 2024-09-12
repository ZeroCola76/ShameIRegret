using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static SODatas;
using UnityEngine.UI;
using Unity.VisualScripting;

/// <summary>
/// csv ������ �Ľ��Ѵ�. ,�� �������� 
/// ���߿� �������̺� �� �����̴�. ������ ����� ������...
/// 
/// �迹���� �ۼ�
/// </summary>
[DefaultExecutionOrder(-100)]
public class ReadNode : MonoBehaviour
{
    //public TextAsset[] csvFile; //���⿡ csv ������ �Ҵ��ҰŴ�.
    public List<TextAsset> csvList = new List<TextAsset>();
    void Start()
    {
        TextAsset[] csvFiles = Resources.LoadAll<TextAsset>("CSV");

        foreach (TextAsset song in csvFiles)
        {
            csvList.Add(song);
        }

        if (csvFiles != null && csvFiles.Length > 0)
        {
            foreach (TextAsset csvFile in csvFiles)
            {
                List<SODatas> noteDataList = ParseCsvFile(csvFile.text);
                SaveNoteDataToScriptableObject(noteDataList,csvFile.name);
            }
        }
        else
        {
            Debug.LogError("���� ���µ�");
        }
    }

    List<SODatas> ParseCsvFile(string csvText)
    {
        List<SODatas.NoteData> noteDataList = new List<SODatas.NoteData>();
        StringReader reader = new StringReader(csvText);
        string title = "";
        string artist = "";
        string level = "";
        bool isTitleParsed = false;
        bool isArtistParsed = false;
        bool isLevelParsed = false;

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine().Trim();  // ���� ���� ����

            // ���ʿ��� ��ǥ ����
            line = line.TrimEnd(',');


            // ����� ��Ƽ��Ʈ�� �����ϴ� ���� ó������ �� ���� ó��
            if (!isTitleParsed && line.StartsWith("Title-"))
            {
                title = line.Substring(6).Trim();
                isTitleParsed = true;
                continue;
            }
            if (!isArtistParsed && line.StartsWith("Artist-"))
            {
                artist = line.Substring(7).Trim();
                isArtistParsed = true;
                continue;
            }
            if (!isLevelParsed && line.StartsWith("Level-"))
            {
                level = line.Substring(6).Trim();
                isLevelParsed = true;
                continue;
            }

            // ��Ʈ �����͸� �����ϴ� ���� ó��
            string[] values = line.Split(',');
            if (values.Length < 5) continue; // ��ȿ���� ���� �� ����

            int x = int.Parse(values[0]);
            int y = int.Parse(values[1]);
            int time = int.Parse(values[2]);
            int type = int.Parse(values[3]);
            int dummy = int.Parse(values[4]);
            int longNoteTime = -1;

            // �ճ�Ʈ�� ��� �ճ�Ʈ �ð� �Ľ�
            if (type == 128 && values.Length > 5)
            {
                string[] longNoteData = values[5].Split(':');
                if (longNoteData.Length > 0)
                {
                    longNoteTime = int.Parse(longNoteData[0]);
                }
                else
                {
                    Debug.LogWarning("�ճ�Ʈ �ð��� ���� �����ϴ�!");
                }
            }

            // NoteData ��ü ���� �� ����
            SODatas.NoteData noteData = new SODatas.NoteData
            {
                x = x,
                y = y,
                time = time,
                type = type,
                longNoteTime = longNoteTime
            };

            // ������ NoteData�� ����Ʈ�� �߰�
            noteDataList.Add(noteData);
        }

        // ScriptableObject ���� �� ����
        SODatas songData = ScriptableObject.CreateInstance<SODatas>();
        songData.title = title;
        songData.artist = artist;
        songData.level = level;
        songData.noteDataArray = noteDataList.ToArray();

        // ������ ScriptableObject�� ����Ʈ�� ��ȯ
        List<SODatas> songDataList = new List<SODatas> { songData };
        return songDataList;
    }

    void SaveNoteDataToScriptableObject(List<SODatas> noteDataList, string fileName)
    {
#if UNITY_EDITOR
        foreach (SODatas songData in noteDataList)
        {
            // ScriptableObject ���� (Asset���� ����)
            string filePath = $"Assets/Resources/Song/{fileName}.asset"; // ���� �̸��� �̿��� ���� ��� ����
            UnityEditor.AssetDatabase.CreateAsset(songData, filePath);
            UnityEditor.AssetDatabase.SaveAssets();

            Debug.Log("SO ���� ����");
        }
#else
        Debug.LogWarning("AssetDatabase is not available at runtime!");
#endif
    }
}
