using Services.pkgCRUDs;
using System;
using System.Collections.Generic;

namespace pkgWallet.pkgDomain
{
    public class clsWallet : clsEntity
    {
        #region Attributes
        private bool attTaxExemption = false;
        private double attTaxExemptionMaxValue = 2500000;
        private double attTax = 0.0004;
        private string attClientName;
        private string attClientEmail;
        private string attClientBank;
        private int attClientBankAccountNumber;
        public List<clsPocket> attMyPockets = new List<clsPocket>();
        #endregion
        #region Operations
        #region Constructor
        public clsWallet(int prmOUID, string prmName, string prmClientFullName, string prmClientEmail) : base(prmOUID, prmName)
        {
            this.attClientName = prmClientFullName;
            this.attClientEmail = prmClientEmail;
        }
        #endregion
        #region Getters
        public bool opItsExempt()
        {
            return this.attTaxExemption;
        }
        public double opGetMaxValueExemptionTax()
        {
            return this.attTaxExemptionMaxValue;
        }
        public double opGetTax()
        {
            return this.attTax;
        }
        public string opGetClientName()
        {
            return this.attClientName;
        }
        public string opGetClientEmail()
        {
            return this.attClientEmail;
        }
        public string opGetClientBank()
        {
            return this.attClientBank;
        }
        public int opGetClientBankAccount()
        {
            return this.attClientBankAccountNumber;
        }
        public clsPocket opGetPocketWith(int prmOUID)
        {
            return clsBrokerCrud<clsPocket, List<clsPocket>>.opGetItemWith(prmOUID, attMyPockets);
        }
        public List<clsPocket> opGetPockets()
        {
            return attMyPockets;
        }
        #endregion
        #region Setters
        public bool opMakeExempt()
        {
            this.attTaxExemption = true;
            return true;
        }
        public bool opSetClientBank(string prmValue)
        {
            this.attClientBank = prmValue;
            return true;
        }
        public bool opSetClientBankAccount(string prmValue)
        {
            this.attClientBankAccountNumber = Convert.ToInt32(prmValue);
            return true;
        }
        public bool opSetClientEmail(string prmValue)
        {
            this.attClientEmail = prmValue;
            return true;
        }
        public bool opSetClientName(string prmValue)
        {
            this.attClientName = prmValue;
            return true;
        }
        #endregion
        #region CRUDs
        public bool opWriteDownPocket(int prmOUID, string prmName, double prmAEInterest, bool prmIsMain)
        {
            if(attMyPockets.Count >= 3)
            {
                return false;
            }
            return clsBrokerCrud<clsPocket, List<clsPocket>>.opWriteDownItem
                (prmOUID, new clsPocket(prmOUID, prmName, prmAEInterest, prmIsMain), attMyPockets);
        }
        public bool opUpdatePocket(int prmOUID, string prmName, double prmAEInterest, bool prmIsMain)
        {
            try
            {
                return opGetPocketWith(prmOUID).opModify(prmName, prmAEInterest, prmIsMain);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool opDeletePocket(int prmOUID)
        {
            return clsBrokerCrud<clsPocket, List<clsPocket>>.opDeleteItem(prmOUID, attMyPockets);
        }
        public bool opWriteDownMovement(int prmOUIDPocket, int prmOUID, string prmName, double prmAmount, string prmDate)
        {
            try
            {
                return opGetPocketWith(prmOUIDPocket).opWriteDownMovement(prmOUID, prmName, prmAmount, prmDate);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool opDeleteMovement(int prmOUIDPocket, int prmOUID)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Modifier
        public bool opModify(string prmName, string prmClientName, string prmClientEmail)
        {
            clsWallet varMemento = new clsWallet(attOUID, attName, attClientName, attClientEmail);
            if (base.opSetName(prmName) && opSetClientName(prmClientName) && opSetClientEmail(prmClientEmail))
            {
                return true;
            }
            attName = varMemento.attName;
            attClientName = varMemento.attClientName;
            attClientEmail = varMemento.attClientEmail;
            return false;
        }
        #endregion
        #region Destroyer
        public override bool opDie()
        {
            this.attOUID = -1;
            this.attName = "";
            this.attClientName = "";
            this.attClientEmail = "";
            this.attClientBank = "";
            this.attClientBankAccountNumber = -1;
            this.attMyPockets = null;
            return true;
        }
        #endregion
        #region CompareTo
        public int CompareTo(clsWallet prmOther)
        {
            if (attTaxExemption == prmOther.attTaxExemption && attClientName == prmOther.attClientName && attClientEmail == prmOther.attClientEmail)
            {
                return 0;
            }
            return base.CompareTo(prmOther);
        }
        #endregion
        #endregion
    }
}