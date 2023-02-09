using Kurisu.AkiBT;
namespace Kurisu.AkiDT
{
    public abstract class DialogueAction : Action
    {
        protected IDialogueTree dialogueTree;
        public sealed override void Awake()
        {
            dialogueTree=tree as IDialogueTree;
            OnAwake();
        }
        public virtual void OnAwake(){}
    }
}
