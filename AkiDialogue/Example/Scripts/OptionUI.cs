using UnityEngine;
using UnityEngine.UI;
namespace Kurisu.AkiDialogue.Example
{
/// <summary>
/// 对话选项UI控制
/// </summary>
public class OptionUI : MonoBehaviour
{
    private Text optionText;
    private Button button;
    private DialogueOption option;
    private IHandleDialogue dialogueSystem;
    private RectTransform rectTransform;
    private void Awake() {
        rectTransform=GetComponent<RectTransform>();
        optionText=GetComponentInChildren<Text>();
        button=GetComponent<Button>();
        button.onClick.AddListener(OnOptionClick);   
    }
    private void OnDestroy() {
        button.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// 更新选项卡
    /// </summary>
    /// <param name="piece">对话内容</param>
    /// <param name="option">选项内容</param>
    public void UpdateOption(IHandleDialogue dialogueSystem,string text,DialoguePiece piece,DialogueOption option)
    {
        this.dialogueSystem=dialogueSystem;
        this.option=option;
        optionText.text=text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
    private void TriggerAllEvent()
    {
        if(option.scriptableEvents==null||option.scriptableEvents.Count==0)return;
        foreach(var scriptableEvent in option.scriptableEvents)scriptableEvent.Trigger();
    }
    private void OnOptionClick()
    {
        option.OnSelectAction?.Invoke();
        TriggerAllEvent();
        if(string.IsNullOrEmpty(option.targetID))
        {
            dialogueSystem.CloseDialogue();
            return;
        }
        dialogueSystem.NextDialogue(option.targetID);
    }
}
}