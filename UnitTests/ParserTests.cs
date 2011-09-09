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

namespace UnitTests
{
    [TestClass]
    public class ParserTests : SilverlightTest
    {
        [TestMethod]
        public void AlwaysTrue()
        {
            Assert.IsTrue(true, "this method always pass");
        }

        [TestMethod]
        [Description("Check that reviews object is created properly.")]
        public void CheckReviewsObjectCreation()
        {
            wp7rt_client.Classes.Reviews review = new wp7rt_client.Classes.Reviews();
            Assert.IsNotNull(review);
        }

        [TestMethod]
        [Description("Check intial values.")]
        public void CheckIntialValues()
        {
            wp7rt_client.Classes.Review review = new wp7rt_client.Classes.Review();
            Assert.Equals(review.AbsoluteLink, "");
            Assert.Equals(review.OrgScore, "NA");
            Assert.IsNotNull(review.Links);                   
        }
    }
}
