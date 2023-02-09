using Kurisu.AkiBT;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [AkiInfo("ModifyPieceText:修改片段内容")]
    [AkiLabel("Piece:Text")]
    [AkiGroup("Piece")]
    public class ModifyPieceText : DialogueAction
    {
        [SerializeField]
        private SharedString Text;
        public override void OnAwake()
        {
           InitVariable(Text);
        }
        protected override Status OnUpdate()
        {
            dialogueTree.ModifyPieceText(Text.Value);
            return Status.Success;
        }
    }
}
