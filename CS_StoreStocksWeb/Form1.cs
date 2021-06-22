using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//https://www.codeproject.com/Tips/1058700/Embedding-Chrome-in-your-Csharp-App-using-CefSharp
using CefSharp;
using CefSharp.WinForms;
using System.IO;
using System.Collections;

namespace CS_StoreStocksWeb
{
    public partial class Form1 : Form
    {
        public int m_intListIndex = 0;
        public ArrayList m_ALUrl = new ArrayList();
        public ArrayList m_ALList = new ArrayList();
        public ChromiumWebBrowser browser01;
        public ChromiumWebBrowser browser02;
        public ChromiumWebBrowser browser03;
        public void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            browser01 = new ChromiumWebBrowser();
            browser02 = new ChromiumWebBrowser();
            browser03 = new ChromiumWebBrowser();
            
            tabPage1.Controls.Add(browser01);
            tabPage2.Controls.Add(browser02);
            tabPage3.Controls.Add(browser03);
            browser01.Dock = DockStyle.Fill;
            browser02.Dock = DockStyle.Fill;
            browser03.Dock = DockStyle.Fill;
        }

        public Form1()
        {
            InitializeComponent();
            InitBrowser();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader sr01 = new StreamReader("List.txt");
            StreamReader sr02 = new StreamReader("Url.txt");
            while (!sr01.EndOfStream)// 每次讀取一行，直到檔尾
            {
                string line = sr01.ReadLine();// 讀取文字到 line 變數
                m_ALList.Add(line);
            }
            while (!sr02.EndOfStream)// 每次讀取一行，直到檔尾
            {
                string line = sr02.ReadLine();// 讀取文字到 line 變數
                m_ALUrl.Add(line);
            }
            sr01.Close();// 關閉串流
            sr02.Close();// 關閉串流
            if(m_ALList.Count>0)
            {
                textBox1.Text = m_ALList[m_intListIndex].ToString();
                label2.Text = String.Format("{0}/{1}", m_intListIndex+1, m_ALList.Count);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_intListIndex--;
            if(m_intListIndex<0)
            {
                m_intListIndex = 0;
            }
            label2.Text = String.Format("{0}/{1}", m_intListIndex + 1, m_ALList.Count);
            textBox1.Text = m_ALList[m_intListIndex].ToString();
            ShowKDChart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_intListIndex++;
            if (m_intListIndex >= m_ALList.Count)
            {
                m_intListIndex = m_ALList.Count-1;
            }
            label2.Text = String.Format("{0}/{1}", m_intListIndex + 1, m_ALList.Count);
            textBox1.Text = m_ALList[m_intListIndex].ToString();
            ShowKDChart();
        }

        public void ShowKDChart()
        {
            String StrUrl = String.Format(m_ALUrl[0].ToString(), textBox1.Text);
            browser01.Load(StrUrl);
            tabControl1.SelectedTab = tabPage1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowKDChart();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String StrUrl = String.Format(m_ALUrl[1].ToString(), textBox1.Text);
            browser02.Load(StrUrl);
            tabControl1.SelectedTab = tabPage2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String StrUrl = String.Format(m_ALUrl[2].ToString(), textBox1.Text);
            browser03.Load(StrUrl);
            tabControl1.SelectedTab = tabPage3;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowKDChart();
            timer1.Enabled = false;
        }
    }
}
