using UnityEngine;
using Kurisu.AkiDialogue;
using Kurisu.AkiBT;
namespace Kurisu.AkiDT
{
    [AkiInfo("ModifyOptionEvent:添加选项事件")]
    [AkiLabel("Option:Event")]
    [AkiGroup("Option")]
    public class ModifyOptionEvent : DialogueAction
    {
        [SerializeField]
        private ScriptableEvent scriptableEvent;
        protected override Status OnUpdate()
        {
            if(scriptableEvent)dialogueTree.AddOptionSelectEvent(scriptableEvent);
            else
            {
                Debug.LogWarning("ScriptableEvent为空",gameObject);
            }
            return Status.Success;
        }
    }
}
