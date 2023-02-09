using System.Collections.Generic;
using UnityEngine;
namespace Kurisu.AkiDialogue
{
[CreateAssetMenu(fileName = "DialogueData", menuName = "AkiDialogue/DialogueData")]
public class DialogueData_SO : ScriptableObject,IProvideDialogue
{
    public List<DialoguePiece> dialoguePieces=new List<DialoguePiece>();   
    private Dictionary<string,DialoguePiece>dialogueIndex=new Dictionary<string, DialoguePiece>();
    private int nextIndex;
    public bool NeedConvert => true;
    private void RefreshIndex()
    {
        dialogueIndex.Clear();
        foreach (DialoguePiece piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID, piece);
        }
    }
    void IProvideDialogue.Generate()
    {
        nextIndex=0;
    }
    public DialoguePiece GetNext(string ID)
    {
        nextIndex=dialoguePieces.IndexOf(dialogueIndex[ID])+1;
        return dialogueIndex[ID];
    }
    public DialoguePiece GetNext()
    {
        int index=nextIndex;
        nextIndex++;
        return dialoguePieces[index];
    }
    public bool HaveNext()
    {
        return nextIndex<dialoguePieces.Count;
    }
#if UNITY_EDITOR
    void OnValidate()//仅在编辑器内执行导致打包游戏后字典空了
    {
        RefreshIndex();
    }
#else
    void Awake()//保证在打包执行的游戏里第一时间获得对话的所有字典匹配 
    {
        RefreshIndex();
    }
#endif
    }
}