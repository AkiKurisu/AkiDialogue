using Kurisu.AkiBT;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [AkiInfo("Dialogue:对话,开始时进入对话,所有结点运行结束后更新数据")]
    [AkiLabel("Dialogue")]
    [AkiGroup("Dialogue")]
    public class Dialogue : Composite
    {
        private IDialogueTree dialogueTree;
        [SerializeField,Tooltip("勾选后会将对话标记为需要转换,可用于View部分统一进行文本修改(例如本地化)。如果无需修改或使用AkiDT的Localization结点则可以不开启。")]
        private bool needConvert;
        protected override void OnAwake() {
            dialogueTree=tree as IDialogueTree;
        }
        protected override Status OnUpdate()
        {
            dialogueTree.ClearDialogue();
            dialogueTree.SetConvert(needConvert);
            foreach (var c in Children)
            {
                c.Update();
            }
            dialogueTree.UpdateDialogue();
            return Status.Success;
        }
    }
    }

