using UnityEditor;
using Kurisu.AkiBT;
using Kurisu.AkiBT.Editor;
namespace Kurisu.AkiDT.Editor
{
    public class DialogueTreeView : BehaviorTreeView
    {
        public DialogueTreeView(IBehaviorTree bt, EditorWindow editor) : base(bt, editor)
        {
        }
        public override sealed string treeEditorName=>"AkiDT";
        public override bool CanSaveToSO=>behaviorTree is DialogueTree;
    }
}