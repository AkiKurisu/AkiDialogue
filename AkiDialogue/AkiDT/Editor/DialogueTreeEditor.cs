using UnityEditor;
using Kurisu.AkiBT.Editor;
using UnityEngine;
using UnityEngine.UIElements;
using Kurisu.AkiBT;
using UnityEditor.UIElements;
using System.Reflection;
using System.Linq;
namespace Kurisu.AkiDT.Editor
{
[CustomEditor(typeof(DialogueTree))]
public class DialogueTreeEditor : UnityEditor.Editor
{
    const string labelText="AkiDT 对话树 Version1.0";
    protected virtual string LabelText=>labelText;
    const string buttonText="打开对话树";
    protected virtual string ButtonText=>buttonText;
    protected VisualElement myInspector;
    private FieldResolverFactory factory=new FieldResolverFactory();
    public override VisualElement CreateInspectorGUI()
    {
        myInspector = new VisualElement();
        var bt = target as DialogueTree;
        var label=new Label(LabelText);
        label.style.fontSize=20;
        myInspector.Add(label);
        myInspector.styleSheets.Add((StyleSheet)Resources.Load("AkiBT/Inspector", typeof(StyleSheet)));
        var field=new PropertyField(serializedObject.FindProperty("externalDialogueTree"),"外部对话树");
        myInspector.Add(field);
        var toggle=new Toggle("运行时使用外部对话树");
        toggle.value=bt.UseSOData;
        toggle.RegisterValueChangedCallback((x)=>bt.UseSOData=x.newValue);
        myInspector.Add(toggle);
        var foldout=DialogueEditorUtility.DrawSharedVariable(bt,factory,target,this);
        if(foldout!=null)myInspector.Add(foldout);
        if(!Application.isPlaying)
        {
            var button=DialogueEditorUtility.GetButton(()=>DialogueEditorWindow.Show(bt));
            button.style.backgroundColor=new StyleColor(new Color(140/255f, 160/255f, 250/255f));
            button.text=ButtonText;
            myInspector.Add(button); 
        }      
        if(Application.isPlaying&&bt is DialogueTree)
        {
            var playButton=DialogueEditorUtility.GetButton(()=>(bt as DialogueTree).PlayDialogue()); 
            playButton.style.backgroundColor=new StyleColor(new Color(140/255f, 160/255f, 250/255f));
            playButton.text="播放对话"; 
            myInspector.Add(playButton);
        }
        return myInspector;
    }     
    }
    [CustomEditor(typeof(DialogueTreeSO))]
    public class DialogueTreeSOEditor : UnityEditor.Editor
    {
        const string labelText="AkiDT 对话树SO";
        protected virtual string LabelText=>labelText;
        const string buttonText="打开对话树SO";
        protected virtual string ButtonText=>buttonText;
        protected VisualElement myInspector;
        private FieldResolverFactory factory=new FieldResolverFactory();
        public override VisualElement CreateInspectorGUI()
        {
            myInspector = new VisualElement();
            var bt = target as IBehaviorTree;
            var label=new Label(LabelText);
            label.style.fontSize=20;
            myInspector.Add(label);
            myInspector.styleSheets.Add((StyleSheet)Resources.Load("AkiBT/Inspector", typeof(StyleSheet)));
            var field=new PropertyField(serializedObject.FindProperty("externalDialogueTree"),"外部对话树");
            myInspector.Add(field);
            myInspector.Add(new Label("对话树描述"));
            var description=new PropertyField(serializedObject.FindProperty("Description"),string.Empty);
            myInspector.Add(description);
            var foldout=DialogueEditorUtility.DrawSharedVariable(bt,factory,target,this);
            if(foldout!=null)myInspector.Add(foldout);
            if(!Application.isPlaying)
            {
                var button=DialogueEditorUtility.GetButton(()=>DialogueEditorWindow.Show(bt));
                button.style.backgroundColor=new StyleColor(new Color(140/255f, 160/255f, 250/255f));
                button.text=ButtonText;
                myInspector.Add(button); 
            }      
            return myInspector;
        }
}
internal class DialogueEditorUtility
{
    internal static Button GetButton(System.Action clickEvent)
    {
        var button=new Button(clickEvent);
        button.style.fontSize=15;
        button.style.unityFontStyleAndWeight=FontStyle.Bold;
        button.style.color=Color.white;
        return button;
    }
    internal static Foldout DrawSharedVariable(IBehaviorTree bt,FieldResolverFactory factory,Object target,UnityEditor.Editor editor)
    {
        int count=bt.SharedVariables.Count(x=>x is not PieceID);
        if(count==0)return null;   
        var foldout=new Foldout();
        foldout.value=false;
        foldout.text="SharedVariables";
        foreach(var variable in bt.SharedVariables)
        { 
            if(variable is PieceID)continue;
            var grid=new Foldout();
            grid.text=$"{variable.GetType().Name}  :  {variable.Name}";
            grid.value=false;
            var content=new VisualElement();
            content.style.flexDirection=FlexDirection.Row;
            var valueField=factory.Create(variable.GetType().GetField("value",BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Public)).GetEditorField(bt.SharedVariables,variable);
            content.Add(valueField);
            var deleteButton=new Button(()=>{ 
                bt.SharedVariables.Remove(variable);
                foldout.Remove(grid);
                EditorUtility.SetDirty(target);
                EditorUtility.SetDirty(editor);
                AssetDatabase.SaveAssets();
            });
            deleteButton.text="Delate";
            deleteButton.style.width=50;
            content.Add(deleteButton);
            grid.Add(content);
            foldout.Add(grid);   
        }
        return foldout;
    }
}
}