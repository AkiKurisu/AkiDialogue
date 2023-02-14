using Kurisu.AkiBT;
using UnityEngine;
using UnityEngine.Localization;
namespace Kurisu.AkiDT.Localization
{
    [AkiInfo("LocalizedOptionText修改对话选项的内容,使用本地化")]
    [AkiLabel("Option:LocalizedText")]
    [AkiGroup("Option")]
    public class LocalizedOptionText : DialogueAction
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
            dialogueTree.ModifyOptionText(asyncGetOnStart?cache:localizedString.GetLocalizedString());
            return Status.Success;
        }
    }
}