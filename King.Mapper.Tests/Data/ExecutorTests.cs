﻿namespace King.Mapper.Tests.Data
{
    using King.Mapper.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public void Constructor()
        {
            var connection = new SqlConnection();
            new Executor(connection);
        }

        [TestMethod]
        public void IsIExecutor()
        {
            var connection = new SqlConnection();
            Assert.IsNotNull(new Executor(connection) as IExecutor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorConnectionNull()
        {
            var sproc = Substitute.For<IStoredProcedure>();
            new Executor(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task NonQuerySprocNull()
        {
            var connection = new SqlConnection();
            var e = new Executor(connection);
            await e.NonQuery((IStoredProcedure)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task NonQueryCommandNull()
        {
            var connection = new SqlConnection();
            var e = new Executor(connection);
            await e.NonQuery((SqlCommand)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ExecuteSprocNull()
        {
            var connection = new SqlConnection();
            var e = new Executor(connection);
            await e.Query((IStoredProcedure)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ExecuteCommandNull()
        {
            var connection = new SqlConnection();
            var e = new Executor(connection);
            await e.Query((SqlCommand)null);
        }
    }
}