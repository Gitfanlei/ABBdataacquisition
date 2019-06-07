using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using System.Configuration;
using System.Timers;
using Newtonsoft.Json;
using ABB.Robotics.Controllers.Configuration;
using System.IO;
using ABB.Robotics.Controllers.EventLogDomain;


namespace ControllerAPI
{
    public partial class Form1 : Form
    {

        // 调用 ABBCollector 类 并建立 归属其的私有变量    私有变量的访问  get set
        private static ABBCollector collector;
        private TextRange TextRange;
        private TextReader TextReader;
        private RapidData RapidData;
        private ProgramPositionEventArgs ProgramPositionEventArgs;
        private ProgramPosition programPosition;


        public Form1()
        {
            InitializeComponent();
            timer2.Enabled = true;
            timer2.Start();

        }

        // kong jian
        public void Form1_Load(object sender, EventArgs e)
        {
            // 调用 ABBCollector 进行接口扫描
            collector = new ABBCollector();

            NetworkScanner networkScanner = new NetworkScanner();
            networkScanner.Scan();
            ControllerInfoCollection controllers = networkScanner.Controllers;
            // 对于扫描到的每一个控制器执行
            foreach (ControllerInfo controller in controllers)
            {
                // listviewitem 是建立的控件中的单元 以系统SystemName为keyword 建立信息类
                ListViewItem item = new ListViewItem(controller.SystemName);
                //获取 IP version 信息
                item.SubItems.Add(controller.IPAddress.ToString());  // ip
                item.SubItems.Add(controller.Version.ToString()); // version
                item.Tag = controller;
                // 指明 item 归属的控件
                this.listControllerView.Items.Add(item);

            }


        }

        private void listControllerView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 建立表格view项目类
            ListViewItem itemView = listControllerView.SelectedItems[0];
            // 判断项目视图
            if (itemView.Tag != null)
            {
                ControllerInfo controllerInfo = (ControllerInfo)itemView.Tag;

                Controller ctrl = ControllerFactory.CreateFrom(controllerInfo);
                ctrl.Logon(UserInfo.DefaultUser);

                // 指明 item 归属的控件
                ListViewItem item = new ListViewItem(ctrl.RobotWare.ToString() + "" + ctrl.State.ToString() + "" + ctrl.OperatingMode.ToString());
                this.listOutput.Items.Add(item);
            }
        }


        // 日志文件的读取（报警信息）
        public void EventLog()
        {

            //NetworkScanner NetS = new NetworkScanner();
            //Controller controller = NetS.GetControllers(NetworkScannerSearchCriterias.Virtual);


            // 管家在于要引入 当前 controller 实例
            EventLogCategory[] eventLogCategories = collector.ABBController.EventLog.GetCategories();

            foreach (EventLogCategory cats in eventLogCategories)
            {

                foreach (EventLogMessage mesg in cats.Messages)
                {
                    //if (mesg.Type.ToString() == "Warning")
                    //{
                        ListViewItem item = new ListViewItem(mesg.SequenceNumber.ToString());
                        item.SubItems.Add(mesg.Number.ToString());
                        item.SubItems.Add(mesg.Title.ToString());
                        item.SubItems.Add(mesg.Type.ToString());
                        item.SubItems.Add(mesg.Timestamp.ToString());
                        this.EvenloglistView.Items.Add(item);
                    //}
                }
            }
        }

        public void GetTime()
        {
            CurTimeLabel.Text = DateTime.Now.ToString();
        }

        public Int32 num;
        List<Int32> intList = new List<Int32>();
        public Int32[] numList;
        public float MinNumCode;
        public float MaxNumCode;
        public float processing;
        
