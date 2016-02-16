using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace AutoUpdate
{
    public partial class Form1 : Form
    {
        ArrayList fileList = new ArrayList();
        string updateVerzija;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Mobilis - AutoUpdate (" + Application.ProductVersion + ")";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log("Mobilis - AutoUpdater", false);
            Log("Copyright ©  2008", false);
            timer1.Enabled = true;
        }

        private bool GetFile(string netUrl, string localPath)
        {
            bool rez = false;
            HttpWebRequest webreq;
            HttpWebResponse webresp;

            FileStream fs;
            BinaryReader r;
            BinaryWriter w = null;


            //save this in a file with the same name	
            try
            {
                webreq = (HttpWebRequest)WebRequest.Create(netUrl);
                webresp = (HttpWebResponse)webreq.GetResponse();
                if (webresp.ResponseUri.AbsoluteUri == netUrl)
                {
                    //open file for writing
                    fs = new FileStream(@localPath, FileMode.OpenOrCreate, FileAccess.Write);

                    rez = true;
                    r = new BinaryReader(webresp.GetResponseStream());
                    w = new BinaryWriter(fs);
                    while (true)
                        w.Write(r.ReadByte());


                }
            }
            catch (System.IO.EndOfStreamException)
            {
                w.Flush();
                w.Close();
            }
            catch
            {
                rez = false;
            }
            return rez;
        }
        private void LoadOldList()
        {
            string putanja = Application.StartupPath;
            if (!putanja.EndsWith("\\"))
                putanja += "\\";
            putanja += "upd.xml";
            if (!File.Exists(putanja))
                return;

            XmlTextReader reader = new XmlTextReader(putanja);
            string ime = "";
            FileStruct item= null;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Datoteka")
                {
                    ime = reader.GetAttribute("ime").ToLower ();
                    for (int i = 0; i < fileList.Count; i++)
                    {
                        item = (FileStruct)fileList[i];
                        if (item.ime.ToLower() == ime)
                        {
                            item.verLocal = reader.GetAttribute("ver");
                            break;
                        }
                    }
                }
            }
            reader.Close();
        }
        private void LoadNewList()
        {
            string putanja = Application.StartupPath;
            if (!putanja.EndsWith("\\"))
                putanja += "\\";
            putanja += "lista.xml";
            if (!File.Exists(putanja))
                return;

            XmlTextReader reader = new XmlTextReader(putanja);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Update")
                {
                    updateVerzija = reader.GetAttribute("verzija");
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Datoteka")
                {
                    fileList.Add(new FileStruct(reader.GetAttribute("ime"), reader.GetAttribute("local"), reader.GetAttribute("url"), reader.GetAttribute("ver"), reader.GetAttribute("md5")));
                }
            }
            reader.Close();
        }
        private bool FillFileList()
        {
            bool rez = false;
            string putanja = Application.StartupPath;
            if (!putanja.EndsWith("\\"))
                putanja += "\\";
            putanja += "lista.xml";
            if (File.Exists(putanja))
                File.Delete(putanja);
            Log(" --Provjeravam dostupna preuzimanja--", false);
            if (GetFile("http://www.mobilis.hr/_program/mobilis/upd.xml", putanja))
            {
                LoadNewList();
                LoadOldList();
                foreach (FileStruct item in fileList)
                {
                    string verLocal;
                    if (item.ime.ToLower() == "autoupdate.exe")
                        verLocal = Application.ProductVersion;
                    else
                        verLocal = item.verLocal;
                    if (UspoediVerzije(verLocal, item.verNet))
                    {
                        rez = true;
                        Log(item.ime + " - dostupna nova verzija (" + item.verLocal + ">" + item.verNet + ")", false);
                    }
                    else
                        Log(item.ime + " - OK ", false);
                }
            }
            else
            {
                rez = false;
                Log(" --Server preuzimanja nedostupan--", false);
            }

            return rez;
        }
        private string MD5Hash(string sFilePath)
        {
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5Provider
                = new System.Security.Cryptography.MD5CryptoServiceProvider();
                FileStream fs
                = new FileStream(sFilePath, FileMode.Open, FileAccess.Read);
                Byte[] hashCode
                = md5Provider.ComputeHash(fs);

                string ret = "";

                foreach (byte a in hashCode)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }

                fs.Close();
                return ret;
            }
            catch 
            {
                MessageBox.Show("md5", "Greška");
                return "";
            }
        }
        private bool UspoediVerzije(string local, string net)
        {
            try
            {
                string[] localA = local.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                string[] netA = net.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                int iLocal;
                int iNet;
                for (int i = 0; i < localA.Length; i++)
                {
                    int.TryParse(localA[i], out iLocal);
                    int.TryParse(netA[i], out iNet);
                    if (iNet > iLocal)
                        return true;
                    else if (iNet < iLocal)
                        return false;
                }
            }
            catch { }

            return false;
        }
        private void Log(string data, bool append)
        {
            if (append)
                lsbLog.Items[lsbLog.Items.Count - 1] = lsbLog.Items[lsbLog.Items.Count - 1] + data;
            else
                lsbLog.Items.Add(data);

            lsbLog.SelectedIndex = lsbLog.Items.Count - 1;
        }
        private void WriteXML(string putanja)
        {
            const string format = "<Datoteka ime=\"{0}\" local=\"\" url=\"\" ver=\"{1}\" md5=\"\" />";
            StreamWriter writer = new StreamWriter(putanja);
            writer .WriteLine ("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            writer .WriteLine ("<Update verzija=\"{0}\">",updateVerzija );
            foreach (FileStruct file in fileList)
            {
                string ver;
                if (file.Preuzeto)
                    ver = file.verNet;
                else
                    ver = file.verLocal;
                writer.WriteLine(format, file.ime, ver);
            }
            writer .WriteLine ("</Update>");
            writer.Flush();
            writer.Close();
        }
        private void UcitajLog(string putanja)
        {
            if (File.Exists(putanja))
            {
                StreamReader reader = new StreamReader(putanja);
                txtLog.Text = reader.ReadToEnd();
                reader.Close();
            }
        }
        private void IzvrsiUpdate()
        {
            int rez = 0;
            FileStruct item= null;
            string localPath;
            string putanja = Application.StartupPath;
            if (!putanja.EndsWith("\\"))
                putanja += "\\";
            Log(" -- Preuzimam --", false);
            if (GetFile("http://www.mobilis.hr/_program/mobilis/log.txt", putanja + "log.txt"))
                UcitajLog(putanja + "log.txt");
            for (int i = 0; i < fileList.Count; i++)
            {
                item = (FileStruct)fileList[i];
                if (item.ime.ToLower() == "autoupdate.exe")
                {
                    if (UspoediVerzije(Application.ProductVersion, item.verNet))
                    {
                        Log("Nova verzija AutoUpdate izasla", false);
                        MessageBox.Show("Nova verzija updatera izašla", "AutoUpdater");
                        // downloadiraj novi updater
                        localPath = putanja + "AutoUpdateUPD.exe";
                        if (File.Exists(putanja))
                            File.Delete(putanja);
                        Log("Preuzimam..." + item.ime, false);
                        GetFile(item.URL, localPath);
                        Log("Preuzimanje gotovo", false);
                        Process process = Process.Start(localPath);
                        if (process != null)
                            Application.Exit();
                        return;
                    }
                }
                else
                {
                    // ostali fajlovi
                    if (item.localPath == "")
                        localPath = putanja + item.ime;
                    else
                        localPath = item.verLocal + item.ime;
                    if (UspoediVerzije(item.verLocal, item.verNet))
                    {
                        Log("Preuzimam... " + item.ime + " (" + item.verLocal + ">" + item.verNet + ")", false);
                        item.Preuzeto = GetFile(item.URL, localPath);
                        if (item.Preuzeto)
                            Log("- Preuzeto", true);
                        else
                        {
                            rez++;
                            Log("- greška", true);
                        }
                    }
                }
            }
            if(File.Exists(putanja + "upd.xml"))
                File.Delete (putanja + "upd.xml");
            if (rez == 0)   // update uspjesan samo ako je skinio sve fajlove
            {
                Log(" --Preuzimanje uspješno--", false);
                WriteXML(putanja + "upd.xml");
            }
            else
                Log(" --Preuzimanje nije potpuno--", false);
            if (File.Exists(putanja + "lista.xml"))
                File.Delete(putanja + "lista.xml");
            btnAzuriraj.Text = "Gotovo";
            btnAzuriraj.Enabled = true;
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnAzuriraj_Click(object sender, EventArgs e)
        {
            string putanja = Application.StartupPath;
            if (!putanja.EndsWith("\\"))
                putanja += "\\";
            putanja += "Mobilis.exe";
            if (File.Exists(putanja))
                Process.Start(putanja);
            else
                MessageBox.Show("Mobilis.exe nije naðen", "AutoUpdate");
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (FillFileList())
                IzvrsiUpdate();
            else
                Log(" --Preuzimanje nije potrebno--", false);
        }
    }
    class FileStruct
    {
        public string ime;
        public string localPath;
        string urlPath;
        public string verNet;
        public string verLocal;
        public string md5;
        private bool preuzeto;
        public FileStruct(string ime, string localPath, string urlPath, string verNet, string md5)
        {
            this.ime = ime;
            this.localPath = localPath;
            this.urlPath = urlPath;
            this.verNet = verNet;
            this.md5 = md5;
            this.verLocal = "1.0.0.0";
        }
        public string URL
        {
            get
            {
                if (!urlPath.StartsWith("http://"))
                    return "http://www.mobilis.hr/_program/mobilis/" + urlPath;
                else
                    return urlPath;
            }
        }
        public bool Preuzeto
        {
            set { preuzeto = value; }
            get { return preuzeto; }
        }
     }
}