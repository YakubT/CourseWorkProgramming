using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork.src.main.cs.utility
{
    public class PropertiesUtil
    {
        private string dir = System.IO.Path.GetDirectoryName(
        System.Reflection.Assembly.GetExecutingAssembly().Location);

        private string file;

        public PropertiesUtil(string filePath)
        {
            this.file = filePath;
        }
        
        public string getValue(string key)
        {
            StreamReader streamReader = new StreamReader(file);
            string s;
            while ((s = streamReader.ReadLine()) != null)
            {
                if (s != "")
                {
                    string keytmp = s.Split('=')[0];
                    keytmp = keytmp.Substring(0, keytmp.Length - 1).Replace(" ", "");
                    if (keytmp.Equals(key))
                    {
                        streamReader.Close();
                        return s.Split('=')[1].Substring(1).Replace(" ", "");
                    }
                }
            }
            streamReader.Close();
            return null;
        }

        public void setValue(string key, string value)
        {
            if (File.Exists(file))
            {
                StreamReader streamReader = new StreamReader(file);
                string s;
                string res = "";
                while ((s = streamReader.ReadLine()) != null)
                {
                    if (s != "")
                    {
                        string keytmp = s.Split('=')[0];
                        keytmp = keytmp.Substring(0, keytmp.Length - 1).Replace(" ", "");
                        if (keytmp.Equals(key))
                        {
                            res += key + " = " + value;
                        }
                        else
                        {
                            res += s;
                        }
                        res += "\n";
                    }
                }
                streamReader.Close();
                StreamWriter streamWriter = new StreamWriter(file);
                streamWriter.WriteLine(res+key+" = "+value+"\n");
                streamWriter.Close();
            }
            else
            {
                string res = key + " = " + value;
                StreamWriter streamWriter = new StreamWriter(file);
                streamWriter.WriteLine(res);
                streamWriter.Close();
            }

        }
    }
}
