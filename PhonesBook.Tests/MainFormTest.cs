// <copyright file="MainFormTest.cs" company="@Yuri Ryabchenko">©Yuri Ryabchenko 2017 - 2018</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;
using PhonesBook;

namespace PhonesBook.Tests
{
    /// <summary>This class contains parameterized unit tests for MainForm</summary>
    [PexClass(typeof(MainForm))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestFixture]
    public partial class MainFormTest
    {
        /// <summary>Test stub for CheckServerAliveDB(String, String, String)</summary>

        [PexMethod]
        public void CheckServerAliveDBTest(
            [PexAssumeUnderTest]MainForm target,
            string serverName,
            string userName,
            string userPassword
        )
        {
            target.CheckServerAliveDB(serverName, userName, userPassword);
            PexAssume.IsNotNullOrEmpty(serverName);
            PexAssume.IsNotNullOrEmpty(userName);
            PexAssume.IsNotNullOrEmpty(userPassword);

            Assert.Fail();
            // TODO: add assertions to method MainFormTest.CheckServerAliveDBTest(MainForm, String, String, String)
        }

        /// <summary>Test stub for CheckServerAliveDB(String, String, String)</summary>
    }
}
