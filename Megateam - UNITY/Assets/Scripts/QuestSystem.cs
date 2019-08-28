using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Story/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public int progress;
}

[System.Serializable]
public class Action
{
    public Quest quest;
    public int progressTo;
}