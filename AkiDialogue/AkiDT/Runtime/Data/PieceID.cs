using Kurisu.AkiBT;
namespace Kurisu.AkiDT
{
    [System.Serializable]
    public class PieceID:SharedVariable<string>
    {
        public PieceID()
        {
            IsShared=true;
        }
        public override object Clone()
        {
            return new PieceID(){Value=this.value,Name=this.Name,IsShared=this.IsShared};
        }
    }
}
