namespace Kurisu.AkiDialogue
{
    /// <summary>
    /// 接口：提供对话片段
    /// </summary>
    public interface IProvideDialogue
    {

        /// <summary>
        /// 需要本地化处理
        /// </summary>
        /// <value></value>
        public bool NeedConvert{get;}
        /// <summary>
        /// 根据索引获取下一个对话片段
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        DialoguePiece GetNext(string ID);
        /// <summary>
        /// 获取下一个对话片段
        /// </summary>
        /// <returns></returns>
        DialoguePiece GetNext();
        /// <summary>
        /// 判断是否还存在下一个对话片段
        /// </summary>
        /// <returns></returns>
        bool HaveNext();
        /// <summary>
        /// 重置和链接对话片段
        /// </summary>
        void Generate();
    }
}
