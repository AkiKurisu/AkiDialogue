using Kurisu.AkiBT;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [AkiInfo("Option:选项,开始时创建选项,所有结点运行结束后向当前对话片段添加选项,如果中途出现失败则取消添加")]
    [AkiLabel("Option")]
    [AkiGroup("Dialogue")]
    public class Option : Composite
    {
        private IDialogueTree dialogueTree;
        [SerializeField,AkiLabel("选项内容")]
        private SharedString optionText;
        protected override void OnAwake() {
            dialogueTree=tree as IDialogueTree;
            InitVariable(optionText);
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
            dialogueTree.CreateOption();
            dialogueTree.ModifyOptionText(optionText.Value);
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
                dialogueTree.ClearOption();
                return Status.Failure;
            }
            dialogueTree.UpdateOption();
            return Status.Success;
        }
    }
}

