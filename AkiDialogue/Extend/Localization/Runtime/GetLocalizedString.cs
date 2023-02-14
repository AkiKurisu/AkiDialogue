using UnityEngine;
using UnityEngine.Localization;
using Kurisu.AkiBT;
namespace Kurisu.AkiDT.Localization
{
    [AkiInfo("Action:获取本地化字符串")]
    [AkiLabel("String:GetLocalizedString")]
    [AkiGroup("String")]
    public class GetLocalizedString : Action
    {
        [SerializeField]
        private LocalizedString localizedString;
        [SerializeField,ForceShared]
        private SharedString storeResult;
        [SerializeField,Tooltip("勾选后会在Start时异步加载并缓存")]
        private bool asyncGetOnStart;
        private string cache;
        public override void Awake()
        {
            InitVariable(storeResult);
            if(asyncGetOnStart)localizedString.GetLocalizedStringAsync().Completed+=(obj)=>cache=obj.Result;
        }
        protected override Status OnUpdate()
        {
            storeResult.Value=asyncGetOnStart?cache:localizedString.GetLocalizedString();
            return Status.Success;
        }
    }
}
