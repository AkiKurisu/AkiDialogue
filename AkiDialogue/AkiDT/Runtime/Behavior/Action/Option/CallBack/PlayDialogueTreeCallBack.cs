using Kurisu.AkiBT;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [AkiInfo("PlayDialogueTreeCallBack:添加播放对话树回调")]
    [AkiLabel("CallBack:PlayDialogueTree")]
    [AkiGroup("CallBack")]
    public class PlayDialogueTreeCallBack : OptionCallBack
    {
        [SerializeField]
        private DialogueTree otherDialogueTree;
        protected override System.Action AddCallBack()
        {
            return otherDialogueTree.PlayDialogue;
        }
    }
}
