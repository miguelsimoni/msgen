using System;
using Microsoft.Win32;

namespace msgen
{
    public class RegManager
    {
        public static string getSetting(string application, string section, string key)
        {
            string path = string.Concat(@"SOFTWARE\xmsim\", application, @"\", section);
            while(path.EndsWith(@"\"))
            {
                path = path.Remove(path.Length - 1, 1);
            }
            path = Registry.LocalMachine.OpenSubKey(path).GetValue(key, "").ToString();
            return path;
        }

        public static void registerKey(string application, string section, string key, string value)
        {
            string path = string.Concat(@"SOFTWARE\xmsim\", application, @"\", section);
            Registry.LocalMachine.OpenSubKey(path).SetValue(key, value);
        }

    }
}
