using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ConversationTriggerScriptableObject", order = 2)]
public class ConversTrriggerSO : ScriptableObject {


    public Announce.EventTypes eventType;
    public int OptionIndex;
    public string TextToSay;

    public float time;

}
