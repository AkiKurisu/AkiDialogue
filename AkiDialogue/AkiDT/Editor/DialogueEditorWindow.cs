using Kurisu.AkiBT.Editor;
using Kurisu.AkiBT;
using System;
using UnityEditor;
using UnityEngine;
namespace Kurisu.AkiDT.Editor
{
public class DialogueEditorWindow : GraphEditorWindow
{
        protected override sealed string TreeName=>"对话树";
        protected override sealed string InfoText=>"欢迎使用AkiDT,一个为对话系统定制的行为树!";  
        protected override sealed Type SOType=>typeof(DialogueTreeSO);
        [MenuItem("Tools/AkiDT Editor")]
        private static void ShowEditorWindow()
        {
            var treeSO=ScriptableObject.CreateInstance<DialogueTreeSO>();
            AssetDatabase.CreateAsset(treeSO,"Assets/DialogueTreeSO.asset");
            AssetDatabase.SaveAssets();
            Show(treeSO);
        }
        public static new void Show(IBehaviorTree bt)
        {
            var window = Create<DialogueEditorWindow>(bt);
            window.Show();
            window.Focus();
        } 
        protected override BehaviorTreeView CreateView(IBehaviorTree behaviorTree)
        {
            return new DialogueTreeView(behaviorTree, this);
        }
}
}