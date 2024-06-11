using Services.pkgCRUDs;
using System;
using System.Collections.Generic;
namespace pkgWallet.pkgDomain
{
    public class clsDashBoard
    {
        #region Attributes
        private static clsDashBoard attInstance;
        public List<clsWallet> attMyWallets;
        public List<clsCurrency> attMyCurrencies;
        #endregion
        #region Operations
        #region Getters
        public static clsDashBoard opGetInstance()
        {
            if (attInstance == null) { attInstance = new clsDashBoard(); }
            return attInstance;
        }
        public clsWallet opGetWalletWith(int prmOUID)
        {
            return clsBrokerCrud<clsWallet, List<clsWallet>>.opGetItemWith(prmOUID, attMyWallets);
        }
        public clsCurrency opGetCurrencyWith(int prmOUID)
        {
            return clsBrokerCrud<clsCurrency, List<clsCurrency>>.opGetItemWith(prmOUID, attMyCurrencies);
        }
        public List<clsCurrency> opGetCurrencies()
        {
            return attMyCurrencies;
        }
        public List<clsWallet> opGetWallets()
        {
            if (attMyWallets == null)
                attMyWallets = new List<clsWallet>();
            return attMyWallets;
        }
        #endregion
        #region CRUDs
        #region WriteDown
        public bool opWriteDownWallet(int prmOUID, string prmName, string prmClientName, string prmClientEmail)
        {
            return clsBrokerCrud<clsWallet, List<clsWallet>>.opWriteDownItem
                (prmOUID, new clsWallet(prmOUID, prmName, prmClientName, prmClientEmail), attMyWallets);
        }
        public bool opWriteDownPocket(int prmOUIDWallet, int prmOUID, string prmName, double prmAEInterest, bool prmIsMain)
        {
            try
            {
                return opGetWalletWith(prmOUIDWallet).opWriteDownPocket(prmOUID, prmName, prmAEInterest, prmIsMain);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool opWriteDownCurrency(int prmOUID, string prmName, double prmRMR)
        {
            return clsBrokerCrud<clsCurrency, List<clsCurrency>>.opWriteDownItem
                (prmOUID, new clsCurrency(prmOUID, prmName, prmRMR), attMyCurrencies);
        }
        public bool opWriteDownMovement(int prmOUIDWallet, int prmOUIDPocket, int prmOUID, string prmName, double prmAmount, string prmDate)
        {
            try
            {
                return opGetWalletWith(prmOUIDWallet).opWriteDownMovement(prmOUIDPocket, prmOUID, prmName, prmAmount, prmDate);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Update
        public bool opUpdateWallet(int prmOUID, string prmName, string prmClientName, string prmClientEmail)
        {
            try
            {
                return opGetWalletWith(prmOUID).opModify(prmName, prmClientName, prmClientEmail);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool opUpdatePocket(int prmOUIDWallet, int prmOUID, string prmName, double prmAEInterest, bool prmIsMain)
        {
            try
            {
                return opGetWalletWith(prmOUIDWallet).opUpdatePocket(prmOUID, prmName, prmAEInterest, prmIsMain);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool opUpdateCurrency(int prmOUID, string prmName, double prmRMR)
        {
            try
            {
                return opGetCurrencyWith(prmOUID).opModify(prmName, prmRMR);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Delete
        public bool opDeleteWallet(int prmOUID)
        {
            return clsBrokerCrud<clsWallet, List<clsWallet>>.opDeleteItem(prmOUID, attMyWallets);
        }
        public bool opDeletePocket(int prmOUIDWallet, int prmOUID)
        {
            try
            {
                return opGetWalletWith(prmOUIDWallet).opDeletePocket(prmOUID);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool opDeleteCurrency(int prmOUID)
        {
            return clsBrokerCrud<clsCurrency, List<clsCurrency>>.opDeleteItem(prmOUID, attMyCurrencies);
        }
        public bool opDeleteMovement(int prmOUIDWallet, int prmOUIDPocket, int prmOUID)
        {
            throw new NotImplementedException();
        }
        public bool opClearMe()
        {
            attMyWallets=new List<clsWallet>();
            attMyCurrencies=new List<clsCurrency>();
            return true;
        }
        #endregion
        #endregion
        #region OperationsWallet
        public bool opWithDrawal(int prmOUIDWallet, int prmOUIDPocket, int prmOUIDMovement, double prmAmount, string prmDate)
        {
            if(opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmOUIDPocket) != null)
            {
                if (opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmOUIDPocket).opGetBalance()<prmAmount)
                {
                    return false;
                }
                if(opWriteDownMovement(prmOUIDWallet, prmOUIDPocket, prmOUIDMovement, "WithDrawal", prmAmount, prmDate))
                {
                    opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmOUIDPocket).opAddBalance(-prmAmount);
                    return true;
                }
            }
            return false;
        }
        public bool opIncome(int prmOUIDWallet, int prmOUIDPocket, int prmOUIDMovement, double prmAmount, string prmDate)
        {
            if (opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmOUIDPocket) != null)
            {
                if (opWriteDownMovement(prmOUIDWallet, prmOUIDPocket, prmOUIDMovement, "Income", prmAmount, prmDate))
                {
                    opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmOUIDPocket).opAddBalance(prmAmount);
                    return true;
                }
            }
            return false;
        }
        public bool opTransfer(int prmOUIDWallet, int prmOUIDPocket, int prmOUIDMovement, int prmOUIDWalletTransfer, int prmOUIDPocketTransfer, double prmAmount, string prmDate)
        {
            if (opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmOUIDPocket) != null && opGetWalletWith(prmOUIDWalletTransfer).opGetPocketWith(prmOUIDPocketTransfer) != null)
            {
                if (opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmOUIDPocket).opGetBalance() < prmAmount)
                {
                    return false;
                }
                if (opWriteDownMovement(prmOUIDWallet, prmOUIDPocket, prmOUIDMovement, "Transfer", prmAmount, prmDate))
                {
                    opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmOUIDPocket).opAddBalance(-prmAmount);
                    opGetWalletWith(prmOUIDWalletTransfer).opGetPocketWith(prmOUIDPocketTransfer).opAddBalance(prmAmount);
                    return true;
                }
            }
            return false;
        }
        public bool ConversionFromCOPToUSD(int prmOUIDWallet, int prmPocketCOP, int prmPocketUSD, double prmAmount)
        {
            if (opGetWalletWith(prmOUIDWallet)!=null && opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmPocketCOP)!=null && opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmPocketUSD) != null)
            {
                if (opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmPocketCOP).opGetBalance()>=prmAmount)
                {
                    opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmPocketCOP).opAddBalance(-prmAmount);
                    opGetWalletWith(prmOUIDWallet).opGetPocketWith(prmPocketUSD).opAddBalance(prmAmount/3900);
                    return true;
                }
            }
            return false;
        }
        #endregion
        #endregion
    }
}