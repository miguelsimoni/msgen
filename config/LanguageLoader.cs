using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace msgen.config
{
    public class LanguageLoader
    {
        private static XmlDocument loadXml(string fileName)
        {
            XmlDocument xml = null;
            if(File.Exists(fileName))
            {
                xml = new XmlDocument();
                xml.Load(fileName);
            }
            return xml;
        }

        public static string getGlobalValue(string langFile, string msgID)
        {
            XmlDocument xml = loadXml(langFile);
            if(xml != null)
            {
                XmlNode node = xml.DocumentElement.SelectSingleNode(@"global/msg[@id='" + msgID + "']");
                if(node != null)
                {
                    return node.Attributes["val"].Value;
                }
            }
            return string.Empty;
        }

        public static Dictionary<string, string> getGlobalValues(string langFile)
        {
            Dictionary<string, string> globalValues = new Dictionary<string,string>();
            XmlDocument xml = loadXml(langFile);
            if(xml != null)
            {
                XmlNodeList globalNodes = xml.DocumentElement.SelectNodes(@"global/msg");
                foreach(XmlNode node in globalNodes)
                {
                    globalValues.Add(node.Attributes["id"].Value, node.Attributes["val"].Value);
                }
            }
            return globalValues;
        }

        public static string getControlValue(string langFile, string formName, string controlName)
        {
            XmlDocument xml = loadXml(langFile);
            if(xml != null)
            {
                XmlNode node = xml.DocumentElement.SelectSingleNode(@"form[@id='" + formName + "']/ctrl[@id='" + controlName + "']");
                if(node != null)
                {
                    return node.Attributes["val"].Value;
                }
            }
            return string.Empty;
        }

        public static void load(string langFile, Form form)
        {
            XmlDocument xml = loadXml(langFile);
            if(xml != null)
            {
                XmlNode formNode = xml.DocumentElement.SelectSingleNode(@"form[@id='" + form.Name + "']");
                if(formNode.Attributes["val"].Value != string.Empty)
                {
                    form.Text = formNode.Attributes["val"].Value;
                }
                setControlValues(form.Controls, formNode);
            }
        }

        private static void setControlValues(Control.ControlCollection controls, XmlNode formXmlNode)
        {
            XmlNode ctrlXmlNode;
            foreach(Control control in controls)
            {
                ctrlXmlNode = formXmlNode.SelectSingleNode(@"ctrl[@id='" + control.Name + "']");
                if(ctrlXmlNode != null)
                {
                    control.Text = ctrlXmlNode.Attributes["val"].Value;
                }
                if(control is GroupBox || control is Panel)
                {
                    setControlValues(control.Controls, formXmlNode);
                }
                else if(control is ToolStrip)
                {
                    setControlValues(((ToolStrip)control).Items, formXmlNode);
                }
            }
        }

        private static void setControlValues(ToolStripItemCollection items, XmlNode formXmlNode)
        {
            XmlNode ctrlXmlNode;
            foreach(ToolStripItem item in items)
            {
                ctrlXmlNode = formXmlNode.SelectSingleNode(@"ctrl[@id='" + item.Name + "']");
                if(ctrlXmlNode != null)
                {
                    item.Text = ctrlXmlNode.Attributes["val"].Value;
                }
                if(item is ToolStripMenuItem)
                {
                    setControlValues(((ToolStripMenuItem)item).DropDownItems, formXmlNode);
                }
            }
        }

        public static void generateXmlLangFile(List<Form> forms, string fileName)
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration header = xml.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xml.AppendChild(header);
            XmlElement root = xml.CreateElement("lang");
            xml.AppendChild(root);
            XmlElement formElem;
            foreach(Form form in forms)
            {
                formElem = xml.CreateElement("form");
                formElem.SetAttribute("id", form.Name);
                formElem.SetAttribute("val", form.Text);
                xml.DocumentElement.AppendChild(formElem);
                appendControls(form.Controls, form.Name, ref xml);
            }
            xml.Save(fileName);
        }

        private static void appendControls(Control.ControlCollection controls, string formName, ref XmlDocument xml)
        {
            foreach(Control control in controls)
            {
                XmlElement ctrlElem = xml.CreateElement("ctrl");
                ctrlElem.SetAttribute("id", control.Name);
                ctrlElem.SetAttribute("val", control.Text);
                xml.DocumentElement.SelectSingleNode(@"form[@id='" + formName + "']").AppendChild(ctrlElem);
                if(control is GroupBox || control is Panel)
                {
                    appendControls(control.Controls, formName, ref xml);
                }
                if(control is ToolStrip)
                {
                    appendControls(((ToolStrip)control).Items, formName, ref xml);
                }
            }
        }

        private static void appendControls(ToolStripItemCollection items, string formName, ref XmlDocument xml)
        {
            foreach(ToolStripItem item in items)
            {
                XmlElement ctrlElem = xml.CreateElement("ctrl");
                ctrlElem.SetAttribute("id", item.Name);
                ctrlElem.SetAttribute("val", item.Text);
                xml.DocumentElement.SelectSingleNode(@"form[@id='" + formName + "']").AppendChild(ctrlElem);
                if(item is ToolStripMenuItem)
                {
                    appendControls(((ToolStripMenuItem)item).DropDownItems, formName, ref xml);
                }
            }
        }

    }
}
