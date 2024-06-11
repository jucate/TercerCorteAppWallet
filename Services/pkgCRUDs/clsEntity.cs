using pkgWallet.pkgDomain.pkgInterfaces;
using System;
namespace pkgWallet.pkgDomain
{
    public class clsEntity : iOUID, IComparable<clsEntity>
    {
        #region Attributes
        #region Own
        protected int attOUID;
        protected string attName;
        protected bool attResult;
        #endregion
        #endregion
        #region Operations
        #region Constructor
        public clsEntity(int prmOUID, string prmName)
        {
            this.attOUID = prmOUID;
            this.attName = prmName;
        }
        #endregion
        #region Getters
        public int opGetOUID()
        {
            return attOUID;
        }
        public string opGetName()
        {
            return attName;
        }
        #endregion
        #region Setters
        public bool opSetName(string prmName)
        {
            this.attName = prmName;
            return true;
        }
        #endregion
        #region Destroyer
        public virtual bool opDie()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region CompareTo
        public int CompareTo(clsEntity prmOther)
        {
            if (attOUID == prmOther.attOUID && attName == prmOther.attName)
                return 0;
            if (attOUID < prmOther.attOUID)
                return -1;
            return 1;
        } 
        #endregion
        #endregion
    }
}
