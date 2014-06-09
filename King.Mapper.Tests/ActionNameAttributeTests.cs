﻿namespace King.Mapper.Tests
{
    using King.Mapper;
    using King.Mapper.Tests.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ActionNameAttributeTests
    {
        #region Error Cases
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorInvalidName()
        {
            new ActionNameAttribute(null);
        }
        #endregion

        #region Valid Cases
        [TestMethod]
        public void Constructor()
        {
            new ActionNameAttribute(Guid.NewGuid().ToString(), ActionFlags.Execute);
        }

        [TestMethod]
        public void Name()
        {
            var data = Guid.NewGuid().ToString();
            var actionName = new ActionNameAttribute(data, ActionFlags.Load);
            Assert.AreEqual<string>(data, actionName.Name);
        }

        [TestMethod]
        public void Action()
        {
            var data = ActionFlags.Execute;
            var actionName = new ActionNameAttribute(Guid.NewGuid().ToString(), data);
            Assert.AreEqual<ActionFlags>(data, actionName.Action);
        }

        [TestMethod]
        public void ActionDefault()
        {
            var actionName = new ActionNameAttribute(Guid.NewGuid().ToString());
            Assert.AreEqual<ActionFlags>(ActionFlags.Load, actionName.Action);
        }

        [TestMethod]
        public void ValueMappingTestActionNames()
        {
            var random = new Random();
            var getValues = new TestActionNames()
            {
                Band = Guid.NewGuid().ToString(),
                Id = random.Next(),
                Song = Guid.NewGuid(),
            };

            var mappings = getValues.ValueMapping(new string[] { "Id", "Band", "Song" }, ActionFlags.Load);
            Assert.AreEqual<int>(5, mappings.Count);
            Assert.AreEqual<string>(getValues.Band, (string)mappings["Band"]);
            Assert.AreEqual<string>(getValues.Band, (string)mappings["CheckBandDude"]);
            Assert.AreEqual<int>(getValues.Id, (int)mappings["Id"]);
            Assert.AreEqual<Guid>(getValues.Song, (Guid)mappings["Song"]);
            Assert.AreEqual<Guid>(getValues.Song, (Guid)mappings["GuidGuid"]);
        }
        #endregion
    }
}