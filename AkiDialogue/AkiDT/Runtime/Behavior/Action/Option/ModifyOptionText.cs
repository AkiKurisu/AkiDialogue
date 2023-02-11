using Kurisu.AkiBT;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [AkiInfo("ModifyOptionText:修改选项内容")]
    [AkiLabel("Option:Text")]
    [AkiGroup("Option")]
    public class ModifyOptionText : DialogueAction
    {
        [SerializeField,AkiLabel("选项内容")]
        private SharedString Text;
        public override void OnAwake()
        {
           InitVariable(Text);
        }
        protected override Status OnUpdate()
        {
            dialogueTree.ModifyOptionText(Text.Value);
            return Status.Success;
        }
    }
}
