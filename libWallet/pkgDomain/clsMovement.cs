using System;

namespace pkgWallet.pkgDomain
{
    public class clsMovement : clsEntity
    {
        #region Attributes
        private string attDate;
        private double attAmount;
        public clsPocket myPocket;
        #endregion
        #region Operations
        #region Constructor
        public clsMovement(int prmOUID, string prmName, double prmAmount, string prmDate) : base(prmOUID, prmName)
        {
            this.attAmount = prmAmount;
            this.attDate = prmDate;
        }
        #endregion
        #region Getters
        public double opGetAmount()
        {
            return this.attAmount;
        }

        public string opGetDate()
        {
            return this.attDate;
        }
        #endregion
        #region Destroyer
        public override bool opDie()
        {
            this.attOUID = -1;
            this.attName = "";
            this.attAmount = -1;
            this.attDate = "";
            return true;
        }
        #endregion 
        #endregion
    }
}