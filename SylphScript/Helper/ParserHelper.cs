using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Helper
{
    public static class ParserHelper
    {
        public static bool SkipSlashAsteriskComment(ref int i, string code)
        {
            if (!CheckPos(i, code, "/*")) return false;
            i += 2;
            while (i < code.Length && !CheckPos(i - 2, code, "*/")) i++;
            return true;
        }

        public static bool SkipDoubleSlashComment(ref int i, string code)
        {
            if (!CheckPos(i, code, "//")) return false;
            i += 2;
            while (i < code.Length && code[i] != '\n') i++;
            i++;
            return true;
        }

        public static bool CheckPos(int i, string code, string target)
        {
            if (i + target.Length > code.Length) return false;
            for (int j = i; j - i < target.Length; j++)
            {
                if (code[j] != target[j - i]) return false;
            }
            return true;
        }

        public static string GetIdentifier(ref int i, string code)
        {
            StringBuilder sb = new StringBuilder();
            int start = i;
            while (i < code.Length && (char.IsLetter(code[i]) || (char.IsDigit(code[i]) && i != start) || code[i] == '_'))
            {
                sb.Append(code[i]);
                i++;
            }
            string str = sb.ToString();
            //if (str == "") i++;
            return str.ToString();
        }

        public static bool GetNumber(ref int i, string code, out int number)
        {
            int num = 0;
            bool success = false;
            while (i < code.Length && char.IsDigit(code[i]))
            {
                num *= 10;
                num += Convert.ToInt32("" + code[i]);
                i++;
                success = true;
            }
            number = num;
            return success;
        }

        public static void SkipSpace(ref int i, string code) // Skip ' ', '\t', '\r', '\n', comments
        {
            while (i < code.Length)
            {
                int j = i;
                if (code[i] == ' ' || code[i] == '\t' || code[i] == '\r' || code[i] == '\n') i++;
                else if (!ParserHelper.SkipDoubleSlashComment(ref i, code)) ParserHelper.SkipSlashAsteriskComment(ref i, code);
                if (j == i) return;
            }
        }
    }
}
