using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Tulpep.NotificationWindow;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace getTestForm
{
    public partial class Form1 : Form
    {

        public static McqNotification staticMcq = null;
        public static SubjectiveNotification staticSubjective = null;
        public static QuestionView staticQuestionView = null;

        public static DummyForm staticDummy = new DummyForm();

        public Form1() { 
            InitializeComponent();

            var ieVersion = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer").GetValue("svcUpdateVersion");
            Console.Out.WriteLine(ieVersion);
            string Root = "HKEY_CURRENT_USER\\";
            string key = "Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION";
            var currentSetting = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(key).GetValue(Assembly.GetExecutingAssembly().FullName + ".exe");

            Console.Out.WriteLine(Assembly.GetExecutingAssembly().FullName);

            Microsoft.Win32.Registry.SetValue(Root + key, "getTestForm" + ".exe", 11001);
            Microsoft.Win32.Registry.SetValue(Root + key, "getTestForm" + ".vshost.exe", 11001);

            notifyIcon1.Icon = null;
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = false;
            this.Hide();
            /*
            DummyForm dummyForm = new DummyForm();
            FlashWindow.Start(dummyForm);
            dummyForm.WindowState = FormWindowState.Minimized;
            dummyForm.Show();
            */
            //notifyIcon1.Icon = Properties.Resources.icons8_survey_48_notif;
            
            DateTime timeNow = DateTime.Now;
            TimeSpan span = new TimeSpan(18,09,00);
            DateTime plannedTime = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day);
            plannedTime = plannedTime.Date + span;
            Console.Out.WriteLine("current time : " + timeNow);
            Console.Out.WriteLine("planned time : " + plannedTime);
            Console.Out.WriteLine((timeNow - plannedTime).TotalMilliseconds);
            
            if((int)(timeNow - plannedTime).TotalMilliseconds>0) {
                timer1.Interval = 86400000-(int)(timeNow - plannedTime).TotalMilliseconds;
                //Console.Out.WriteLine(timer1.Interval + " " + (int)(timeNow - plannedTime).TotalMilliseconds);
            } else {
                timer1.Interval = Math.Abs((int)(timeNow - plannedTime).TotalMilliseconds);
                //Console.Out.WriteLine(timer1.Interval + " " + (int)(timeNow - plannedTime).TotalMilliseconds);
            }
            // (int)(time - (new DateTime(time.Year, time.Month, time.Day)) + span).TotalMilliseconds;
            
            timer1.Start();
        }

        private async void button1_ClickAsync(object sender, EventArgs e) {
            Console.Out.WriteLine("button clicked");
        }


        private void timer1_Tick(object sender, EventArgs e) {
            DateTime timeNow = DateTime.Now;

            TimeSpan span = new TimeSpan(16, 6, 45);
            DateTime plannedTime = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day);
            plannedTime = plannedTime.Date + span;
            timer1.Interval = 86400000;// Math.Abs((int)(timeNow - plannedTime).TotalMilliseconds);
            Console.Out.WriteLine(timer1.Interval);

            notifyIcon1.Icon = Properties.Resources.icons8_survey_48_notif;
            notifyIcon1.Visible = true;

            QuestionView questionView = new QuestionView();
            staticQuestionView = questionView;
            questionView.StartPosition = FormStartPosition.Manual;
            questionView.Left = Screen.PrimaryScreen.WorkingArea.Width - 440;
            questionView.Top = 450;
            questionView.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            questionView.WindowState = FormWindowState.Normal;

            //questionView.webBrowser1.Url = new Uri("http://localhost:8085/questionView?" + (new Random()).NextDouble().ToString());
            //questionView.webBrowser1.Navigate(questionView.webBrowser1.Url);
            //questionView.webBrowser1.Refresh(WebBrowserRefreshOption.Completely);

            /*
            if (JObject.Parse(response)["question_type"].ToString().Equals("mcq")) {
                Console.Out.WriteLine("question type is mcq");
                McqNotification mcq = new McqNotification();
                staticMcq = mcq;
                staticSubjective = null;
                mcq.StartPosition = FormStartPosition.Manual;
                mcq.Left = Screen.PrimaryScreen.WorkingArea.Width - 400;
                mcq.Top = 500;
                if (JObject.Parse(response)["options"] != null) {
                    JArray options = (JArray)JObject.Parse(response)["options"];
                    Button[] buttonOptions = { mcq.buttonOption1, mcq.buttonOption2, mcq.buttonOption3 };
                    for (int i = 0; i < options.Count(); i++) {
                        buttonOptions[i].Text = options[i].ToString();
                        buttonOptions[i].Visible = true;
                    }
                }
                mcq.Text = String.Empty;
                mcq.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                mcq.questionLabel.Text = JObject.Parse(response)["question"].ToString();
                mcq.WindowState = FormWindowState.Normal;
                //mcq.Show();
                //FlashWindow.Start(mcq);
                //mcq.webBrowser1.Refresh();
                //while (!McqNotification.isLoaded) ;
                //notifyIcon1.Icon = Properties.Resources.icons8_survey_48_notif;
            } else {//if (JObject.Parse(response)["question_type"].ToString().Equals("subjective")) {
                Console.Out.WriteLine("question type is subjective");
                SubjectiveNotification subjective = new SubjectiveNotification();
                staticSubjective = subjective;
                staticMcq = null;
                subjective.StartPosition = FormStartPosition.Manual;
                subjective.Left = Screen.PrimaryScreen.WorkingArea.Width - 400;
                subjective.Top = 500;
                subjective.Text = String.Empty;
                subjective.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                subjective.questionLabel.Text = JObject.Parse(response)["question"].ToString();
                subjective.WindowState = FormWindowState.Normal;
                //subjective.Show();
                //FlashWindow.Start(subjective);
                //subjective.webBrowser1.Refresh();
                //while (!SubjectiveNotification.isLoaded) ;
                //notifyIcon1.Icon = Properties.Resources.icons8_survey_48_notif;
            }
            */



            /*
            MyWebRequest m = new MyWebRequest("https://script.google.com/macros/s/AKfycbzI5etjYBSe639KZxkPxDijeF8_tL3XkvfPgJ1zIl53M14y5Q0/exec", "GET");
            string response = m.GetResponse();
            Console.Out.WriteLine(response);

            if (JObject.Parse(response)["question_type"].ToString().Equals("mcq")) {
                Console.Out.WriteLine("question type is mcq");
                McqNotification mcq = new McqNotification();
                mcq.StartPosition = FormStartPosition.Manual;
                mcq.Left = Screen.PrimaryScreen.WorkingArea.Width - 400;
                mcq.Top = 50;
                if (JObject.Parse(response)["options"] != null) {
                    JArray options = (JArray)JObject.Parse(response)["options"];
                    Button[] buttonOptions = { mcq.buttonOption1, mcq.buttonOption2, mcq.buttonOption3 };
                    for (int i = 0; i < options.Count(); i++) {
                        buttonOptions[i].Text = options[i].ToString();
                        buttonOptions[i].Visible = true;
                    }
                }
                mcq.Text = String.Empty;
                mcq.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                mcq.questionLabel.Text = JObject.Parse(response)["question"].ToString();
                mcq.Show();
            } else if (JObject.Parse(response)["question_type"].ToString().Equals("subjective")) {
                Console.Out.WriteLine("question type is subjective");
                SubjectiveNotification subjective = new SubjectiveNotification();
                subjective.StartPosition = FormStartPosition.Manual;
                subjective.Left = Screen.PrimaryScreen.WorkingArea.Width - 400;
                subjective.Top = 50;
                subjective.Text = String.Empty;
                subjective.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                subjective.questionLabel.Text = JObject.Parse(response)["question"].ToString();
                subjective.Show();
            }
            */
        }

        private void popupNotifier1_Click(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            Console.Out.WriteLine("system trey icon double clicked");
        }

        private void notifyIcon1_Click(object sender, EventArgs e) {
            Console.Out.WriteLine("system trey icon clicked");

            if(notifyIcon1.Icon.Size == Properties.Resources.icons8_survey_48_notif.Size) {
                /*
                if (Form1.staticMcq != null) {
                    Console.Out.WriteLine("staticMcq is being used");
                    notifyIcon1.Icon = Properties.Resources.icons8_survey_48;
                    Form1.staticMcq.Show();
                } else if (Form1.staticSubjective != null) {
                    Console.Out.WriteLine("staticSubjective is being used");
                    notifyIcon1.Icon = Properties.Resources.icons8_survey_48;
                    Form1.staticSubjective.Show();
                }
                */
                Console.Out.WriteLine("staticQuestionView is being used");
                Form1.notifyIcon1.Icon = Properties.Resources.icons8_survey_48;
                Form1.staticQuestionView.Show();

                staticDummy.WindowState = FormWindowState.Minimized;
                FlashWindow.Stop(staticDummy);
                staticDummy.Hide();
            }

        }
    }
}
