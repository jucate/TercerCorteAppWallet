// File:    clsPocket.cs
// Author:  USUARIO
// Created: lunes, 19 de febrero de 2024 4:13:56 p. m.
using Services.pkgCRUDs;
using System;
using System.Collections.Generic;

namespace pkgWallet.pkgDomain
{
    public class clsPocket : clsEntity
    {
        #region Attributes
        private double attAEInterest = 0;
        private double attTotalBalance = 0;
        private bool attIsMain = false;
        public clsWallet myWallet;
        public clsCurrency myCurrency;
        public System.Collections.Generic.List<clsMovement> attMyMovements = new List<clsMovement>();
        #endregion
        #region Operations
        #region Constructor
        public clsPocket(int prmOUID, string prmName, double prmAEInterest, bool prmMain) : base(prmOUID, prmName)
        {
            this.attAEInterest = prmAEInterest;
        }
        #endregion
        #region Getters
        public double opGetAEInterest()
        {
            return attAEInterest;
        }
        public double opGetBalance()
        {
            return attTotalBalance;
        }
        public bool opIsMainPocket()
        {
            return attIsMain;
        }
        public clsMovement opGetMovementWith(int prmOUID)
        {
            return clsBrokerCrud<clsMovement, List<clsMovement>>.opGetItemWith(prmOUID, attMyMovements);
        }
        #endregion
        #region Setters
        public bool opMakeMainPocket(bool prmValue)
        {
            this.attIsMain = prmValue;
            return true;
        }
        public bool opSetAInterest(double prmValue)
        {
            this.attAEInterest = prmValue;
            return true;
        }
        public bool opAddBalance(double prmValue)
        {
            this.attTotalBalance = this.attTotalBalance + prmValue;
            return true;
        }
        #endregion
        #region Modifiers
        public bool opModify(string prmName, double prmAEInterest, bool prmIsMain)
        {
            clsPocket varMemento = new clsPocket(attOUID, attName, attAEInterest, prmIsMain);
            if (base.opSetName(prmName) && opSetAInterest(prmAEInterest) && opMakeMainPocket(prmIsMain))
            {
                return true;
            }
            attName = varMemento.attName;
            attAEInterest = varMemento.attAEInterest;
            attIsMain = varMemento.attIsMain;
            return false;
        }
        #endregion
        #region Destroyer
        public override bool opDie()
        {
            this.attOUID = -1;
            this.attName = "";
            this.attAEInterest = 0;
            this.attTotalBalance = 0;
            this.attIsMain = false;
            return true;
        }
        #endregion
        #region CRUDs
        public bool opWriteDownMovement(int prmOUID, string prmName, double prmAmount, string prmDate)
        {
            return clsBrokerCrud<clsMovement, List<clsMovement>>.opWriteDownItem
                (prmOUID, new clsMovement(prmOUID, prmName, prmAmount, prmDate), attMyMovements);
        }

        public bool opDeleteMovement(int prmOUID)
        {
            return clsBrokerCrud<clsMovement, List<clsMovement>>.opDeleteItem(prmOUID, attMyMovements);
        }
        #endregion
        #endregion
    }
}