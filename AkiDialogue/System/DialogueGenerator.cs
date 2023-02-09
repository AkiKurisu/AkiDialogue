using System.Collections.Generic;
using Kurisu.AkiDialogue.Utility;
namespace Kurisu.AkiDialogue
{
    /// <summary>
    /// 对话生成器,通过运行时填写数据并Update给对话系统实现动态对话,对话链接方式通过字典建立索引
    /// </summary>
    public class DialogueGenerator : IProvideDialogue
    {
        public DialogueGenerator(bool needConvert=false)
        {
            this.needConvert=needConvert;
        }
        private List<DialoguePiece> dialoguePieces=new List<DialoguePiece>();
        private Dictionary<string,DialoguePiece>dialogueIndex=new Dictionary<string, DialoguePiece>();
        private int nextIndex;
        private readonly bool needConvert;
        public bool NeedConvert => needConvert;
        void IProvideDialogue.Generate()
        {
            nextIndex=0;
            dialogueIndex.Clear();
            foreach (DialoguePiece piece in dialoguePieces)
            {
                if (!dialogueIndex.ContainsKey(piece.ID))
                    dialogueIndex.Add(piece.ID, piece);
            }
        }
        DialoguePiece IProvideDialogue.GetNext(string ID)
        {
            nextIndex=dialoguePieces.IndexOf(dialogueIndex[ID])+1;
            return dialogueIndex[ID];
        }
        DialoguePiece IProvideDialogue.GetNext()
        {
            int index=nextIndex;
            nextIndex++;
            return dialoguePieces[index];
        }
        public void Clear()
        {
            dialoguePieces.ForEach(x=>x.ObjectPushPool());
            dialoguePieces.Clear();
        }
        bool IProvideDialogue.HaveNext()
        {
           return nextIndex<dialoguePieces.Count;
        }
        public void AddPiece(DialoguePiece piece)
        {
            dialoguePieces.Add(piece);
        }
        public static DialoguePiece CreatePiece()
        {
            return PoolManager.Instance.GetObject<DialoguePiece>().Reset();
        }
        public static DialogueOption CreateOption()
        {
            return PoolManager.Instance.GetObject<DialogueOption>().Reset();
        }
    }
}
