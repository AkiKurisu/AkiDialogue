using Kurisu.AkiBT;
namespace Kurisu.AkiDT
{
    [AkiInfo("Dialogue:对话,开始时进入对话,所有结点运行结束后更新数据")]
    [AkiLabel("Dialogue")]
    [AkiGroup("Dialogue")]
    public class Dialogue : Composite
    {
        private IDialogueTree dialogueTree;
        protected override void OnAwake() {
            dialogueTree=tree as IDialogueTree;
        }
        protected override Status OnUpdate()
        {
            dialogueTree.ClearDialogue();
            foreach (var c in Children)
            {
                c.Update();
            }
            dialogueTree.UpdateDialogue();
            return Status.Success;
        }
    }
    }

