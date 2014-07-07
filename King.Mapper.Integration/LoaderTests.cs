﻿namespace King.Mapper.Integration
{
    using King.Mapper.Data;
    using King.Mapper.Integration.Model;
    using NUnit.Framework;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    [TestFixture]
    public class LoaderTests
    {
        #region Members
        private readonly string connectionString = ConfigurationManager.AppSettings["database"];
        #endregion

        [Test]
        public async Task ReaderModel()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = SimulatedSelectStatement.Create();

                var cmd = sproc.Build(con);
                await con.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                Assert.IsTrue(reader.Read());

                var loader = new Loader<SelectData>();
                var obj = loader.Model(reader);

                Assert.IsNotNull(obj);
                Assert.AreEqual(sproc.TestInt, obj.Identifier);
                Assert.AreEqual(sproc.TestBigInt, obj.BigInt);
                Assert.AreEqual(sproc.TestBit, obj.Bit);
                Assert.AreEqual(sproc.TestDate.Date, obj.Date.Date);
                Assert.AreEqual(sproc.TestDateTime.Date, obj.DateTime.Date);
                Assert.AreEqual(sproc.TestDateTime2.Date, obj.DateTime2.Date);
                Assert.AreEqual(sproc.TestDecimal, obj.Decimal);
                Assert.AreEqual(sproc.TestFloat, obj.Float);
                Assert.AreEqual(Math.Round(sproc.TestMoney, 4), obj.Money);
                Assert.AreEqual(sproc.TestNChar, obj.NChar);
                Assert.AreEqual(sproc.TestNText, obj.NText);
                Assert.AreEqual(sproc.TestText, obj.Text);
                CollectionAssert.AreEqual(sproc.TestBinary, obj.Binary);
                CollectionAssert.AreEqual(sproc.TestImage, obj.Image);
                Assert.AreEqual(sproc.TestGuid, obj.Unique);
            }
        }

        [Test]
        public async Task ReaderModels()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = new SelectMultipleStatement();

                var cmd = sproc.Build(con);
                await con.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                var loader = new Loader<SelectData>();
                var objs = loader.Models(reader);

                Assert.IsNotNull(objs);

                var i = 0;
                foreach (var obj in objs)
                {
                    Assert.AreEqual(i, obj.Identifier);
                    i++;
                }
            }
        }

        [Test]
        public async Task IDbCommandModel()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = SimulatedSelectStatement.Create();

                var cmd = sproc.Build(con);

                var loader = new Loader<SelectData>();
                await con.OpenAsync();

                var obj = loader.Model(cmd);

                Assert.IsNotNull(obj);
                Assert.AreEqual(sproc.TestInt, obj.Identifier);
                Assert.AreEqual(sproc.TestBigInt, obj.BigInt);
                Assert.AreEqual(sproc.TestBit, obj.Bit);
                Assert.AreEqual(sproc.TestDate.Date, obj.Date.Date);
                Assert.AreEqual(sproc.TestDateTime.Date, obj.DateTime.Date);
                Assert.AreEqual(sproc.TestDateTime2.Date, obj.DateTime2.Date);
                Assert.AreEqual(sproc.TestDecimal, obj.Decimal);
                Assert.AreEqual(sproc.TestFloat, obj.Float);
                Assert.AreEqual(Math.Round(sproc.TestMoney, 4), obj.Money);
                Assert.AreEqual(sproc.TestNChar, obj.NChar);
                Assert.AreEqual(sproc.TestNText, obj.NText);
                Assert.AreEqual(sproc.TestText, obj.Text);
                CollectionAssert.AreEqual(sproc.TestBinary, obj.Binary);
                CollectionAssert.AreEqual(sproc.TestImage, obj.Image);
                Assert.AreEqual(sproc.TestGuid, obj.Unique);
            }
        }

        [Test]
        public async Task IDbCommandLoadNothing()
        {
            var random = new Random();
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = new SimulatedInsertStatement()
                {
                    TestInt = random.Next(),
                };

                var cmd = sproc.Build(con);

                var loader = new Loader<SelectData>();
                await con.OpenAsync();

                var obj = loader.Model(cmd);

                Assert.IsNull(obj);
            }
        }

        [Test]
        public async Task IDbCommandModels()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = new SelectMultipleStatement();

                var cmd = sproc.Build(con);

                var loader = new Loader<SelectData>();
                await con.OpenAsync();

                var objs = loader.Models(cmd);

                Assert.IsNotNull(objs);

                var i = 0;
                foreach (var obj in objs)
                {
                    Assert.AreEqual(i, obj.Identifier);
                    i++;
                }
            }
        }

        [Test]
        public async Task DataTableModel()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = SimulatedSelectStatement.Create();

                var cmd = sproc.Build(con);

                var loader = new Loader<SelectData>();
                await con.OpenAsync();
                var adapter = new SqlDataAdapter(cmd);

                var ds = new DataSet();
                adapter.Fill(ds);
                var table = ds.Tables[0];
                var obj = loader.Model(table);

                Assert.IsNotNull(obj);
                Assert.AreEqual(sproc.TestInt, obj.Identifier);
                Assert.AreEqual(sproc.TestBigInt, obj.BigInt);
                Assert.AreEqual(sproc.TestBit, obj.Bit);
                Assert.AreEqual(sproc.TestDate.Date, obj.Date.Date);
                Assert.AreEqual(sproc.TestDateTime.Date, obj.DateTime.Date);
                Assert.AreEqual(sproc.TestDateTime2.Date, obj.DateTime2.Date);
                Assert.AreEqual(sproc.TestDecimal, obj.Decimal);
                Assert.AreEqual(sproc.TestFloat, obj.Float);
                Assert.AreEqual(Math.Round(sproc.TestMoney, 4), obj.Money);
                Assert.AreEqual(sproc.TestNChar, obj.NChar);
                Assert.AreEqual(sproc.TestNText, obj.NText);
                Assert.AreEqual(sproc.TestText, obj.Text);
                CollectionAssert.AreEqual(sproc.TestBinary, obj.Binary);
                CollectionAssert.AreEqual(sproc.TestImage, obj.Image);
                Assert.AreEqual(sproc.TestGuid, obj.Unique);
            }
        }

        [Test]
        public async Task DataTableModels()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = new SelectMultipleStatement();

                var cmd = sproc.Build(con);

                var loader = new Loader<SelectData>();
                await con.OpenAsync();
                var adapter = new SqlDataAdapter(cmd);

                var ds = new DataSet();
                adapter.Fill(ds);
                var table = ds.Tables[0];
                var objs = loader.Models(table);

                Assert.IsNotNull(objs);

                var i = 0;
                foreach (var obj in objs)
                {
                    Assert.AreEqual(i, obj.Identifier);
                    i++;
                }
            }
        }

        [Test]
        public async Task DataSetModel()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = SimulatedSelectStatement.Create();

                var cmd = sproc.Build(con);

                var loader = new Loader<SelectData>();
                await con.OpenAsync();
                var adapter = new SqlDataAdapter(cmd);

                var ds = new DataSet();
                adapter.Fill(ds);
                var obj = loader.Model(ds);

                Assert.IsNotNull(obj);
                Assert.AreEqual(sproc.TestInt, obj.Identifier);
                Assert.AreEqual(sproc.TestBigInt, obj.BigInt);
                Assert.AreEqual(sproc.TestBit, obj.Bit);
                Assert.AreEqual(sproc.TestDate.Date, obj.Date.Date);
                Assert.AreEqual(sproc.TestDateTime.Date, obj.DateTime.Date);
                Assert.AreEqual(sproc.TestDateTime2.Date, obj.DateTime2.Date);
                Assert.AreEqual(sproc.TestDecimal, obj.Decimal);
                Assert.AreEqual(sproc.TestFloat, obj.Float);
                Assert.AreEqual(Math.Round(sproc.TestMoney, 4), obj.Money);
                Assert.AreEqual(sproc.TestNChar, obj.NChar);
                Assert.AreEqual(sproc.TestNText, obj.NText);
                Assert.AreEqual(sproc.TestText, obj.Text);
                CollectionAssert.AreEqual(sproc.TestBinary, obj.Binary);
                CollectionAssert.AreEqual(sproc.TestImage, obj.Image);
                Assert.AreEqual(sproc.TestGuid, obj.Unique);
            }
        }

        [Test]
        public async Task DataSetModels()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var sproc = new SelectMultipleStatement();

                var cmd = sproc.Build(con);

                var loader = new Loader<SelectData>();
                await con.OpenAsync();
                var adapter = new SqlDataAdapter(cmd);

                var ds = new DataSet();
                adapter.Fill(ds);
                var objs = loader.Models(ds);

                Assert.IsNotNull(objs);

                var i = 0;
                foreach (var obj in objs)
                {
                    Assert.AreEqual(i, obj.Identifier);
                    i++;
                }
            }
        }
    }
}