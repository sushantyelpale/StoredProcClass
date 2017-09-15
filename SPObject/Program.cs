using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace SPObj
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Dictionary<string, List<string>> SpNameClassNameDict = new Dictionary<string, List<string>>();

            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.ShowDialog();
            string fileName = opnfd.FileName;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                MessageBox.Show("Please Select one file ");
                return;
            }

            Solution solution = new Solution(fileName);
            solution.ChooseCSProjFile(fileName);

            MatchExpr Mr = new MatchExpr();
            Mr.SearchInvocationToAdd(solution);

            FindSP findSP  = new FindSP();
            findSP.CheckforAnySP(solution, SpNameClassNameDict);

            WriteSPFile writeSPFile = new WriteSPFile();
            writeSPFile.WriteSPFileMethod(SpNameClassNameDict);
        }
    }
}