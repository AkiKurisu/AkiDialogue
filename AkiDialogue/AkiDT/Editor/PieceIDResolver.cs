using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kurisu.AkiBT;
using Kurisu.AkiBT.Editor;
using UnityEngine.UIElements;
namespace Kurisu.AkiDT.Editor
{
    public class PieceIDResolver :FieldResolver<PieceIDField,PieceID>
    {
        public PieceIDResolver(FieldInfo fieldInfo) : base(fieldInfo)
        {
        }
        protected override void SetTree(BehaviorTreeView ownerTreeView)
        {
           editorField.InitField(ownerTreeView);
        }
        private PieceIDField editorField;
        protected override PieceIDField CreateEditorField(FieldInfo fieldInfo)
        {
            bool isReferenced=fieldInfo.GetCustomAttribute<Reference>()!=null;
            editorField = new PieceIDField(fieldInfo.Name,null,isReferenced);
            return editorField;
        }
        public static bool IsAcceptable(Type infoType,FieldInfo info)=>infoType ==typeof(PieceID) ;
         
    }
    public class PieceIDField : BaseField<PieceID>
    {
        private BehaviorTreeView treeView;
        private DropdownField nameDropdown;
        private SharedVariable bindExposedProperty;
        private readonly bool isReferenced;
        public PieceIDField(string label, VisualElement visualInput,bool isReferenced) : base(label, visualInput)
        {
            AddToClassList("SharedVariableField");
            this.isReferenced=isReferenced;
        }
        internal void InitField(BehaviorTreeView treeView)
        {
            this.treeView=treeView;
            treeView.OnPropertyNameEditingEvent+=(variable)=>
            {
                if(variable!=bindExposedProperty)return;
                UpdateID(variable);
            };
            BindProperty();
            if(bindExposedProperty==null&&!treeView.IsRestored&&!isReferenced)
            {
                bindExposedProperty=new PieceID();
                treeView.AddPropertyToBlackBoard(bindExposedProperty);
                value.Name=bindExposedProperty.Name;
            }  
            UpdateValueField();
        } 
        private void UpdateID(SharedVariable variable)
        {
            if(isReferenced)
            {
                nameDropdown.value=variable.Name;
            }
            else label=variable.Name;
            value.Name=variable.Name;
        }
        private static List<string> GetList(BehaviorTreeView treeView)
        {
            return treeView.ExposedProperties
            .Where(x=>x.GetType()==typeof(PieceID))
            .Select(v => v.Name)
            .ToList();
        }
        private void BindProperty()
        {
            bindExposedProperty=treeView.ExposedProperties.Where(x=>x.GetType()==typeof(PieceID)&&x.Name.Equals(value.Name)).FirstOrDefault();
        }
        private void UpdateValueField()
        {
            if(isReferenced)
            {
                if(nameDropdown==null)AddDropDown();
                else nameDropdown.value=value.Name;
            }
            else
            {
                label=value.Name;
            }
        }
        private void AddDropDown(){
            nameDropdown=new DropdownField("Piece ID",GetList(treeView),value.Name??string.Empty);
            nameDropdown.RegisterCallback<MouseEnterEvent>((evt)=>{nameDropdown.choices=GetList(treeView);});
            nameDropdown.RegisterValueChangedCallback(evt => {value.Name = evt.newValue;BindProperty();});
            Add(nameDropdown);
        }
        public sealed override PieceID value {get=>base.value; set {
            if(value!=null)
            {
                base.value=value.Clone() as PieceID;
                BindProperty();
                UpdateValueField();
            }
            else
            {
                base.value=new PieceID();
            }
        } 
        }
    }
}