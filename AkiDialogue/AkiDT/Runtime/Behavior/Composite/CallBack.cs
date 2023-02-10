using Kurisu.AkiBT;
namespace Kurisu.AkiDT
{
    [AkiInfo("CallBack:回调,开始时创建回调,不会运行结点而是将所有可以运行的结点注册进选项的回调函数中")]
    [AkiLabel("CallBack")]
    [AkiGroup("Dialogue")]
    public class CallBack : Composite
    {
        private IDialogueTree dialogueTree;
        protected override void OnAwake() {
            dialogueTree=tree as IDialogueTree;
        }
        protected override Status OnUpdate()
        {
            foreach (var child in Children)
            {
                if(child.CanUpdate())dialogueTree.RegisterOptionSelectCallBack(()=>child.Update());
            }
            return Status.Success;
        }
    }
}

