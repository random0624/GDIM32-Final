using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "")]
public class DialogueData : ScriptableObject
{
    [Header("Properties")]
    public List<string> startingText;
    public List<string> finalText;
}
