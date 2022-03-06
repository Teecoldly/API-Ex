
using System;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
namespace Common.Service.Utility
{
    public static class UtilityFunc
    {
        #region Standard Function	
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            return firstDayInWeek;
        }
        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }
        public static string toReplace(string s)
        {
            s = s.Replace("&nbsp;", "");
            return s;
        }
        public static Boolean cBoolean(string st)
        {
            if (st.ToLower().Equals("true"))
            {
                return true;
            }
            else if (st.Equals("1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static DateTime cDate(string st)
        {
            if (IsDate(st))
            {
                return Convert.ToDateTime(st);
            }
            else
            {
                return new DateTime();
            }
        }

        public static string nDateNameFile(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('/');
            for (int i = 0; i < f.Length; i++)
            {
                if (i == 0)
                {
                    d = f[0].ToString();
                }
                else if (i == 1)
                {
                    m = f[1].ToString();
                    result = y + "-" + m;
                }
                else if (i == 2)
                {
                    y = Convert.ToString(Convert.ToUInt32(f[2].ToString()) - 2500);
                    result = d + "" + m + "" + y;
                }

            }

            return result;
        }
        public static DateTime? parseDateTime(string st)
        {
            if (IsDate(st))
            {
                return Convert.ToDateTime(st);
            }
            else
            {
                return null; //new DateTime();
            }
        }
        public static string nDateNameFolder(string s)
        {
            string y = "", result = "";
            string[] f = s.Split('/');
            for (int i = 0; i < f.Length; i++)
            {
                if (i == 2)
                {
                    y = Convert.ToString(Convert.ToUInt32(f[2].ToString()));
                    result = y;
                }

            }

            return result;
        }

        public static string nDateLoad(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('-');
            for (int i = 0; i < f.Length; i++)
            {
                if (i == 0)
                {
                    d = f[2].ToString();
                }
                else if (i == 1)
                {
                    m = f[1].ToString();
                    result = m + "-" + y;
                }
                else if (i == 2)
                {
                    y = Convert.ToString(Convert.ToUInt32(f[0].ToString()) + 543);
                    result = d + "/" + m + "/" + y;
                }

            }

            return result;
        }
        public static string nDate(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('/');
            for (int i = 0; i < f.Length; i++)
            {
                if (i == 0)
                {
                    d = f[0].ToString();
                }
                else if (i == 1)
                {
                    m = f[1].ToString();
                    result = m + "-" + d;
                }
                else if (i == 2)
                {
                    y = Convert.ToString(Convert.ToUInt32(f[2].ToString()) - 543);
                    result = y + "-" + m + "-" + d;
                }

            }

            return result;
        }
        public static string nDateSave(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('/');

            d = f[0].ToString();

            m = f[1].ToString();

            y = f[2].ToString();
            result = y + "-" + m + "-" + d;

            return result;
        }
        public static string nDateSaveBE(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('/');

            d = f[0].ToString();

            m = f[1].ToString();

            y = Convert.ToString(Convert.ToUInt32(f[2].ToString()));
            result = y + "-" + m + "-" + d;

            return result;
        }
        public static string nDateTH(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('/');

            d = f[0].ToString();

            m = f[1].ToString();

            y = Convert.ToString(Convert.ToUInt32(f[2].ToString()) + 543);
            result = d + " " + getMonthNameTH(ParseInt(m)) + " " + y;

            return result;
        }
        public static string nDateOnlyNum(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('/');

            d = f[0].ToString();

            m = f[1].ToString();

            y = f[2].ToString();
            result = y + m + d;

            return result;
        }
        public static int ParseInt(string st)
        {
            if (IsNumeric(st))
            {
                return Convert.ToInt32(st);
            }
            else
            {
                return 0;
            }
        }
        public static float ParseFloat(string st)
        {
            if (IsNumeric(st))
            {
                return float.Parse(st);
            }
            else
            {
                return 0;
            }
        }
        public static double ParseDouble(string st)
        {
            if (IsNumeric(st))
            {
                return Convert.ToDouble(st);
            }
            else
            {
                return 0;
            }
        }
        public static bool IsValueEqualsZero(double[] data)
        {
            double value = 0;
            for (int i = 0; i < data.Length; i++)
                value += data[i];
            if (value == 0)
                return false;
            else
                return true;
        }
        public static string cFormatString(int num, int length)
        {
            string st = "" + num;
            for (int i = st.Length; i < length; i++)
                st = "0" + st;
            return st;
        }
        public static string cFormatString(string st, int length)
        {
            if (st.Length == 0)
                return st;
            else
            {
                for (int i = st.Length; i < length; i++)
                    st = "0" + st;
                return st;
            }
        }
        public static bool IsDate(string strDate)
        {
            bool blnIsDate = false;
            try
            {
                DateTime myDateTime = DateTime.Parse(strDate);
                blnIsDate = true;
            }
            catch { }
            return (blnIsDate);
        }
        public static bool IsNumeric(string numberString)
        {
            bool isNum = false;
            double retNum;
            isNum = Double.TryParse(numberString, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static string writeNum(string st)
        {
            string reSt = "-";
            if (IsNumeric(st))
            {
                double num = Convert.ToDouble(st);
                if (num == 0)
                {
                    reSt = "-";
                }
                else
                {
                    reSt = num.ToString("#,##0");
                }
            }
            return reSt;
        }
        public static string writeNum(string st, int floting)
        {
            string reSt = "-";
            if (IsNumeric(st))
            {
                double num = Convert.ToDouble(st);
                string sFloating = "";
                for (int i = 0; i < floting; i++)
                    sFloating += "0";
                reSt = num.ToString("#,##0." + sFloating);
            }
            return reSt;
        }
        public static string writeNum2(string st)
        {
            string reSt = "0";
            if (IsNumeric(st))
            {
                double num = Convert.ToDouble(st);
                if (num == 0)
                {
                    reSt = "0";
                }
                else
                {
                    reSt = num.ToString("#,##0");
                }
            }
            return reSt;
        }
        public static string getDateString(DateTime d)
        {
            return d.ToLongDateString();
        }
        public static string getDateNumber(DateTime d)
        {
            return d.Year.ToString() + cFormatString(d.Month, 2) + cFormatString(d.Day, 2);
        }
        public static string getUniqId()
        {
            DateTime d = DateTime.Now;
            return d.ToString("yyyyMMddHHmmssfffffff");
        }
        public static string toThaiNumber(string s)
        {
            s = s.Replace("0", "๐");
            s = s.Replace("1", "๑");
            s = s.Replace("2", "๒");
            s = s.Replace("3", "๓");
            s = s.Replace("4", "๔");
            s = s.Replace("5", "๕");
            s = s.Replace("6", "๖");
            s = s.Replace("7", "๗");
            s = s.Replace("8", "๘");
            s = s.Replace("9", "๙");
            return s;
        }
        public static string getMonthNameTH(int m)
        {
            if (m == 1)
                return "มกราคม";
            else if (m == 2)
                return "กุมภาพันธ์";
            else if (m == 3)
                return "มีนาคม";
            else if (m == 4)
                return "เมษายน";
            else if (m == 5)
                return "พฤษภาคม";
            else if (m == 6)
                return "มิถุนายน";
            else if (m == 7)
                return "กรกฎาคม";
            else if (m == 8)
                return "สิงหาคม";
            else if (m == 9)
                return "กันยายน";
            else if (m == 10)
                return "ตุลาคม";
            else if (m == 11)
                return "พฤศจิกายน";
            else
                return "ธันวาคม";

        }
        private static string ParseHtml2Text(string content, string str, int ps)
        {
            int sStart;
            int sEnd;
            sStart = content.IndexOf(">", ps);
            if (sStart > -1)
            {
                sEnd = content.IndexOf("<", sStart + 1);
                if (sEnd > -1)
                {
                    str += content.Substring(sStart + 1, sEnd - (sStart + 1));
                    if (sEnd < content.Length - 1)
                    {
                        return ParseHtml2Text(content, str, sEnd + 1);
                    }
                }
            }
            return str;
        }
        public static string writeTextNull(string st)
        {
            string reSt = "";
            if (st == "" || st == null)
            {
                reSt = "-";
            }
            else
            {
                reSt = st;
            }
            return reSt;
        }
        public static string writeTextTimeNull(string st)
        {
            string reSt = "";
            if (st == "" || st == null)
            {
                reSt = "00:00";
            }
            else
            {
                reSt = st;
            }
            return reSt;
        }
        public static int fiscalYear(DateTime d)
        {
            int result = 0;
            int m = d.Month;

            if (m > 9) result = d.Year + 543 + 1;
            else result = d.Year + 543;

            return result;
        }
        public static string ParseText2Html(string txtRawText)
        {
            StringBuilder sb = new StringBuilder(HttpUtility.HtmlEncode(txtRawText));
            sb.Replace("&lt;b&gt;", "<b>");
            sb.Replace("&lt;/b&gt;", "</b>");
            sb.Replace("&lt;i&gt;", "<i>");
            sb.Replace("&lt;/i&gt;", "</i>");
            sb.Replace("&lt;p&gt;", "<p>");
            sb.Replace("&lt;/p&gt;", "</p>");
            sb.Replace("&lt;sup&gt;", "<sup>");
            sb.Replace("&lt;/sup&gt;", "</sup>");
            sb.Replace("&lt;sub&gt;", "<sub>");
            sb.Replace("&lt;/sub&gt;", "</sub>");
            sb.Replace("&lt;strong&gt;", "<strong>");
            sb.Replace("&lt;/strong&gt;", "</strong>");
            sb.Replace("\n", "<br />");
            sb.Replace("&lt;br /&gt;", "<br />");
            sb.Replace("&lt;br&gt;", "<br />");
            sb.Replace("&nbsp;", " ");
            sb.Replace("  ", "&nbsp;&nbsp;");
            sb.Replace("&quot;", "\"");
            sb.Replace("&amp;", "&");
            //Replace All Unwanted tag
            //sb.Replace("&lt;", "<");
            //sb.Replace("&gt;", ">");
            return sb.ToString();
        }
        public static string ParseHtml(string txtEncodeHtml)
        {
            StringBuilder sb = new StringBuilder(txtEncodeHtml);
            sb.Replace("<br />", "\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<BR>", "\n");
            sb.Replace("<BR/>", "\n");
            //sb.Replace("&quot;", "\"");
            sb.Replace("&nbsp;", " ");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            return HttpUtility.HtmlDecode(sb.ToString());
        }

        #endregion

        #region Security
        public static string Encrypt(string plainText, string passPhrase)
        {
            string saltValue = "s@1tValue";
            string hashAlgorithm = "SHA1";
            int passwordIterations = 2;
            string initVector = "@1B2c3D4e5F6g7H8";
            int keySize = 256;
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }
        public static string Decrypt(string cipherText, string passPhrase)
        {
            string saltValue = "s@1tValue";
            string hashAlgorithm = "SHA1";
            int passwordIterations = 2;
            string initVector = "@1B2c3D4e5F6g7H8";
            int keySize = 256;
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        #endregion


        #region Date
        public static string nDate8w(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('/');

            d = f[0].ToString();

            m = f[1].ToString();

            y = Convert.ToString(Convert.ToUInt32(f[2].ToString()) + 543);

            result = y + "-" + m + "-" + d;

            return result;
        }

        public static string nDate8(string s)
        {
            string d = "", m = "", y = "", result = "";
            string[] f = s.Split('/');
            for (int i = 0; i < f.Length; i++)
            {
                if (i == 0)
                {
                    d = f[0].ToString();
                }
                else if (i == 1)
                {
                    m = f[1].ToString();
                    result = m + "-" + d;
                }
                else if (i == 2)
                {
                    y = Convert.ToString(Convert.ToUInt32(f[2].ToString()) - 543);
                    result = Convert.ToInt32(d) + "/" + Convert.ToInt32(m) + "/" + y;
                }
            }
            return result;
        }
        public static bool ObjHasProp(object obj, string property_name)
        {
            var dicObj = ((IDictionary<String, object>)obj);
            return (dicObj.ContainsKey(property_name)
                    && dicObj[property_name] != null
                    && dicObj[property_name].ToString() != "");
        }
        #endregion
    }
}
