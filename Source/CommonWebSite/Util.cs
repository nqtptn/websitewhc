using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

namespace CommonWebSite
{
    public class Util
    {
        // Fields
        public static string CR = Convert.ToChar(13).ToString();
        public static int maxRetrievingItemNum = 150;
        public static DateTime NullDateTime = new DateTime(0x76c, 1, 1, 0, 0, 0);
        public static int PAGE_SIZE = 20;

        // Methods
        public static void AssignValues<T>(T entityA, T entityB, string[] exceptFields) where T: class
        {
            PropertyInfo[] properties = entityA.GetType().GetProperties();
            PropertyInfo[] infoArray2 = entityB.GetType().GetProperties();
            foreach (PropertyInfo info in properties)
            {
                Func<string, bool> predicate = null;
                foreach (PropertyInfo propertyInfoB in infoArray2)
                {
                    if (info.Name.Equals(propertyInfoB.Name))
                    {
                        if (exceptFields != null)
                        {
                            if (predicate == null)
                            {
                                predicate = delegate (string f) {
                                    return f.Equals(propertyInfoB.Name);
                                };
                            }
                            if (exceptFields.Where<string>(predicate).FirstOrDefault<string>() == null)
                            {
                                info.SetValue(entityA, propertyInfoB.GetValue(entityB, null), null);
                            }
                        }
                        else
                        {
                            info.SetValue(entityA, propertyInfoB.GetValue(entityB, null), null);
                        }
                    }
                }
            }
        }

        public static string DateTimeToString(DateTime dateTimeVal)
        {
            return Convert.ToString(dateTimeVal, new CultureInfo("en-GB"));
        }

        public static string DoubleToString(double dblVal)
        {
            return Convert.ToString(dblVal, new CultureInfo("en-GB"));
        }

        public static DateTime ExtractDateInTourCode(string tourCode)
        {
            try
            {
                int month = int.Parse(tourCode.Substring(6, 2));
                int year = int.Parse(tourCode.Substring(4, 2)) + 0x7d0;
                return new DateTime(year, month, int.Parse(tourCode.Substring(8, 2)));
            }
            catch (Exception)
            {
                return NullDateTime;
            }
        }

        public static DateTime GetCurrentDate()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }

        public static string GetDMYFromDate(DateTime theDateTime)
        {
            if (theDateTime.Equals(NullDateTime))
            {
                return string.Empty;
            }
            try
            {
                return (theDateTime.Day.ToString("00") + "/" + theDateTime.Month.ToString("00") + "/" + theDateTime.Year.ToString("0000"));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DateTime GetEndDateOfCurrentMonth()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int num3 = DateTime.DaysInMonth(year, month);
            return Convert.ToDateTime(string.Format("{0}-{1}-{2}", month, num3, year));
        }

        public static DateTime GetEndDateOfMonth(DateTime date)
        {
            int month = date.Month;
            int year = date.Year;
            int num3 = DateTime.DaysInMonth(year, month);
            return Convert.ToDateTime(string.Format("{0}-{1}-{2}", month, num3, year));
        }

        public static DateTime GetFirstDateOfCurrentMonth()
        {
            return Convert.ToDateTime(string.Format("{0}-{1}-{2}", DateTime.Now.Month, 1, DateTime.Now.Year));
        }

        public static DateTime GetFirstDateOfMonth(DateTime date)
        {
            return Convert.ToDateTime(string.Format("{0}-{1}-{2}", date.Month, 1, date.Year));
        }

        public static T GetValue<T>(object entity, string fieldName)
        {
            try
            {
                object obj2 = entity.GetType().GetProperty(fieldName).GetValue(entity, null);
                return ((obj2 == null) ? default(T) : ((T) obj2));
            }
            catch
            {
                return default(T);
            }
        }

        public static object GetValue(object entity, string fieldName)
        {
            try
            {
                return entity.GetType().GetProperty(fieldName).GetValue(entity, null);
            }
            catch
            {
                return null;
            }
        }

        public static string IntToString(int intVal)
        {
            return Convert.ToString(intVal, new CultureInfo("en-GB"));
        }

        public static bool IsDate(string anyString)
        {
            if (anyString == null)
            {
                anyString = "";
            }
            if (anyString.Length > 0)
            {
                try
                {
                    DateTime time = DateTime.Parse(anyString);
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool IsMatchString(string source, string regString)
        {
            Regex regex = new Regex(regString, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return regex.IsMatch(source);
        }

        public static bool IsNumeric(string strToCheck)
        {
            return Regex.IsMatch(strToCheck, @"^\d+(\.\d+)?$");
        }

        public static bool IsTourCodeDateValid(string tourCode)
        {
            return !ExtractDateInTourCode(tourCode).Equals(NullDateTime);
        }

        public static void SetValue(object entity, string fieldName, object value)
        {
            try
            {
                entity.GetType().GetProperty(fieldName).SetValue(entity, value, null);
            }
            catch
            {
            }
        }


    } 
}
