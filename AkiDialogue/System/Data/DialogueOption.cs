using System.Collections.Generic;
using UnityEngine;
namespace Kurisu.AkiDialogue
{
/// <summary>
/// 对话选项
/// </summary>
[System.Serializable]
public class DialogueOption 
{
    public string text;
    public string targetID;
    public List<ScriptableEvent> scriptableEvents=new List<ScriptableEvent>();
    /// <summary>
    /// 选项事件,运行时添加
    /// </summary>
    [HideInInspector]
    public System.Action OnSelectAction;
    public DialogueOption SetUp(string text,string targetID=null,System.Action onSelectAction=null)
    {
        this.text=text;
        this.targetID=targetID;
        this.OnSelectAction=onSelectAction;
        return this;
    }
    public DialogueOption Reset()
    {
        text=string.Empty;
        targetID=null;
        OnSelectAction=null;
        scriptableEvents.Clear();
        return this;
    }
    public DialogueOption Clone()
    {
        var option=DialogueGenerator.CreateOption();
        option.text=text;
        option.targetID=targetID;
        foreach(var scriptableEvent in scriptableEvents) option.scriptableEvents.Add(scriptableEvent);
        return option;
    }
}
}