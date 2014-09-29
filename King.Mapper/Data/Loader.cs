﻿namespace King.Mapper.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Loader
    /// </summary>
    public class Loader<T> : ILoader<T>
            where T : new()
    {
        #region IDbCommand
        public virtual IEnumerable<T> Models(IDbCommand cmd, ActionFlags action = ActionFlags.Load)
        {
            if (null == cmd)
            {
                throw new ArgumentNullException("cmd");
            }

            var reader = cmd.ExecuteReader();

            return reader.Models<T>(action);
        }
        public virtual T Model(IDbCommand cmd, ActionFlags action = ActionFlags.Load)
        {
            if (null == cmd)
            {
                throw new ArgumentNullException("cmd");
            }

            var reader = cmd.ExecuteReader();
            return reader.Read() ? reader.Model<T>(action) : default(T);
        }
        #endregion

        #region IDataReader
        public virtual IEnumerable<T> Models(IDataReader reader, ActionFlags action = ActionFlags.Load)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            return reader.Models<T>(action);
        }

        public virtual T Model(IDataReader reader, ActionFlags action = ActionFlags.Load)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            return reader.Model<T>(action);
        }
        #endregion

        #region DataTable
        public virtual IEnumerable<T> Models(DataTable data, ActionFlags action = ActionFlags.Load)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            return data.Models<T>(action);
        }
        public virtual T Model(DataTable data, ActionFlags action = ActionFlags.Load)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            return data.Model<T>(action);
        }
        public virtual IDictionary<string, object> Dictionary(DataTable data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            return data.Dictionary();
        }
        public virtual IEnumerable<IDictionary<string, object>> Dictionaries(DataTable data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            return data.Dictionaries();
        }
        #endregion

        #region DataSet
        public virtual IEnumerable<T> Models(DataSet data, ActionFlags action = ActionFlags.Load)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            return data.Models<T>(action);
        }
        public virtual T Model(DataSet data, ActionFlags action = ActionFlags.Load)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            return data.Model<T>(action);
        }
        #endregion
    }
}