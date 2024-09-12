using UnityEngine;

//order�� �켱����
/// <summary>
/// 2�� �Ľ̵Ǵ� ������ ��� SOŬ����
/// </summary>
[CreateAssetMenu(fileName = "SODatas", menuName = "SOData/NoteData", order = 1)]
public class SODatas : ScriptableObject
{
    public string title;
    public string artist;
    public string level;
    public NoteData[] noteDataArray; // ��Ʈ ������ �迭

    [System.Serializable]
    public class NoteData
    {
        public int x;
        public int y;
        public int time;
        public int type;
        //public int dummy;
        public int longNoteTime;
    }
}
