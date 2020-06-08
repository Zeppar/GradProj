using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAsset", menuName = "ChaseTheLight/New DialogueInfo")]
public class DialogueAsset : ScriptableObject
{
    public List<DialogueInfo> dialogueInfos;
}

