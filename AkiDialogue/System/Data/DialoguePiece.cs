using System.Collections.Generic;
using Kurisu.AkiDialogue.Utility;
using UnityEngine;
namespace Kurisu.AkiDialogue
{
/// <summary>
/// 对话片段
/// </summary>
[System.Serializable]
public class DialoguePiece 
{
   const string defaultValue="00";
   public string ID=defaultValue;
   [Multiline(3)]
   public string text;
   public List<DialogueOption>options=new List<DialogueOption>();
   public DialoguePiece Reset() {
      ID=defaultValue;
      text=string.Empty;
      foreach(var option in options)
      {
         option.ObjectPushPool();
      }
      options.Clear();
      return this;
   }
   public DialoguePiece Clone()
   {
      var piece=DialogueGenerator.CreatePiece();
      piece.ID=ID;
      piece.text=text;
      foreach(var option in options)
      {
         piece.options.Add(option.Clone());
      }
      return piece;
   }
}
}