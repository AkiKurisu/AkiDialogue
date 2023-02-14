using Kurisu.AkiDT;
using Kurisu.AkiBT;
using UnityEngine;
using UnityEngine.Localization;
namespace Kurisu.AkiDT.Localization
{
    [AkiInfo("LocalizedPieceText:修改对话片段的内容,使用本地化")]
    [AkiLabel("Piece:LocalizedText")]
    [AkiGroup("Piece")]
    public class LocalizedPieceText : DialogueAction
    {
        [SerializeField]
        private LocalizedString localizedString;
        [SerializeField,Tooltip("勾选后会在Start时异步加载并缓存")]
        private bool asyncGetOnStart;
        private string cache;
        public override void OnAwake()
        {
            if(asyncGetOnStart)localizedString.GetLocalizedStringAsync().Completed+=(obj)=>cache=obj.Result;
        }
        protected override Status OnUpdate()
        {
            dialogueTree.ModifyPieceText(asyncGetOnStart?cache:localizedString.GetLocalizedString());
            return Status.Success;
        }
    }
}