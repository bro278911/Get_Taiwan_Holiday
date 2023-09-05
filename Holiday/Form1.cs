using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;
namespace Holiday
{
    public partial class Form1 : Form
    {
        public class Holiday_info//定義Json內部數據欄位名稱
        {
            public string date { get; set; }

            public string week { get; set; }

            public string isHoliday { get; set; }

            public string description { get; set; }
        }
        public Form1()
        {
            InitializeComponent();
            fill_listbox();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            obj_Clear();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(textBox1.Text);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            
            richTextBox1.AppendText(responseBody);
            //Holiday_info jsonObject = JsonConvert.DeserializeObject<Holiday_info>(responseBody);
            List<Holiday_info> myDeserializedObjList = (List<Holiday_info>)Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody, typeof(List<Holiday_info>));//數組解析Json
            //Console.WriteLine(myDeserializedObjList[0].isHoliday);//檢視Json檔內容
            
            for (int i = 0; i < myDeserializedObjList.Count; i++)
            {
                DateTime dt = DateTime.ParseExact(myDeserializedObjList[i].date, "yyyyMMdd", CultureInfo.InvariantCulture);
                string date = dt.ToString("yyyy年MM月dd日");
                richTextBox2.AppendText($"日期：{date}，是否是假日：{myDeserializedObjList[i].isHoliday}\n");
                //richTextBox2.AppendText('日期：' + myDeserializedObjList[i].date + '是否是假日：' + myDeserializedObjList[i].isHoliday + '\n');
            }
            //richTextBox2.Text = myDeserializedObjList[i].isHoliday;
            
        }
        private void obj_Clear()
        {
            richTextBox2.Clear();
            richTextBox1.Clear();
        }
        private void fill_textbox(string year)
        {
            string api = "https://cdn.jsdelivr.net/gh/ruyut/TaiwanCalendar@master/data/year.json";
            string newapi = api.ToString().Replace("year", year);
            textBox1.Text = newapi;
        }
        private void fill_listbox()
        {
            string currentYear = DateTime.Now.Year.ToString();
            //Console.WriteLine(currentYear);
            listBox1.Items.Clear();
            listBox1.Items.Add(int.Parse(currentYear) - 1);
            listBox1.Items.Add(currentYear);
            listBox1.Items.Add(int.Parse(currentYear) + 1);
            Console.WriteLine(int.Parse(currentYear) - 1);
            Console.WriteLine(int.Parse(currentYear) + 1);
            fill_textbox(currentYear);
        }
        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string year =listBox1.SelectedItem.ToString();
            fill_textbox(year);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
