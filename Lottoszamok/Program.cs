using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottoszamok
{
    class Program
    {
        private static void ReadFile(String file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                sr.ReadLine();      // skip header
                HashSet<DateTime> datumok = new HashSet<DateTime>();
                while (!sr.EndOfStream)
                {
                    string[] lines = sr.ReadLine().Split(';');
                    datumok.Add(Convert.ToDateTime(lines[0]));
                }
            }
        }
        
        static void Main(string[] args)
        {
            #region 1.feladat
            Console.WriteLine("1. feladat:");
            String filePath = @"..\..\src\sorsolas.csv";
            ReadFile(filePath);
            #endregion
        }
    }
}
