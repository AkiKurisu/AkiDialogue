using System.Collections.Generic;
using Kurisu.AkiBT;
using UnityEngine;
using Kurisu.AkiDialogue;
using Kurisu.AkiDialogue.Utility;
namespace Kurisu.AkiDT
{
    public interface IDialogueTree:IBehaviorTree
    {
        void ClearDialogue();
        void ClearPiece();
        void ClearOption();
        void CreatePiece();
        void CreateOption();
        void AddPiece(DialoguePiece piece);
        void AddOption(DialogueOption option);
        void RegisterOptionSelectCallBack(System.Action callBack);
        void AddOptionSelectEvent(ScriptableEvent scriptableEvent);
        void ModifyPieceText(string Text);
        void ModifyPieceID(string ID);
        void ModifyOptionText(string Text);
        void ModifyOptionTarget(string target);
        void UpdateDialogue();
        void UpdatePiece();
        void UpdateOption();
    }
    [DisallowMultipleComponent]
    public class DialogueTree :MonoBehaviour,IDialogueTree
    {
        #region  Tree Part
        [HideInInspector,SerializeReference]
        private Root root = new Root();
        Object IBehaviorTree._Object=>this;
        [HideInInspector]
        [SerializeReference]
        private List<SharedVariable> sharedVariables = new List<SharedVariable>();
        [SerializeField,Tooltip("使用外部对话树替换组件内对话树,保存时会覆盖组件内对话树")]
        private DialogueTreeSO externalDialogueTree;
        public DialogueTreeSO ExternalData{get=>externalDialogueTree;set=>externalDialogueTree=value;}
        #if UNITY_EDITOR
        public BehaviorTreeSO ExternalBehaviorTree=>externalDialogueTree;

        [SerializeField,HideInInspector]
        private bool autoSave;
        [SerializeField,HideInInspector]
        private string savePath="Assets";
        public string SavePath
        {
                get => savePath;
                set => savePath = value;

        }
        public bool AutoSave
        {
            get => autoSave;
            set => autoSave = value;
        }
        [SerializeField,HideInInspector]
        private List<GroupBlockData> blockData=new List<GroupBlockData>();
        public List<GroupBlockData> BlockData { get => blockData; set=>blockData=value;}
        #endif
        public Root Root
        {
            get=>root;
            #if UNITY_EDITOR
                set => root = value;
            #endif
        }
        public List<SharedVariable> SharedVariables
        {
            get=>sharedVariables;
            #if UNITY_EDITOR
                set=>sharedVariables=value;
            #endif
        }

        /// <summary>
        /// 获取共享变量
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SharedVariable<T> GetShareVariable<T>(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                Debug.LogError($"<color=#ff2f2f>AkiDT</color>共享变量名称不能为空",this);
                return null;
            }
            foreach(var variable in sharedVariables)
            {
                if(variable.Name.Equals(name))
                {
                    if( variable is SharedVariable<T>)return variable as SharedVariable<T>;
                    else Debug.LogError($"<color=#ff2f2f>AkiDT</color>{name}名称变量不是{typeof(T).Name}类型",this);
                    return null;
                }
            }
            Debug.LogError($"<color=#ff2f2f>AkiDT</color>没有找到共享变量:{name}",this);
            return null;
        }
        #endregion
        /// <summary>
        /// 需要本地化
        /// </summary>
        /// <returns></returns>
        private DialogueGenerator generator=new DialogueGenerator(true);
        private DialoguePiece tempPiece;
        private DialogueOption tempOption;
        [SerializeField]
        private bool useSOData;
        public bool UseSOData{get=>useSOData;set=>useSOData=value;}
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
        private void Awake() {
            foreach(var variable in sharedVariables)
            {
                if(variable is PieceID)(variable as PieceID).Value=System.Guid.NewGuid().ToString();
            }
            root.Run(gameObject,this);
            root.Awake();
        }
        private void Start() {
            root.Start();
        }
        /// <summary>
        /// 播放对话
        /// </summary>
        public void PlayDialogue()
        {
            if(useSOData)
            {
                PlayDataDialogue();
                return;
            }
            root.PreUpdate();
            root.Update();
            root.PostUpdate();
        }
        private void PlayDataDialogue()
        {
            if(externalDialogueTree==null)
            {
                Debug.LogError("<color=#ff2f2f>AkiDT</color>没有外部对话树",this);
                return;
            }
            externalDialogueTree.GenerateID();
            externalDialogueTree.Init(gameObject);
            externalDialogueTree.Update();
        }
    }
}
