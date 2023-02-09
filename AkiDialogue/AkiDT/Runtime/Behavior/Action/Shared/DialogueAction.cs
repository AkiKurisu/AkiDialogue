using Kurisu.AkiBT;
namespace Kurisu.AkiDT
{
    public abstract class DialogueAction : Action
    {
        protected IDialogueTree dialogueTree;
        public override void Awake()
        {
            dialogueTree=tree as IDialogueTree;
        }
    }
}
