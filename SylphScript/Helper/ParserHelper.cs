﻿using System;
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
            while (!CheckPos(i - 2, code, "*/") && i < code.Length) i++;
            return true;
        }

        public static bool SkipDoubleSlashComment(ref int i, string code)
        {
            if (!CheckPos(i, code, "//")) return false;
            i += 2;
            while (code[i] != '\n' && i < code.Length) i++;
            i++;
            return true;
        }

        public static bool CheckPos(int i, string code, string target)
        {
            if (i + target.Length > code.Length) return false;
            for (int j = i; j < target.Length; j++)
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
            if (str == "") i++;
            return str.ToString();
        }

        public static int GetNumber(ref int i, string code)
        {
            int num = 0;
            while (i < code.Length && char.IsDigit(code[i]))
            {
                i++;
                num *= 10;
                num += Convert.ToInt32(code[i]);
            }
            return num;
        }
    }
}
