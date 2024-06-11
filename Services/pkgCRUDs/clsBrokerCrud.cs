using pkgWallet.pkgDomain.pkgInterfaces;
using System;
using System.Collections.Generic;

namespace Services.pkgCRUDs
{
    public static class clsBrokerCrud<T, List> where T : class, iOUID
    {
        public static T opGetItemWith(int prmOUID, List<T> prmColletion)
        {
            foreach (T varObj in prmColletion)
            {
                if (varObj.opGetOUID() == prmOUID)
                {
                    return varObj;
                }
            }
            return null;
        }

        public static bool opWriteDownItem(int prmOUID,T prmObj, List<T> prmColletion)
        {
            if (prmColletion == null)
            {
                prmColletion = new List<T>();
            }
            if (opGetItemWith(prmOUID, prmColletion) != null)
            {
                return false;
            }       
            prmColletion.Add(prmObj);
            return true;
        }

        public static bool opDeleteItem(int prmOUID, List<T> prmColletion)
        {
            T varObj = opGetItemWith(prmOUID, prmColletion);
            if (varObj == null) { return false; }
            if (varObj.opDie() == false) { return false; }
            return prmColletion.Remove(varObj);
        }
    }
}
