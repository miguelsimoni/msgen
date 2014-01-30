using System;
using System.Text.RegularExpressions;

namespace msgen
{
    public class Gen
    {
        private static string _msgboxerr = "Oops!";
        public static string msgBoxError
        {
            get { return _msgboxerr; }
            set { _msgboxerr = (value != string.Empty ? value : _msgboxerr); }
        }

        private static string _msgboxwrn = "Hey!";
        public static string msgBoxWarning
        {
            get { return _msgboxwrn; }
            set { _msgboxwrn = (value != string.Empty ? value : _msgboxwrn); }
        }

        private static string _msgboxnfo = "Info!";
        public static string msgBoxInfo
        {
            get { return _msgboxnfo; }
            set { _msgboxnfo = (value != string.Empty ? value : _msgboxnfo); }
        }

        private static string _contactsysadmin = "Contacte al administrador del sistema.";
        public static string contactSysAdmin
        {
            get { return _contactsysadmin; }
            set { _contactsysadmin = (value != string.Empty ? value : _contactsysadmin); }
        }

        public static String FORMAT_DECIMAL = "#,##0.00";

        public static Regex rgxNumber = new Regex(@"^[0-9]+$");

        public static Regex rgxDecimal = new Regex(@"^[0-9]+(\.[0-9][0-9]?)?$");

    }
}
