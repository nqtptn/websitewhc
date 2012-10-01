using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.Linq;
using System.Linq.Expressions;

namespace CommonWebSite
{
    public class EntityBL
    {
        // Fields
        private DataContext dataContext;

        // Events
        public event CreateDataContextHandler CreateDataContext;

        // Methods
        public DataContext GetDataContext()
        {
            return this.dataContext;
        }

        #region GetEntity
        public Table<T> GetEntity<T>() where T : class
        {
            this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
            return this.dataContext.GetTable<T>();
        }

        public T GetEntityByID<T>(string ID) where T : class
        {
            return this.GetEntityByID<T>(null, ID);
        }

        public T GetEntityByID<T>(DataContext dataContext, string ID) where T : class
        {
            T local;
            try
            {
                ParameterExpression expression;
                Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(T), "item"), "ID"), Expression.Constant(ID)), new ParameterExpression[] { expression });
                dataContext = (dataContext == null) ? this.CreateDataContext() : dataContext;
                local = dataContext.GetTable<T>().Where<T>(predicate).FirstOrDefault<T>();
            }
            catch (Exception)
            {
                throw;
            }
            return local;
        }
        
        public List<T> GetEntityList<T>() where T : class
        {
            List<T> list;
            try
            {
                list = this.GetEntity<T>().ToList<T>();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<T> GetEntityList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetEntityList<T>(string.Empty, predicate);
        }

        public List<T> GetEntityList<T>(string IDOrgUnit, Expression<Func<T, bool>> predicate) where T : class
        {
            Func<T, bool> func = null;
            List<T> list2;
            try
            {
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                List<T> source = this.dataContext.GetTable<T>().Where<T>(predicate).ToList<T>();
                if (!string.IsNullOrEmpty(IDOrgUnit))
                {
                    if (func == null)
                    {
                        func = delegate(T item)
                        {
                            return (Util.GetValue(item, "IDOrgUnit") is string) && ((Util.GetValue(item, "IDOrgUnit") as string) == IDOrgUnit);
                        };
                    }
                    source = source.Where<T>(func).ToList<T>();
                }
                list2 = source;
            }
            catch (Exception)
            {
                throw;
            }
            return list2;
        }

        public List<T> GetEntityListByOneField<T>(string fieldName, object value) where T : class
        {
            List<T> list;
            try
            {
                ParameterExpression expression;
                Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Convert(Expression.Property(expression = Expression.Parameter(typeof(T), "item"), fieldName), value.GetType()), Expression.Constant(value)), new ParameterExpression[] { expression });
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                list = this.dataContext.GetTable<T>().Where<T>(predicate).ToList<T>();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        private string GetEntityValueByProperty<T>(T entity, string propertyName) where T : class
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.Equals(propertyName))
                {
                    return (string)info.GetValue(entity, null);
                }
            }
            return string.Empty;
        }
        //Toan
        public T GetEntityByID<T>(int ID) where T : class
        {
            return this.GetEntityByID<T>(null, ID);
        }
        public T GetEntityByID<T>(DataContext dataContext, int ID) where T : class
        {
            T local;
            try
            {
                ParameterExpression expression;
                Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(expression = Expression.Parameter(typeof(T), "item"), "ID"), Expression.Constant(ID)), new ParameterExpression[] { expression });
                dataContext = (dataContext == null) ? this.CreateDataContext() : dataContext;
                local = dataContext.GetTable<T>().Where<T>(predicate).FirstOrDefault<T>();
            }
            catch (Exception)
            {
                throw;
            }
            return local;
        }
        //EToan
        #endregion

        #region Insert
        public bool InsertEntity<T>(T paramEntity) where T : class
        {
            return this.InsertEntity<T>(paramEntity, string.Empty);
        }

        public bool InsertEntity<T>(T paramEntity, string uniqueField) where T : class
        {
            bool flag;
            try
            {
                if (paramEntity == null)
                {
                    return false;
                }
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                if (!string.IsNullOrEmpty(uniqueField))
                {
                    object obj2 = Util.GetValue(paramEntity, uniqueField);
                    List<T> entityListByOneField = this.GetEntityListByOneField<T>(uniqueField, obj2);
                    if ((entityListByOneField != null) && (entityListByOneField.Count > 0))
                    {
                        return false;
                    }
                }
                this.dataContext.GetTable<T>().InsertOnSubmit(paramEntity);
                this.dataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public bool InsertEntityList<T>(IList<T> entityList) where T : class
        {
            return this.InsertEntityList<T>(entityList, string.Empty);
        }

        public bool InsertEntityList<T>(IList<T> entityList, string uniqueField) where T : class
        {
            bool flag;
            try
            {
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                if (!string.IsNullOrEmpty(uniqueField))
                {
                    List<T> list = new List<T>();
                    foreach (T local in entityList)
                    {
                        object obj2 = Util.GetValue(local, uniqueField);
                        List<T> entityListByOneField = this.GetEntityListByOneField<T>(uniqueField, obj2);
                        if ((entityListByOneField != null) && (entityListByOneField.Count > 0))
                        {
                            list.Add(local);
                        }
                    }
                    foreach (T local in list)
                    {
                        entityList.Remove(local);
                    }
                }
                this.dataContext.GetTable<T>().InsertAllOnSubmit<T>(entityList);
                this.dataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        #endregion

        #region Update
        public bool UpdateEntity<T>(T paramEntity) where T : class, new()
        {
            return this.UpdateEntity<T>(paramEntity, string.Empty);
        }

        public bool UpdateEntity<T>(T paramEntity, string uniqueField) where T : class, new()
        {
            bool flag;
            if (paramEntity == null)
            {
                return false;
            }
            try
            {
                Func<T, bool> predicate = null;
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                string paramEntityID = (string)Util.GetValue(paramEntity, "ID");
                T entityByID = this.GetEntityByID<T>(this.dataContext, paramEntityID);
                if (!string.IsNullOrEmpty(uniqueField))
                {
                    object obj2 = Util.GetValue(paramEntity, uniqueField);
                    if (predicate == null)
                    {
                        predicate = delegate(T item)
                        {
                            return ((string)Util.GetValue(item, "ID")) != paramEntityID;
                        };
                    }
                    if (this.GetEntityListByOneField<T>(uniqueField, obj2).Where<T>(predicate).FirstOrDefault<T>() != null)
                    {
                        return false;
                    }
                }
                if (entityByID != null)
                {
                    Util.AssignValues<T>(entityByID, paramEntity, new string[] { "ID" });
                }
                this.dataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public bool UpdateEntityList<T>(IList<T> entityList) where T : class, new()
        {
            return this.UpdateEntityList<T>(entityList, string.Empty);
        }

        public bool UpdateEntityList<T>(IList<T> entityList, string uniqueField) where T : class, new()
        {
            bool flag;
            try
            {
                foreach (T local in entityList)
                {
                    Func<T, bool> predicate = null;
                    this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                    string paramEntityID = (string)Util.GetValue(local, "ID");
                    T entityByID = this.GetEntityByID<T>(this.dataContext, paramEntityID);
                    if (!string.IsNullOrEmpty(uniqueField))
                    {
                        object obj2 = Util.GetValue(local, uniqueField);
                        if (predicate == null)
                        {
                            predicate = delegate(T item)
                            {
                                return ((string)Util.GetValue(item, "ID")) != paramEntityID;
                            };
                        }
                        if (this.GetEntityListByOneField<T>(uniqueField, obj2).Where<T>(predicate).FirstOrDefault<T>() != null)
                        {
                            continue;
                        }
                    }
                    if (entityByID != null)
                    {
                        Util.AssignValues<T>(entityByID, local, null);
                    }
                    this.dataContext.SubmitChanges();
                }
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        #endregion

        #region Delete
        public bool DeleteEntity<T>(T paramEntity) where T : class
        {
            bool flag;
            try
            {
                if (paramEntity == null)
                {
                    return false;
                }
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                string iD = (string)Util.GetValue(paramEntity, "ID");
                T entityByID = this.GetEntityByID<T>(this.dataContext, iD);
                this.dataContext.GetTable<T>().DeleteOnSubmit(entityByID);
                this.dataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public bool DeleteEntityAll<T>() where T : class
        {
            IList<T> entityList = this.GetEntityList<T>();
            foreach (T local in entityList)
            {
                this.DeleteEntity<T>(local);
            }
            return true;
        }

        public bool DeleteEntityByID<T>(string IDEntity) where T : class
        {
            bool flag;
            try
            {
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                T entityByID = this.GetEntityByID<T>(this.dataContext, IDEntity);
                this.DeleteEntity<T>(entityByID);
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }

        public bool DeleteEntityList<T>(IList<T> entityList) where T : class
        {
            foreach (T local in entityList)
            {
                this.DeleteEntity<T>(local);
            }
            return true;
        }
        //Toan
        public bool DeleteEntityByID<T>(int IDEntity) where T : class
        {
            bool flag;
            try
            {
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                T entityByID = this.GetEntityByID<T>(this.dataContext, IDEntity);
                this.DeleteEntity<T>(entityByID,IDEntity);
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public bool DeleteEntity<T>(T paramEntity,int ID) where T : class
        {
            bool flag;
            try
            {
                if (paramEntity == null)
                {
                    return false;
                }
                this.dataContext = (this.dataContext == null) ? this.CreateDataContext() : this.dataContext;
                int iD = (int)Util.GetValue(paramEntity, "ID");
                T entityByID = this.GetEntityByID<T>(this.dataContext, iD);
                this.dataContext.GetTable<T>().DeleteOnSubmit(entityByID);
                this.dataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        //EToan
        #endregion

        #region Find
        public T FindFirstEntity<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            List<T> entityList = this.GetEntityList<T>(predicate);
            if ((entityList != null) && (entityList.Count > 0))
            {
                return entityList[0];
            }
            return default(T);
        }

        public string FindIDByFieldName<T>(string key) where T : class, new()
        {
            return this.FindIDByFieldName<T>(key, "Code");
        }

        public string FindIDByFieldName<T>(string key, string fieldName) where T : class, new()
        {
            Func<T, bool> predicate = null;
            string str;
            try
            {
                if (predicate == null)
                {
                    predicate = delegate(T item)
                    {
                        return ((string)Util.GetValue(item, fieldName)) == key;
                    };
                }
                T entity = this.GetEntityList<T>().Where<T>(predicate).FirstOrDefault<T>();
                str = (entity != null) ? ((string)Util.GetValue(entity, "ID")) : string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
            return str;
        }
        #endregion

        // Nested Types
        public delegate DataContext CreateDataContextHandler();
    } 
}
