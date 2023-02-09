using Kurisu.AkiBT;
namespace Kurisu.AkiDT
{
    public abstract class OptionCallBack : DialogueAction
    {
        protected abstract System.Action AddCallBack();
        protected override Status OnUpdate()
        {
            dialogueTree.RegisterOptionSelectCallBack(AddCallBack());
            return Status.Success;
        }
    }
}
