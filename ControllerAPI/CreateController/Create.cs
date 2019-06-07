using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using System;
using System.Timers; //定义采集时间间隔
using System.Configuration;
using Newtonsoft.Json;


namespace ControllerAPI
{
    /// <summary>
    /// Creates a controller instance from various sources.
    /// </summary>
    class Create
    {
        static string SystemID = "";

        // 调用 ABBCollector 类 并建立 归属其的私有变量    私有变量的访问  get set
        private static ABBCollector collector;

        // 调用Timer 类 并建立 时间间隔 aTimer 变量
        private static Timer aTimer;
        private static readonly int timerInterval = Convert.ToInt32(ConfigurationManager.AppSettings.Get("collectFrequnencyWithUnitMs"));

        [MTAThread]
        static void Main(string[] args)
        {
            SetUp();

            // 打开动态扫描接口

            Console.WriteLine("Press any key to terminate");
            Console.ReadKey();

            // 设定 SetUp之后的时间 stop动作以及Dispose 动作；
            aTimer.Stop();
            aTimer.Dispose();
        }

        // 建立一个初始化程序建立函数
        private static void SetUp()
        {
            // 启动 collector 调用ABBCollector 类 实现扫描等
            collector = new ABBCollector();

            // 启动 SetTimer
            SetTimer(timerInterval);
        }

        // 建立 SetTimer 函数
        private static void SetTimer(int interval)
        {
            aTimer = new Timer(interval);

            // 建立时间到达后发生的动作
            aTimer.Elapsed += OnTimedEvent;

            // 设置自动重置和使能
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            ABBDownLoad abbDownload = new ABBDownLoad
            // abbDownload 的属性 定义（程序源加载）
            {
                ID = collector.SystemID,
                IP = collector.SystemIP,
                Name = collector.SystemName,
                TimeStamp = DateTime.Now,
                PositionInfo = collector.PositionInfo
            };
            // 定义数据输出格式  JsonConvert 表示JSON 格式的转换
            string ABBPositionJson = JsonConvert.SerializeObject(abbDownload);
            Console.WriteLine(ABBPositionJson.ToString());

        }

    }

        

 
}
