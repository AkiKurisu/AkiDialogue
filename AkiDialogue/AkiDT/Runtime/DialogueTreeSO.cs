using Kurisu.AkiBT;
using Kurisu.AkiDialogue;
using Kurisu.AkiDialogue.Utility;
using UnityEngine;
namespace Kurisu.AkiDT
{
    [CreateAssetMenu(fileName = "DialogueTreeSO", menuName = "AkiDialogue/DialogueTreeSO")]
    public class DialogueTreeSO : BehaviorTreeSO,IDialogueTree
    {
        #if UNITY_EDITOR
        [SerializeField,HideInInspector]
        private DialogueTreeSO externalDialogueTree;
        public sealed override BehaviorTreeSO ExternalBehaviorTree=>externalDialogueTree;
        # endif
        private DialogueGenerator generator=new DialogueGenerator();
        private DialoguePiece tempPiece;
        private DialogueOption tempOption;
        public string CurrentPieceText=>tempPiece?.text;
        public string CurrentOptionText=>tempOption?.text;
        public void ClearDialogue()
        {
            generator.Clear();
        }
        public void ClearPiece()
        {
            tempPiece?.ObjectPushPool();
            tempPiece=null;
        }
        public void ClearOption()
        {
            tempOption?.ObjectPushPool();
            tempOption=null;
        }
        public void CreatePiece()
        {
            ClearPiece();
            tempPiece=DialogueGenerator.CreatePiece();
        }
        public void CreateOption()
        {
            ClearOption();
            tempOption=DialogueGenerator.CreateOption();
        }
        public void AddPiece(DialoguePiece piece)
        {
            generator.AddPiece(piece);
        }
        public void AddOption(DialogueOption option)
        {
            if(tempPiece==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话片段 无效Option",this);
                return;
            }
            tempPiece.options.Add(option);
        }
        public void ModifyPieceText(string Text)
        {
            if(tempPiece==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话片段 无效PieceText:{Text}",this);
                return;
            }
            tempPiece.text=Text;
        }
        public void ModifyPieceID(string ID)
        {
            if(tempPiece==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话片段 无效PieceID:{ID}",this);
                return;
            }
            tempPiece.ID=ID;
        }
        public void ModifyOptionText(string Text)
        {
            if(tempOption==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话选项 无效OptionText:{Text}",this);
                return;
            }
            tempOption.text=Text;
        }
        public void ModifyOptionTarget(string target)
        {
            if(tempOption==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话选项 无效OptionTarget:{target}",this);
                return;
            }
            tempOption.targetID=target;
        }
        public void UpdateDialogue()
        {
            DialogueManager.Instance.UpdateDialogueData(generator);
        }
        public void RegisterOptionSelectCallBack(System.Action callBack)
        {
            if(tempOption==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话选项 无效RegisterCallBack",this);
                return;
            }
            tempOption.OnSelectAction+=callBack;
        }
        public void AddOptionSelectEvent(ScriptableEvent scriptableEvent)
        {
            if(tempOption==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话选项 无效AddEvent",this);
                return;
            }
            tempOption.scriptableEvents.Add(scriptableEvent);
        }
        public void UpdatePiece()
        {
            if(tempPiece==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话片段 更新无效",this);
                return;
            }
            generator.AddPiece(tempPiece);
            tempPiece=null;
        }
        public void UpdateOption()
        {
            if(tempOption==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话选项 更新无效",this);
                return;
            }
            if(tempPiece==null)
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>你没有初始化对话片段 更新无效",this);
                return;
            }
            tempPiece.options.Add(tempOption);
            tempOption=null;
        }
        internal void GenerateID()
        {
            foreach(var variable in SharedVariables)
            {
                if(variable is PieceID)(variable as PieceID).Value=System.Guid.NewGuid().ToString();
            }
        }
        internal DialoguePiece FindPiece(string pieceID)
        {
            return generator.FindPiece(pieceID);
        }
        public void SetConvert(bool needConvert)=>generator.NeedConvert=needConvert;
    }
}
