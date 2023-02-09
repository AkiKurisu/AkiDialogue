using UnityEngine;
using Kurisu.AkiDialogue;
using Kurisu.AkiDialogue.Utility;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Kurisu.AkiDialogue.Example
{
    public class DialogueDisplayUI : MonoBehaviour
    {
        [SerializeField]
        private Text mainText;
        [SerializeField]
        private GameObject dialoguePanel;
        [SerializeField]
        private Transform optionPanel;
        private List<OptionUI>optionSlots=new List<OptionUI>();
        [SerializeField]
        private OptionUI optionPrefab;
        [SerializeField]
        private Transform[] slotPoints;
        private IHandleDialogue dialogueSystem;
        void Awake()
        {
            dialogueSystem=GetComponent<IHandleDialogue>();
        }
        private void Start() {
            dialogueSystem.OnDialogueOpenEvent.Register(OpenDialogueHandler).UnRegisterWhenGameObjectDestroyed(gameObject);
            dialogueSystem.OnDialogueCloseEvent.Register(CloseDialogueHandler).UnRegisterWhenGameObjectDestroyed(gameObject);
            dialogueSystem.OnDialoguePiecePlayEvent.Register(PlayDialogue).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        private void CloseDialogueHandler()
        {
            dialoguePanel.SetActive(false);
        }
        private void OpenDialogueHandler()
        {
            dialoguePanel.SetActive(true);

        }
        private void PlayDialogue(DialoguePiece piece)
        {
            foreach(var slot in optionSlots)Destroy(slot.gameObject);
            optionSlots.Clear();
            SubtitlePlay(piece.text,piece,0);
        }
        /// <summary>
        /// 字幕式播放,按字分割
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="startIndex"></param>
        void SubtitlePlay(string text,DialoguePiece piece,int startIndex)
        {
            const int maxWord=30;
            mainText.text=string.Empty;
            if(startIndex+maxWord>=text.Length){
                int Length=text.Length-startIndex;
                StartCoroutine(PlayText(text.Substring(startIndex,Length),()=>{GenerateButton(piece);}));
                return;
            }
            StartCoroutine(PlayText(text.Substring(startIndex,maxWord),()=>{SubtitlePlay(text,piece,startIndex+maxWord);}));
        }  
        private StringBuilder stringBuilder=new StringBuilder();
        IEnumerator PlayText(string text,System.Action callBack)
        {
            WaitForSeconds seconds=new WaitForSeconds(0.2f);
            int count=text.Length;
            stringBuilder.Clear();
            for(int i=0;i<count;i++)
            {
                stringBuilder.Append(text[i]);
                mainText.text=stringBuilder.ToString();
                yield return seconds;
            }
            callBack?.Invoke();
        }
        IEnumerator LateEnd()
        {
            yield return new WaitForSeconds(1.5f);
            dialogueSystem.DialogueOver();
        }
        
        void GenerateButton(DialoguePiece piece)
        {
            if(piece.options.Count==0){
                StartCoroutine(LateEnd());
                return;
            }
            CreateOptions(piece);
        }
        const string DialogueOption="DialogueOption";
        void CreateOptions(DialoguePiece piece)
        {
            for(int i=0;i<piece.options.Count;i++)
            {
                OptionUI option=GetOption();
                optionSlots.Add(option);
                option.UpdateOption(dialogueSystem,piece.options[i].text,piece,piece.options[i]);
                option.transform.position=slotPoints[i].position;
            }
        }
        private OptionUI GetOption()
        {
            return(Instantiate(optionPrefab,optionPanel));
        }
    }
}
