using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoImpression
{
    class Config
    {
        private string config_path;

        public Config() {
            this.config_path = System.Environment.CurrentDirectory.ToString()+"\\Config\\Config.ini";
        }

        public void WriteConfig(string title,string content) {
            
            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(this.config_path);
            file.WriteLine(title+"="+content);

            file.Close();
        }

        public String ReadConfig(string title) {
            try
            {
                string[] records = System.IO.File.ReadAllLines(this.config_path);

                foreach (string line in records)
                {
                    string[] splits = line.Split('=');

                    if (splits[0].Equals(title))
                        return splits[1].ToString();
                }
                return null;
            }catch(Exception ){
                return null;
            }
        }
    }
}
