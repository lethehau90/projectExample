using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Common
{
    public class StringClass
    {

        /// <summary>
        /// Ma hoa chuoi ky tu (MD5)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            byte[] valueArray = Encoding.ASCII.GetBytes(value);
            valueArray = md5.ComputeHash(valueArray);
            var sb = new StringBuilder();
            for (int i = 0; i < valueArray.Length; i++)
                sb.Append(valueArray[i].ToString("x2").ToLower());
            return sb.ToString();
        }
        /// <summary>
        /// Tao mot chuoi ngau nghien
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        #region Random String
        public static string RandomString(int size)
        {
            Random rnd = new Random();
            string srds = "";
            string[] str = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < size; i++)
            {
                srds = srds + str[rnd.Next(0, 61)];
            }
            return srds;
        }

        public static int RandomNumber(int min, int max)
        {

            Random random = new Random(); return random.Next(min, max);
        }

        public static string RandomString(int size, bool lowerCase)
        {

            StringBuilder builder = new StringBuilder();

            Random random = new Random();
            char ch;

            for (int i = 0; i < size; i++)
            {

                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);

            }

            if (lowerCase)
                return builder.ToString().ToLower();

            return builder.ToString();
        }

        #endregion
        /// <summary>
        /// Tao chuoi dung cho rewrite url
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        #region Name To Tag
        public static string NameToTag(string strName)
        {
            string strReturn = "";
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            strReturn = Regex.Replace(strName, "[^\\w\\s]", string.Empty).Replace(" ", "-").ToLower();
            string strFormD = strReturn.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");
        }
        #endregion

        public static string ShowNameLevel(string Name, string Level)
        {
            int strLevel = (Level.Length / 5);
            string strReturn = "";
            for (int i = 1; i < strLevel; i++)
            {
                strReturn = strReturn + ".....";
            }
            strReturn += Name;
            return strReturn;
        }

        /// <summary>
        /// Xoa ky tu unicode tu chuoi
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        #region Remove Unicode
        public static string RemoveUnicode(string strString)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string strFormD = strString.Normalize(NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");
        }
        #endregion
        /// <summary>
        /// Ma hoa mot chuoi
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region Encode
        public static string Encode(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            string strtemp = Convert.ToBase64String(encbuff);
            string strtam = "";
            Int32 i = 0, len = strtemp.Length;
            for (i = 3; i <= len; i += 3)
            {
                strtam = strtam + strtemp.Substring(i - 3, 3) + RandomString(1);
            }
            strtam = strtam + strtemp.Substring(i - 3, len - (i - 3));
            return strtam;
        }
        #endregion
        /// <summary>
        /// Giai ma mot chuoi
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region Decode
        public static string Decode(string str)
        {
            string strtam = "";
            Int32 i = 0, len = str.Length;
            for (i = 4; i <= len; i += 4)
            {
                strtam = strtam + str.Substring(i - 4, 3);
            }
            strtam = strtam + str.Substring(i - 4, len - (i - 4));
            byte[] decbuff = Convert.FromBase64String(strtam);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        #endregion
        public static void sendmail(string to, string from, string bcc, string objects, string body, string user, string pass, string Mail_Port, string Mail_Smtp)
        {
            MailMessage mail = new MailMessage(from, to, objects, body);
            mail.BodyEncoding = mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Bcc.Add(bcc);
            mail.IsBodyHtml = true;
            SmtpClient smtps = new SmtpClient(Mail_Smtp);
            smtps.Port = Convert.ToInt32(Mail_Port);
            smtps.Credentials = new System.Net.NetworkCredential(user, pass);
            smtps.EnableSsl = true;
            smtps.Send(mail);
        }


        public static string CatChuoi(string s, int length)
        {
            if (String.IsNullOrEmpty(s))
                throw new ArgumentNullException(s);
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                throw new ArgumentException("Từ đầu tiên dài hơn chuỗi cần cắt");
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0}", sb.ToString().TrimEnd(' '));
                sb.Append(word + " ");
            }
            return string.Format("{0}", sb.ToString().TrimEnd(' '));
        }


        #region xóa các thẻ tag html

        public static string RemoveHTMLTag(string HTML)
        {
            // Xóa các thẻ html
            System.Text.RegularExpressions.Regex objRegEx = new System.Text.RegularExpressions.Regex("<[^>]*>");

            return objRegEx.Replace(HTML, "");
        }

        #endregion

        public static string VietHoa(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ  

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
        }
        /// <summary>
        /// /có dấu thành không dấu có gạch-
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string IDMatutang(string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }
        /* 
         * ID mã tự tăng: lưu ý phải có id tự động để nối
         * VD:
         * var booking = Common.ServiceConnect.AllBooking().OrderByDescending(a => a.ID).ToList();
         bookingID = booking.Count > 0 ? (booking[0].ID + 1).ToString() : "";
         //bookingID = "123";
         string bookingIDNew = ("B_0000000" + bookingID).IDMatutang(bookingID.Length + 9);*/
    }

}
