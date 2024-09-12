using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DLDatas", menuName = "SOData/DLData", order = 2)]
public class DLDatas : ScriptableObject
{
    public TalkData[] talkDataArray;

    [System.Serializable]
    public class TalkData
    {
        public string name;
        public string line;
    }
}
