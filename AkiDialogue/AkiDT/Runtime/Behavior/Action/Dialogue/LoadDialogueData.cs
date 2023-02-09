using Kurisu.AkiBT;
using UnityEngine;
using Kurisu.AkiDialogue;
namespace Kurisu.AkiDT
{
    [AkiInfo("LoadDialogueData:加载对话数据,使用保存在DialogueData的对话数据,除了ScriptableEvent所有对话数据均以深拷贝形式加载")]
    [AkiLabel("Dialogue:LoadData")]
    [AkiGroup("Dialogue")]
    public class LoadDialogueData : DialogueAction
    {
        [SerializeField]
        private DialogueData_SO dialogueData;
        protected override Status OnUpdate()
        {
            foreach(var dialoguePiece in dialogueData.dialoguePieces)
            {
                dialogueTree.AddPiece(dialoguePiece.Clone());
            }
            return Status.Success;
        }
    }
}