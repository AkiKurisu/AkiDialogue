using Kurisu.AkiBT;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [AkiInfo("PlayDialogueTree:播放另一棵对话树")]
    [AkiLabel("Dialogue:PlayDialogueTree")]
    [AkiGroup("Dialogue")]
    public class PlayDialogueTree : Action
    {
        [SerializeField]
        private DialogueTree dialogueTree;
        protected override Status OnUpdate()
        {
            dialogueTree.PlayDialogue();
            return Status.Success;
        }
    }
}