        // MotionPointer / ProgramPointer
        public void Textrange()
        {
            ABB.Robotics.Controllers.RapidDomain.Task[] tasks = collector.ABBController.Rapid.GetTasks();  // 获取当前任务 ****

            foreach (ABB.Robotics.Controllers.RapidDomain.Task task in tasks)
            {
                task.Motion.ToString();  // 验证是否获取了当前运动   ture 则获取成功*****
                task.Cycle.ToString();  // 获取当前 程序的执行次数

                task.ExecutionStatus.ToString();  // 获取当前程序的执行状态 *****
                statusTextBox.Text = task.ExecutionStatus.ToString();

                //MessageBox.Show("");

                ProgramPosition motion = task.MotionPointer;  // 获取当权运动指针的位置 ******
                num = motion.Range.Begin.Row;      // 获取当前指针所在行数
                if (num != 0)
                {
                    intList.Add(num);
                    int[] strArray = intList.ToArray();
                }

                MinNumCode = intList.Min();
                MaxNumCode = intList.Max();

                if (MaxNumCode != MinNumCode)
                {
                    processing = ((num - MinNumCode) / (MaxNumCode - MinNumCode)) * 100;
                    ProcessingtextBox.Text = processing.ToString("0.00") + "%";
                    Console.WriteLine($"{num},{MinNumCode},{MaxNumCode}");

                }
                else if (processing.ToString("0.00") == "100.00")
                {
                    ProcessingtextBox.Text = 0.00 + "%";
                }
                else if (MinNumCode == 0)
                {
                    ProcessingtextBox.Text = 0.00 + "%";
                                       
                }

                Console.WriteLine($"{num},{MinNumCode},{MaxNumCode}");
                // 当前运动程序的相关参数    补充
                motion.Range.End.Row.ToString();
                motion.Routine.ToString();
                motion.Module.ToString();

                //MessageBox.Show(motion.Range.Begin.Row.ToString());

                ProgramPosition programPosition = task.ProgramPointer; // 获取程序指针当前的位置 *****

                programPosition.Module.ToString();  // 获取当前运行的模块  ****
                programPosition.Routine.ToString(); //获取当前程序事务(程序)的位置  ****

                programPosition.Range.End.Column.ToString();  // 当前文本范围  列 与 行
                programPosition.Range.Begin.Row.ToString(); // 获取当前 程序开始的行
                programPosition.Range.End.Row.ToString(); //获取当前 程序结束的行

            }

        }

        // 点击button 开始数据采集
        private void Startbutton_Click(object sender, EventArgs e)
        {
            SetUp();

            // 打开动态扫描接口

            Console.WriteLine("Start collecting the information");

            timer1.Enabled = true;
            timer1.Start();
        }




        // 建立一个初始化程序建立函数
        private static void SetUp()
        {
            // 启动 collector 调用ABBCollector 类 实现扫描等
            //collector = new ABBCollector();
        }


        //public void OnTimedEvent()
        //{

        //    ABBDownLoad abbDownload = new ABBDownLoad
        //    // abbDownload 的属性 定义（程序源加载）
        //    {
        //        ID = collector.SystemID,
        //        IP = collector.SystemIP,
        //        Name = collector.SystemName,
        //        TimeStamp = DateTime.Now,
        //        PositionInfo = collector.PositionInfo
        //    };
        //    // 定义数据输出格式  JsonConvert 表示JSON 格式的转换
        //    string ABBPositionJson = JsonConvert.SerializeObject(abbDownload);

        //    //Console.WriteLine(abbDownload.PositionInfo.ToString());
        //    Console.WriteLine(ABBPositionJson.ToString());

        //}

        //实时轴数据



        public void DataRead()
        {

            ABBDownLoad abbPositionInfo = new ABBDownLoad
            {
                ID = collector.SystemID,
                IP = collector.SystemIP,
                Name = collector.SystemName,
                PositionInfo = collector.PositionInfo,
                TimeStamp = DateTime.Now,
                axisNum = collector.AxisNum,
                speedRatio = collector.SpeedRatio,
                value = collector.Value,
                level = collector.runLevel,
                curUser = collector.CurUser,
                Temperature = collector.Temperature,
                operationMode = collector.OperationMode,
                contrllerName = collector.ControllerName,
                text = collector.Text,
                categoryId = collector.categoryId

            };

            var AxisData = abbPositionInfo.PositionInfo;
            label7.Text = AxisData.Rax_1.ToString();
            label8.Text = AxisData.Rax_2.ToString();
            label9.Text = AxisData.Rax_3.ToString();
            label10.Text = AxisData.Rax_4.ToString();
            label11.Text = AxisData.Rax_5.ToString();
            label12.Text = AxisData.Rax_6.ToString();

            //MessageBox.Show(abbPositionInfo.text);

            //格式转换 json
            string ABBPositioninfoJson = JsonConvert.SerializeObject(abbPositionInfo);


            FileStream datafile = new FileStream($"../DataSet/{abbPositionInfo.Name}.txt", FileMode.Append, FileAccess.Write);
            StreamWriter datawrite = new StreamWriter(datafile, System.Text.Encoding.Default);
            datawrite.WriteLine($"{abbPositionInfo.Name}:" + abbPositionInfo.PositionInfo);
            datawrite.Flush(); // 缓存清理

            datafile.Close();
            datafile.Dispose();

            //Console.WriteLine(AxisData);     终端输出

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DataRead();
            EventLog();
            Textrange();
            

            //OnTimedEvent();

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            GetTime();
        }


        // 解决线程问题  ???
        private void Stopbutton_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            EndUp();
        }

        private static void EndUp()
        {
            MessageBox.Show("All processing had Exited!");
            Application.ExitThread();
            Application.Exit();

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        
    }
}
