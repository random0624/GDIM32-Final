using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "")]
public class DialogueData : ScriptableObject
{
    [Header("Properties")]
    public string [] lines;
}
