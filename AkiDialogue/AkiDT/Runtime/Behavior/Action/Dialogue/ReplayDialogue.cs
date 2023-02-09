using Kurisu.AkiBT;
namespace Kurisu.AkiDT
{
    [AkiInfo("ReplayDialogue:重新播放当前对话树")]
    [AkiLabel("Dialogue:ReplayDialogue")]
    [AkiGroup("Dialogue")]
    public class ReplayDialogue : Action
    {
        private DialogueTree currentTree;
        public override void Awake()
        {
            currentTree=gameObject.GetComponent<DialogueTree>();
        }
        protected override Status OnUpdate()
        {
            currentTree.PlayDialogue();
            return Status.Success;
        }
    }
}
