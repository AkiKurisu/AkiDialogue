using Kurisu.AkiDialogue.Utility;
namespace Kurisu.AkiDialogue
{
    public interface IHandleDialogue
    {
        /// <summary>
        /// 关闭对话栏
        /// </summary>
        void CloseDialogue();
        /// <summary>
        /// 打开对话栏
        /// </summary>
        void OpenDialogue();
        /// <summary>
        /// 下一段对话
        /// </summary>
        /// <param name="nextPieceID"></param>
        void NextDialogue(string nextPieceID);
        /// <summary>
        /// 更新对话
        /// </summary>
        /// <param name="data">对话数据</param>
        void UpdateDialogueData(IProvideDialogue data);
        /// <summary>
        /// 申明一段对话结束
        /// </summary>
        void DialogueOver();
        /// <summary>
        /// 对话结束事件
        /// </summary>
        /// <value></value>
        public AkiEvent OnDialogueOverEvent{get;}
        /// <summary>
        /// 对话打开事件
        /// </summary>
        /// <value></value>
        public AkiEvent OnDialogueOpenEvent{get;}
        /// <summary>
        /// 对话片段播放事件
        /// </summary>
        /// <value></value>
        public AkiEvent<DialoguePiece> OnDialoguePiecePlayEvent{get;}
        /// <summary>
        /// 对话关闭事件
        /// </summary>
        /// <value></value>
        public AkiEvent OnDialogueCloseEvent{get;}     
        public IProvideDialogue CurrentDialogue{get;}   
    }
}
