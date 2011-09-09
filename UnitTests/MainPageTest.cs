using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.Phone.Shell;
using wp7rt_client.Classes;


namespace UnitTests
{
    [TestClass]
    public class MainPageTest : SilverlightTest
    {
        [TestMethod]
        public void AlwaysTrue()
        {
            Assert.IsTrue(true, "this method always pass");
        }

        [TestMethod]
        [Description("Check if MainPage is created properly.")]
        public void CheckMainPageNotNull()
        {
            wp7rt_client.MainPage MainPage = new wp7rt_client.MainPage();
            Assert.IsNotNull(MainPage);            
        }

    }
}
