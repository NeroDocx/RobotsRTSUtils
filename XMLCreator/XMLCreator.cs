// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Collections.Generic;
using RobotsRTS.Xml;
using RobotsRTS.Xml.SaveAndReadXmlData;
using System.Windows.Forms;
using System.IO;
namespace XMLCreator
{
    public class XMLCreator
    {
        private const string BDAdress = "127.0.0.1";
        private const string BDLogin = "postgres";
        private const string BDPassword = "admin_postgres";
        private const string BDName = "robots_rts";

        [STAThread]
        static void Main(string[] args)
        {

            Dictionary<int, object> dic = new Dictionary<int, object>();
            dic[1] = null;
            Console.WriteLine(dic.Count);

            DataXmlCreator xmlCreator = new DataXmlCreator(BDAdress, BDLogin, BDPassword, BDName);
            DataXml dataXml = null;
            if (!xmlCreator.CreateDataXml(out dataXml))
            {
                Console.WriteLine("Fail create dataXml!!!");
                Console.ReadKey();
                return;
            }

            using (FolderBrowserDialog sFile = new FolderBrowserDialog())
            {
                DialogResult result = sFile.ShowDialog();
                switch (result)
                {
                    case DialogResult.OK:
                    case DialogResult.Yes:
                        string path = Path.Combine(sFile.SelectedPath, "Data.xml");
                        string resultMsg = string.Empty;
                        if (SaveAndReadXmlData.SaveXmlData(path, dataXml, out resultMsg))
                        {
                            Console.WriteLine("Create dataXml!!!");
                        }
                        else
                        {
                            Console.WriteLine("Fail save dataXml!!!");
                            Console.WriteLine("resultMsg");
                        }
                        break;

                }
            }
            Console.WriteLine(string.Empty);
            Console.WriteLine("End...");
            Console.ReadKey();

        }
    }
}
