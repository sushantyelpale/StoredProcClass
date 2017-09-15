using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SPObj
{
    class WriteSPFile
    {
        public void WriteSPFileMethod(Dictionary<string, List<string>> SpNameClassNameDict)
        {
            /*            var indx = 0;
            foreach (var keyVal in SpNameClassNameDict)
            {
                Console.Write("\n"+count++ +" "+ keyVal.Key + "\t");
                foreach(var ele in keyVal.Value)
                {
                    if (indx > 0)
                    {
                        Console.Write(",");
                    }
                    Console.Write(ele);
                    indx++;
                }
                indx = 0;
            }
            */
            
            StringBuilder cmSep = new StringBuilder();
            foreach (var keyVal in SpNameClassNameDict)
            {
                //Console.Write("\n" + count++ + " " + keyVal.Key + "\t");
                cmSep.Append(keyVal.Key.ToString());
                cmSep.Append(" ");
                cmSep.Append(string.Join(",", keyVal.Value.Select(item => item.ToString())));
                //Console.Write(cmSep);
                cmSep.Append("\n");
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            try
            {
                File.WriteAllText(ofd.FileName, cmSep.ToString());
                MessageBox.Show("written an object to specified file");
            }catch (ArgumentException)
            {
                MessageBox.Show("Please Select one file ");
            }
        }
        
    }
}
