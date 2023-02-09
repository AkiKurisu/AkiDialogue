using Kurisu.AkiBT;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [AkiInfo("ModifyOptionTargetID:修改选项目标ID")]
    [AkiLabel("Option:TargetID")]
    [AkiGroup("Option")]
    public class ModifyOptionTargetID : DialogueAction
    {
        [SerializeField,AkiLabel("目标ID"),Tooltip("你不需要填写该共享变量,因为其值会在运行时自动生成"),Reference]
        private PieceID TargetID;
        public override void OnAwake()
        {
           InitVariable(TargetID);
        }
        protected override Status OnUpdate()
        {
            dialogueTree.ModifyOptionTarget(TargetID.Value);
            return Status.Success;
        }
    }
}
