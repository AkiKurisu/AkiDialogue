using Kurisu.AkiBT;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [AkiInfo("Piece:对话片段,开始时创建对话片段,所有结点运行结束后向当前对话添加片段数据,如果中途出现失败则取消添加")]
    [AkiLabel("Piece")]
    [AkiGroup("Dialogue")]
    [CopyDisable]
    public class Piece : Composite
    {
        [SerializeField,AkiLabel("片段ID"),Tooltip("你不需要填写该共享变量,因为其值会在运行时自动生成")]
        private PieceID pieceID;
        [SerializeField,AkiLabel("片段内容")]
        private SharedString pieceText;
        private IDialogueTree dialogueTree;
        protected override void OnAwake() {
            dialogueTree=tree as IDialogueTree;
            InitVariable(pieceID);
            InitVariable(pieceText);
        }
        public override bool CanUpdate()
        {
            //this node can update when all children can update
            foreach (var child in Children)
            {
                if (!child.CanUpdate())
                {
                    return false;
                }
            }
            return true;
        }

        protected override Status OnUpdate()
        {
            dialogueTree.CreatePiece();
            dialogueTree.ModifyPieceID(pieceID.Value);
            dialogueTree.ModifyPieceText(pieceText.Value);
            return UpdateWhileSuccess(0);
        }

        private Status UpdateWhileSuccess(int start)
        {
            for (var i = start; i < Children.Count; i++)
            {
                var target = Children[i];
                var childStatus = target.Update();
                if (childStatus == Status.Success)
                {
                    continue;
                }
                dialogueTree.ClearPiece();
                return Status.Failure;
            }
            dialogueTree.UpdatePiece();
            return Status.Success;
        }
    }
}

