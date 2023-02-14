using Kurisu.AkiBT;
using Kurisu.AkiDialogue;
namespace Kurisu.AkiDT
{
    public interface IDialogueTree:IBehaviorTree
    {
        /// <summary>
        /// 当前片段内容
        /// </summary>
        /// <value></value>
        public string CurrentPieceText{get;}
        /// <summary>
        /// 当前选项内容
        /// </summary>
        /// <value></value>
        public string CurrentOptionText{get;}
        /// <summary>
        /// 清空对话
        /// </summary>
        void ClearDialogue();
        /// <summary>
        /// 清空当前对话片段
        /// </summary>
        void ClearPiece();
        /// <summary>
        /// 清空当前选项
        /// </summary>
        void ClearOption();
        /// <summary>
        /// 创建片段
        /// </summary>
        void CreatePiece();
        /// <summary>
        /// 创建选项
        /// </summary>
        void CreateOption();
        /// <summary>
        /// 添加对话片段
        /// </summary>
        /// <param name="piece"></param>
        void AddPiece(DialoguePiece piece);
        /// <summary>
        /// 添加对话选项
        /// </summary>
        /// <param name="option"></param>
        void AddOption(DialogueOption option);
        /// <summary>
        /// 注册选项回调
        /// </summary>
        /// <param name="callBack"></param>
        void RegisterOptionSelectCallBack(System.Action callBack);
        /// <summary>
        /// 添加选项事件
        /// </summary>
        /// <param name="scriptableEvent"></param>
        void AddOptionSelectEvent(ScriptableEvent scriptableEvent);
        /// <summary>
        /// 修改当前对话片段内容
        /// </summary>
        /// <param name="Text"></param>
        void ModifyPieceText(string Text);
        /// <summary>
        /// 修改当前对话片段ID
        /// </summary>
        /// <param name="ID"></param>
        void ModifyPieceID(string ID);
        /// <summary>
        /// 修改当前选项内容
        /// </summary>
        /// <param name="Text"></param>
        void ModifyOptionText(string Text);
        /// <summary>
        /// 修改当前选项目标ID
        /// </summary>
        /// <param name="target"></param>
        void ModifyOptionTarget(string target);
        /// <summary>
        /// 更新对话到对话系统中
        /// </summary>
        void UpdateDialogue();
        /// <summary>
        /// 更新对话片段
        /// </summary>
        void UpdatePiece();
        /// <summary>
        /// 更新对话选项
        /// </summary>
        void UpdateOption();
        void SetConvert(bool needConvert);
    }
}
