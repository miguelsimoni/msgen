using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace msgen.io
{
    public class IniFile
    {
        public enum CommentSign
        {
            Semicolon,
            Number
        }

        public enum KeyValueDelimiterSign
        {
            Equal,
            Colon
        }

        private string defaultSectionName = string.Empty;

        private char keyValueDelimiter;
        public char KeyValueDelimiter
        {
            get { return keyValueDelimiter; }
            //set { delimiter = value; }
        }

        private char commentCharacter;
        public char CommentCharacter
        {
            get { return commentCharacter; }
            //set { comment = value; }
        }

        private Dictionary<string, Dictionary<string, string>> sections;
        public Dictionary<string, Dictionary<string, string>> Sections
        {
            get { return sections; }
            set { sections = value; }
        }

        public Dictionary<string, string> this[string sectionName]
        {
            get { return sections[sectionName]; }
            set { sections[sectionName] = value; }
        }

        public IniFile()
        {
            this.sections = new Dictionary<string, Dictionary<string, string>>();
            this.keyValueDelimiter = '=';
            this.commentCharacter = ';';
        }

        public IniFile(KeyValueDelimiterSign delimiter, CommentSign comment)
        {
            this.sections = new Dictionary<string, Dictionary<string, string>>();
            //asign key-value delimiter character
            this.keyValueDelimiter = '=';
            if(delimiter == KeyValueDelimiterSign.Colon)
                this.keyValueDelimiter = ':';
            //asign comment character
            this.commentCharacter = ';';
            if(comment == CommentSign.Number)
                this.commentCharacter = '#';
        }

        public IniFile(string filePath)
        {
            this.sections = new Dictionary<string, Dictionary<string, string>>();
            this.keyValueDelimiter = '=';
            this.commentCharacter = ';';
            read(filePath);
        }

        public IniFile(string filePath, KeyValueDelimiterSign delimiter, CommentSign comment)
        {
            this.sections = new Dictionary<string, Dictionary<string, string>>();
            //asign key-value delimiter character
            this.keyValueDelimiter = '=';
            if(delimiter == KeyValueDelimiterSign.Colon)
                this.keyValueDelimiter = ':';
            //asign comment character
            this.commentCharacter = ';';
            if(comment == CommentSign.Number)
                this.commentCharacter = '#';
            //read the .ini file
            read(filePath);
        }

        public void read(string filePath)
        {
            string sectionName = "";
            Dictionary<string, string> section = null;
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filePath, Encoding.Default);
                string line = sr.ReadLine();
                while(line != null)
                {
                    if(line.Trim().Length > 0)
                    {
                        if(line.IndexOf(this.commentCharacter) > -1)
                        {
                            line = line.Remove(line.IndexOf(this.commentCharacter));
                        }
                        line = line.TrimStart().TrimEnd();
                        if(line.Length > 0)
                        {
                            if(line.StartsWith("[") && line.EndsWith("]"))
                            {
                                sectionName = line.Replace("[", "").Replace("]", "").TrimStart().TrimEnd();
                                section = new Dictionary<string, string>();
                                line = sr.ReadLine();
                                while(line != null && !line.StartsWith("["))
                                {
                                    addToSection(section, line);
                                    line = sr.ReadLine();
                                }
                                this.sections.Add(sectionName, section);
                            }
                            else
                            {
                                if(!this.sections.ContainsKey(this.defaultSectionName))
                                {
                                    this.sections.Add(this.defaultSectionName, new Dictionary<string, string>());
                                }
                                addToSection(this.sections[this.defaultSectionName], line);
                                line = sr.ReadLine();
                            }
                        }
                        else
                        {
                            line = sr.ReadLine();
                        }
                    }
                }
            }
            catch(Exception)
            {
                //todo handle read ini file exception
            }
            finally
            {
                if(sr != null)
                    sr.Close();
            }
        }

        private void addToSection(Dictionary<string, string> section, string line)
        {
            if(line.Contains(this.keyValueDelimiter.ToString()))
            {
                string[] item = line.Split(this.keyValueDelimiter);
                string key = item[0].TrimStart().TrimEnd();
                string value = item[1].TrimStart().TrimEnd();
                section.Add(key, value);
            }
            else
            {
                section.Add(line, null);
            }
        }

        public void write(string filePath)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(filePath, false, Encoding.Default);
                sw.Write(this.ToString());
            }
            catch(Exception)
            {
                //todo handle write .ini file exception
            }
            finally
            {
                if(sw != null)
                    sw.Close();
            }
        }

        public void addSection(string sectionName)
        {
            this.sections.Add(sectionName, new Dictionary<string, string>());
        }

        public void removeSection(string sectionName)
        {
            this.sections.Remove(sectionName);
        }

        public void addItem(string sectionName, string key, string value)
        {
            this.sections[sectionName].Add(key, value);
        }

        public void addItem(string sectionName, KeyValuePair<string, string> item)
        {
            this.sections[sectionName].Add(item.Key, item.Value);
        }

        public void removeItem(string sectionName, string key)
        {
            this.sections[sectionName].Remove(key);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<string, Dictionary<string, string>> section in this.sections)
            {
                if(section.Key.Length > 0)
                {
                    sb.Append("[");
                    sb.Append(section.Key);
                    sb.Append("]");
                    sb.AppendLine();
                }
                foreach(KeyValuePair<string, string> item in section.Value)
                {
                    sb.Append(item.Key);
                    if(item.Value != null)
                    {
                        sb.Append(this.keyValueDelimiter);
                        sb.Append(item.Value);
                    }
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}
