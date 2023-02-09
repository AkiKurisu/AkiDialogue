using System.Collections.Generic;
using Kurisu.AkiDialogue.Utility;
using UnityEngine;
namespace Kurisu.AkiDialogue
{
    public class DialogueManager : MonoBehaviour, IHandleDialogue
    {
        private static DialogueManager instance;
        public static DialogueManager Instance =>instance??GetInstance();
        private enum DialogueState
        {
            未开始,对话中
        }
        [SerializeField]
        private DialogueState currentState;
        private IProvideDialogue dialogue;
        private static DialogueManager GetInstance()
        {
            instance= GameObject.FindObjectOfType<DialogueManager>();
            if(instance==null)
            {
                Debug.LogError("缺少Dialogue Manager");
            }
            return instance;
        }
        private void Awake() {
            OnDialogueOpenEvent=new AkiEvent();
            OnDialogueCloseEvent=new AkiEvent();
            OnDialoguePiecePlayEvent=new AkiEvent<DialoguePiece>();
            OnDialogueDataUpdateEvent=new AkiEvent();
            OnDialogueOverEvent=new AkiEvent();
        }
        public AkiEvent OnDialogueOpenEvent{get;private set;}
        public AkiEvent<DialoguePiece> OnDialoguePiecePlayEvent{get;private set;}
        public AkiEvent OnDialogueCloseEvent{get;private set;}
        public AkiEvent OnDialogueDataUpdateEvent{get;private set;}
        public AkiEvent OnDialogueOverEvent{get;private set;}
        public IProvideDialogue CurrentDialogue=>dialogue;
        private Queue<IProvideDialogue> dialogueQueue=new Queue<IProvideDialogue>();
        public void CloseDialogue()
        {
            currentState=DialogueState.未开始;
            if(dialogueQueue.Count!=0)
            {
                UpdateDialogueData(dialogueQueue.Dequeue());
                return;
            }
            OnDialogueCloseEvent?.Trigger();
        }
        public void OpenDialogue()
        {
            OnDialogueOpenEvent?.Trigger();
        }
        public void UpdateDialogueData(IProvideDialogue data)
        {
            if(currentState==DialogueState.对话中)
            {
                dialogueQueue.Enqueue(data);
                return;
            }
            OpenDialogue();
            data.Generate();
            dialogue=data;
            currentState=DialogueState.对话中;
            PlayDialogue(dialogue.GetNext());//播放第一个片段
        }
        public void DialogueOver()
        {
            OnDialogueOverEvent?.Trigger();
            CloseDialogue();
        }
        private void PlayDialogue(DialoguePiece piece)
        {
            OnDialoguePiecePlayEvent.Trigger(piece);
        }
        void IHandleDialogue.NextDialogue(string nextPieceID)
        {
            PlayDialogue(dialogue.GetNext(nextPieceID));
        }
    }
}
