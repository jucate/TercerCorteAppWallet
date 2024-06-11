using System;
using Services.pkgCRUDs;
namespace pkgWallet.pkgDomain
{
    public class clsCurrency : clsEntity
    {
        #region Attributes
        private double attRMR;
        private bool attMainCurrency = false;
        public System.Collections.Generic.List<clsPocket> attMyPockets
        {
            get
            {
                if (attMyPockets == null)
                    attMyPockets = new System.Collections.Generic.List<clsPocket>();
                return attMyPockets;
            }
            set
            {
                attMyPockets = value;
            }
        }
        #endregion
        #region Operations
        #region Constructor
        public clsCurrency(int prmOUID, string prmName, double prmRMR) : base(prmOUID, prmName)
        {
            this.attRMR = prmRMR;
        }
        #endregion
        #region Getters
        public double opGetRMR()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Query
        public bool opIsMainCurrency()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Setters
        public bool opSetRMR(double prmValue)
        {
            this.attRMR = prmValue;
            return true;
        }

        public bool opSetMain(bool prmValue)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Destroyer
        public bool opModify(string prmName, double prmRMR)
        {
            if(base.opSetName(prmName) && opSetRMR(prmRMR))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Destroyer
        public override bool opDie()
        {
            this.attOUID = -1;
            this.attName = "";
            this.attRMR = -1;
            this.attMainCurrency = false;
            this.attMyPockets = null;
            return true;
        }
        #endregion
        #region CompareTo
        public int CompareTo(clsCurrency prmOther)
        {
            if (attMainCurrency == prmOther.attMainCurrency && base.CompareTo(prmOther) == 0 && attRMR == prmOther.attRMR)
            {
                return 0;
            }
            return base.CompareTo(prmOther);
        } 
        #endregion
        #endregion
    }
}