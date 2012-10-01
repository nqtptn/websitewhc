using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CommonWebSite
{
    public class EntityHelper
    {
        // Methods
        public static void AssignBlankValues(object blankEntity)
        {
            PropertyInfo[] properties = blankEntity.GetType().GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (info.PropertyType == typeof(string))
                {
                    info.SetValue(blankEntity, string.Empty, null);
                }
                if ((info.PropertyType == typeof(double?)) || (info.PropertyType == typeof(double)))
                {
                    info.SetValue(blankEntity, 0.0, null);
                }
                if ((info.PropertyType == typeof(Guid?)) || (info.PropertyType == typeof(Guid)))
                {
                    info.SetValue(blankEntity, Guid.Empty, null);
                }
                if ((info.PropertyType == typeof(DateTime?)) || (info.PropertyType == typeof(DateTime)))
                {
                    info.SetValue(blankEntity, Util.NullDateTime, null);
                }
                if (((((info.PropertyType == typeof(int?)) || (info.PropertyType == typeof(int))) || ((info.PropertyType == typeof(short?)) || (info.PropertyType == typeof(short)))) || (info.PropertyType == typeof(int?))) || (info.PropertyType == typeof(int)))
                {
                    info.SetValue(blankEntity, 0, null);
                }
            }
        }

        private static bool CheckID<T>(T p, string ID)
        {
            string str = (string) Util.GetValue(p, "ID");
            return str.Equals(ID);
        }

        public static T CreateBlankEntity<T>() where T: class, new()
        {
            T blankEntity = Activator.CreateInstance<T>();
            AssignBlankValues(blankEntity);
            return blankEntity;
        }

        public static List<T> CreateListWithBlankItem<T>(object draftList) where T: class, new()
        {
            List<T> list = (List<T>) draftList;
            list.Add(Activator.CreateInstance<T>());
            return list;
        }

        public static int GetIndex<T>(IList<T> list, string ID) where T: class, new()
        {
            int num = 0;
            foreach (T local in list)
            {
                if (CheckID<T>(local, ID))
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        public static T GetItem<T>(IList<T> list, string ID) where T: class, new()
        {
            T local = list.Where<T>(delegate (T p) {
                return CheckID<T>(p, ID);
            }).FirstOrDefault<T>();
            return ((local == null) ? Activator.CreateInstance<T>() : local);
        }
    }
}
