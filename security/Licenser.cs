using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace msgen.security
{
    public class Licenser
    {
        static public string generate(string appName, string appVersion, int seq)
        {
            return Hasher.sha1(appName + appVersion + seq, false);
        }

        static public bool validate(string appName, string appVersion, string key)
        {
            bool ok = false;
            for(int seq = 0; seq < 10000; seq++)
            {
                if(generate(appName, appVersion, seq) == key)
                    return true;
            }
            return ok;
        }


    }
}
