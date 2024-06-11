using pkgWallet.pkgDomain;
namespace uTestWallet
{
    [TestClass]
    public class uTestDashboard
    {
        private clsDashBoard testMyApp = clsDashBoard.opGetInstance();
        private clsCurrency testExpectedObjCurrency;
        private clsWallet testExpectedObjWallet;
        private clsPocket testExpectedObjPocket;
        private clsMovement testExpectedObjMovement;

        [TestMethod]
        public void testWriteDownCurrency()
        {
            #region Setup
            testMyApp.opClearMe();
            testExpectedObjCurrency = new clsCurrency(0, "COP", 3830);
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opWriteDownCurrency(0, "COP", 3830));
            Assert.AreEqual(1, testMyApp.opGetCurrencies().Count);
            Assert.AreEqual(0, testExpectedObjCurrency.CompareTo(testMyApp.opGetCurrencies()[0]));
            #endregion
        }
        [TestMethod]
        public void testWriteDownWallet()
        {
            #region Setup
            testMyApp.opClearMe();
            testExpectedObjWallet = new clsWallet(0, "milly", "george", "george@gmail.com");
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opWriteDownWallet(0, "milly", "george", "george@gmail.com"));
            Assert.AreEqual(1, testMyApp.opGetWallets().Count);
            Assert.AreEqual(0, testExpectedObjWallet.CompareTo(testMyApp.opGetWallets()[0]));
            #endregion
        }
        [TestMethod]
        public void testWriteDownPocket()
        {
            #region Setup
            testMyApp.opClearMe();
            testExpectedObjPocket = new clsPocket(0, "Bob's", 13.5, true);
            testMyApp.opGetWallets().Add(new clsWallet(0, "milly", "George", "george@gmail.com"));
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opWriteDownPocket(0, 0, "Bob's", 13.5, true));
            Assert.AreEqual(1, testMyApp.opGetWallets()[0].opGetPockets().Count);
            Assert.AreEqual(0, testExpectedObjPocket.CompareTo(testMyApp.opGetWallets()[0].opGetPockets()[0]));
            #endregion
        }
        [TestMethod]
        public void testWriteDownMovement()
        {
            #region Setup
            testMyApp.opClearMe();
            testExpectedObjMovement = new clsMovement(0, "Retiro", 1000, "09/06/2024");
            testExpectedObjMovement.myPocket = new clsPocket(0, "Bob's", 13.5, true); 
            testMyApp.opGetCurrencies().Add(new clsCurrency(0, "COP", 3830));
            testMyApp.opGetWallets().Add(new clsWallet(0, "milly", "George", "george@gmail.com"));
            testMyApp.opGetWallets()[0].opGetPockets().Add(new clsPocket(0, "Bob's", 13.5, true));
            testMyApp.opGetWallets()[0].opGetPockets()[0].myCurrency = testMyApp.opGetCurrencies()[0];
                        
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opWriteDownMovement(0, 0, 0, "Retiro", 1000, "09/06/2024"));
            Assert.AreEqual(1, testMyApp.opGetWallets()[0].opGetPockets()[0].attMyMovements.Count);
            Assert.AreEqual(0, testExpectedObjMovement.CompareTo(testMyApp.opGetWallets()[0].opGetPockets()[0].attMyMovements[0]));
            #endregion
        }
        [TestMethod]
        public void testUpdateCurrency()
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetCurrencies().Add(new clsCurrency(0, "COP", 3830));
            testExpectedObjCurrency = new clsCurrency(0, "USD", 1);
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opUpdateCurrency(0, "USD", 1));
            Assert.AreEqual(1, testMyApp.opGetCurrencies().Count);
            Assert.AreEqual(0, testExpectedObjCurrency.CompareTo(testMyApp.opGetCurrencies()[0]));
            #endregion
        }
        [TestMethod]
        public void testUpdateWallet()
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetWallets().Add(new clsWallet(0, "milly", "george", "george@gmail.com"));
            testExpectedObjWallet = new clsWallet(0, "milly", "charles", "charles@gmail.com");
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opUpdateWallet(0, "milly", "charles", "charles@gmail.com"));
            Assert.AreEqual(1, testMyApp.opGetWallets().Count);
            Assert.AreEqual(0, testExpectedObjWallet.CompareTo(testMyApp.opGetWallets()[0]));
            #endregion
        }
        [TestMethod]
        public void testUpdatePocket() 
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetWallets().Add(new clsWallet(0, "milly", "george", "george@gmail.com"));
            testMyApp.opGetCurrencies().Add(new clsCurrency(0, "COP", 3830));
            testExpectedObjPocket = new clsPocket(0, "JuanPocket", 13.5, true);
            testMyApp.opGetWallets()[0].opGetPockets().Add(new clsPocket(0, "Bob's", 13.5, true));
            testMyApp.opGetWallets()[0].opGetPockets()[0].myCurrency = testMyApp.opGetCurrencies()[0];
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opUpdatePocket(0, 0, "JuanPocket", 13.5, true));
            Assert.AreEqual(1, testMyApp.opGetWallets()[0].opGetPockets().Count);
            Assert.AreEqual(0, testExpectedObjPocket.CompareTo(testMyApp.opGetWallets()[0].opGetPockets()[0]));
            #endregion
        }
        [TestMethod]
        public void testDeleteWallet()
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetWallets().Add(new clsWallet(0, "georgeWallet", "georgi", "george@unicauca.edu.co"));
            testExpectedObjWallet = null;
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opDeleteWallet(0));
            Assert.AreEqual(0, testMyApp.opGetWallets().Count);
            Assert.AreEqual(testExpectedObjWallet, testMyApp.opGetWalletWith(0));
            #endregion
        }
        [TestMethod]
        public void testDeletePocket()
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetWallets().Add(new clsWallet(0, "georgeWallet", "georgi", "george@unicauca.edu.co"));
            testMyApp.opGetWallets()[0].opGetPockets().Add(new clsPocket(0, "georgePocket", 13.5, true));
            testMyApp.opGetCurrencies().Add(new clsCurrency(0, "COP", 3830));
            testMyApp.opGetWallets()[0].opGetPockets()[0].myCurrency = testMyApp.opGetCurrencies()[0];
            testExpectedObjPocket = null;
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opDeletePocket(0, 0));
            Assert.AreEqual(0, testMyApp.opGetWallets()[0].opGetPockets().Count);
            Assert.AreEqual(testExpectedObjPocket, testMyApp.opGetWalletWith(0).opGetPocketWith(0));
            #endregion
        }
        [TestMethod]
        public void testWithDrawal()
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetWallets().Add(new clsWallet(0, "georgeWallet", "georgi", "george@unicauca.edu.co"));
            testMyApp.opGetWallets()[0].opGetPockets().Add(new clsPocket(0, "georgePocket", 13.5, true));
            testMyApp.opGetCurrencies().Add(new clsCurrency(0, "COP", 3830));
            testMyApp.opGetWallets()[0].opGetPockets()[0].myCurrency = testMyApp.opGetCurrencies()[0];
            testMyApp.opGetWallets()[0].opGetPocketWith(0).opAddBalance(20000);
            testExpectedObjMovement = new clsMovement(0, "WithDrawal", 1000, "09/06/2024");
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opWithDrawal(0, 0, 0, 1000, "09/06/2024"));
            Assert.AreEqual(0, testExpectedObjMovement.CompareTo(testMyApp.opGetWallets()[0].opGetPocketWith(0).opGetMovementWith(0)));
            Assert.AreEqual(19000, testMyApp.opGetWallets()[0].opGetPocketWith(0).opGetBalance());
            #endregion
        }
        [TestMethod]
        public void testIncome()
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetWallets().Add(new clsWallet(0, "georgeWallet", "georgi", "george@unicauca.edu.co"));
            testMyApp.opGetWallets()[0].opGetPockets().Add(new clsPocket(0, "georgePocket", 13.5, true));
            testMyApp.opGetCurrencies().Add(new clsCurrency(0, "COP", 3830));
            testMyApp.opGetWallets()[0].opGetPockets()[0].myCurrency = testMyApp.opGetCurrencies()[0];
            testMyApp.opGetWallets()[0].opGetPocketWith(0).opAddBalance(20000);
            testExpectedObjMovement = new clsMovement(0, "Income", 1000, "09/06/2024");
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opIncome(0, 0, 0, 1000, "09/06/2024"));
            Assert.AreEqual(0, testExpectedObjMovement.CompareTo(testMyApp.opGetWallets()[0].opGetPocketWith(0).attMyMovements[0]));
            Assert.AreEqual(21000, testMyApp.opGetWallets()[0].opGetPocketWith(0).opGetBalance());
            #endregion
        }
        [TestMethod]
        public void testTransfer()
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetWallets().Add(new clsWallet(0, "georgeWallet", "georgi", "george@unicauca.edu.co"));
            testMyApp.opGetWallets()[0].opGetPockets().Add(new clsPocket(0, "georgePocket", 13.5, true));
            testMyApp.opGetCurrencies().Add(new clsCurrency(0, "COP", 3830));
            testMyApp.opGetWallets()[0].opGetPockets()[0].myCurrency = testMyApp.opGetCurrencies()[0];
            testMyApp.opGetWallets()[0].opGetPocketWith(0).opAddBalance(20000);
            testMyApp.opGetWallets().Add(new clsWallet(1, "JuanWallet", "Juan", "juan@unicauca.edu.co"));
            testMyApp.opGetWallets()[1].opGetPockets().Add(new clsPocket(0, "JuanPocket", 13.5, true));
            testMyApp.opGetWallets()[1].opGetPockets()[0].myCurrency = testMyApp.opGetCurrencies()[0];
            testExpectedObjMovement = new clsMovement(0, "Transfer", 1000, "09/06/2024");
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.opTransfer(0, 0, 0, 1, 0, 1000, "09/06/2024"));
            Assert.AreEqual(0, testExpectedObjMovement.CompareTo(testMyApp.opGetWallets()[0].opGetPocketWith(0).attMyMovements[0]));
            Assert.AreEqual(19000, testMyApp.opGetWallets()[0].opGetPocketWith(0).opGetBalance());
            Assert.AreEqual(1000, testMyApp.opGetWallets()[1].opGetPocketWith(0).opGetBalance());
            #endregion
        }
        [TestMethod]
        public void testConversionFromCOPToUSD()
        {
            #region Setup
            testMyApp.opClearMe();
            testMyApp.opGetWallets().Add(new clsWallet(0, "georgeWallet", "georgi", "george@unicauca.edu.co"));
            testMyApp.opGetWallets()[0].opGetPockets().Add(new clsPocket(0, "georgePocketCOP", 13.5, true));
            testMyApp.opGetWallets()[0].opGetPockets().Add(new clsPocket(1, "georgePocketUSD", 13.5, false));
            testMyApp.opGetCurrencies().Add(new clsCurrency(0, "COP", 3830));
            testMyApp.opGetCurrencies().Add(new clsCurrency(1, "USD", 1));
            testMyApp.opGetWallets()[0].opGetPockets()[0].myCurrency = testMyApp.opGetCurrencies()[0];
            testMyApp.opGetWallets()[0].opGetPockets()[1].myCurrency = testMyApp.opGetCurrencies()[1];
            testMyApp.opGetWallets()[0].opGetPocketWith(0).opAddBalance(19500);
            double testExpectedConversionAmount = 5;
            #endregion
            #region Test & Assert
            Assert.IsTrue(testMyApp.ConversionFromCOPToUSD(0, 0, 1, 19500));
            Assert.AreEqual(testExpectedConversionAmount, testMyApp.opGetWallets()[0].opGetPocketWith(1).opGetBalance());
            #endregion
        }
    }   
}
