﻿namespace King.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region System.Object
        /// <summary>
        /// Get Properties
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Property Info</returns>
        public static PropertyInfo[] GetProperties(this object value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var t = value.GetType();
            return t.GetProperties();
        }

        /// <summary>
        /// Convert Object to Dictionary
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <returns>Dictionary</returns>
        public static IDictionary<string, object> ToDictionary(this object value, ActionFlags action = ActionFlags.Load)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var parameters = value.Parameters();
            return value.ValueMapping(parameters, action);
        }

        /// <summary>
        /// Parameters
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Parameters</returns>
        public static string[] Parameters(this object value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return (from property in value.GetProperties()
                    where property.CanWrite
                    select property.Name).ToArray();
        }

        /// <summary>
        /// Get Value Mapping of Object and parameters
        /// </summary>
        /// <param name="value">Object with Values</param>
        /// <param name="parameters">Stored Procedure Parameters</param>
        /// <param name="action">Action</param>
        /// <returns>Mapped Parameters to Values</returns>
        public static IDictionary<string, object> ValueMapping(this object value, string[] parameters, ActionFlags action = ActionFlags.Execute)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            if (null == parameters)
            {
                throw new ArgumentNullException("parameters");
            }

            var row = new Dictionary<string, object>(parameters.Length);
            var columnHash = new HashSet<string>(parameters);
            var properties = value.GetProperties();

            foreach (var property in properties)
            {
                foreach (var actionName in property.GetAttributes<ActionNameAttribute>())
                {
                    if (null != actionName && actionName.Action == action)
                    {
                        var name = actionName.Name.Replace("@", string.Empty);
                        if (!columnHash.Contains(name) && !row.ContainsKey(name))
                        {
                            row.Add(name, property.GetValue(value, null));
                        }
                    }
                }

                if (columnHash.Contains(property.Name))
                {
                    row.Add(property.Name, property.GetValue(value, null));
                }
            }

            return row;
        }

        /// <summary>
        /// Fill Object with values from the data store
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="value">Value</param>
        /// <param name="columns">Columns</param>
        /// <param name="values">Values</param>
        /// <param name="action">Action</param>
        public static void Fill(this object value, string[] columns, object[] values, ActionFlags action = ActionFlags.Load)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (null == columns)
            {
                throw new ArgumentNullException("columns");
            }

            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

            if (columns.Length != values.Length)
            {
                throw new ArgumentException("Columns don't match values.");
            }

            var properties = value.GetProperties();

            var propertyDictionary = new Dictionary<string, PropertyInfo>(properties.Count());
            foreach (var property in properties)
            {
                propertyDictionary.Add(property.Name, property);

                var names = from a in property.GetAttributes<ActionNameAttribute>()
                            where null != a && a.Action == action && !propertyDictionary.ContainsKey(a.Name)
                            select a;

                foreach (var actionName in names)
                {
                    propertyDictionary.Add(actionName.Name, property);
                }
            }

            for (var i = 0; i < columns.Length; i++)
            {
                if (propertyDictionary.ContainsKey(columns[i]))
                {
                    var property = propertyDictionary[columns[i]];
                    if (null != property)
                    {
                        property.Set(value, values[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Map properties from one object to another
        /// </summary>
        /// <typeparam name="T">Type of Secondary Object</typeparam>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        /// <returns>Object</returns>
        public static T Map<T>(this object from, T to)
        {
            if (null == from)
            {
                throw new ArgumentNullException("from");
            }

            if (null == to)
            {
                throw new ArgumentNullException("to");
            }

            var properties = from.ValueMapping(from.Parameters());
            to.Fill(properties.Keys.ToArray(), properties.Values.ToArray());
            return to;
        }

        /// <summary>
        /// Map properties from one object to another
        /// </summary>
        /// <typeparam name="T">Type of Secondary Object</typeparam>
        /// <param name="from">From</param>
        /// <returns>Object</returns>
        public static T Map<T>(this object from)
        {
            if (null == from)
            {
                throw new ArgumentNullException("from");
            }

            return from.Map<T>(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Get Attribute
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Attribute</returns>
        public static T GetAttribute<T>(this object value)
            where T : Attribute
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var t = value.GetType();
            var attributes = t.GetCustomAttributes(false);
            return attributes.GetAttribute<T>();
        }
        #endregion

        #region System.Object[]
        /// <summary>
        /// Get Attribute
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        /// <param name="attributes">Attributes</param>
        /// <returns>Attribute of Type</returns>
        public static T GetAttribute<T>(this object[] attributes)
            where T : Attribute
        {
            if (null == attributes)
            {
                throw new ArgumentNullException("attributes");
            }

            return (T)(from item in attributes
                       where item.GetType() == typeof(T)
                       select item).FirstOrDefault();
        }
        #endregion

        #region System.Reflection.PropertyInfo
        /// <summary>
        /// Get Attribute
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        /// <param name="attributes">Attributes</param>
        /// <returns>Attribute of Type</returns>
        public static IEnumerable<T> GetAttributes<T>(this PropertyInfo property)
            where T : Attribute
        {
            if (null == property)
            {
                throw new ArgumentNullException("property");
            }

            var enumeration = new List<T>();

            foreach (var obj in property.GetCustomAttributes(false))
            {
                if (obj.GetType() == typeof(T))
                {
                    enumeration.Add((T)obj);
                }
            }

            return enumeration;
        }

        /// <summary>
        /// Get Attribute
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        /// <param name="attributes">Attributes</param>
        /// <returns>Attribute of Type</returns>
        public static T GetAttribute<T>(this PropertyInfo property)
            where T : Attribute
        {
            if (null == property)
            {
                throw new ArgumentNullException("property");
            }

            return (from a in property.GetAttributes<T>()
                    select a).FirstOrDefault();
        }

        /// <summary>
        /// Set Value of Property
        /// </summary>
        /// <param name="property">Property</param>
        /// <param name="value">Value</param>
        public static void Set(this PropertyInfo property, object owner, object value = null)
        {
            if (null == property)
            {
                throw new ArgumentNullException("property");
            }

            if (null == owner)
            {
                throw new ArgumentNullException("owner");
            }

            if (property.CanWrite)
            {
                object ptr = null;
                if (DBNull.Value != value && null != value)
                {
                    if (property.PropertyType.BaseType == typeof(Enum))
                    {
                        ptr = Enum.ToObject(property.PropertyType, value);
                    }
                    else if (value as IConvertible != null)
                    {
                        var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        ptr = Convert.ChangeType(value, t);
                    }
                    else
                    {
                        ptr = value;
                    }
                }

                property.SetValue(owner, ptr, null);
            }
        }
        #endregion

        #region System.Collections.Generic
        public static T Map<T>(this IDictionary<string, object> dictionary, ActionFlags flag = ActionFlags.Load)
        {
            if (null == dictionary)
            {
                throw new ArgumentNullException("dictionary");
            }

            var obj = Activator.CreateInstance<T>();
            obj.Fill(dictionary.Keys.ToArray(), dictionary.Values.ToArray(), flag);
            return obj;
        }
        #endregion

        #region System.Collections.Generic
        public static T Map<T>(this NameValueCollection dictionary, ActionFlags flag = ActionFlags.Load)
        {
            if (null == dictionary)
            {
                throw new ArgumentNullException("dictionary");
            }

            var obj = Activator.CreateInstance<T>();
            if (dictionary.HasKeys())
            {
                var keys = dictionary.AllKeys;
                var objs = new object[keys.Count()];
                for (var i = 0; i < objs.Count(); i++)
                {
                    objs[i] = dictionary[keys[i]];
                }

                obj.Fill(keys, objs, flag);
            }
            return obj;
        }
        #endregion
    }
}