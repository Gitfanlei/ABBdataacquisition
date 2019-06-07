using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using System;
using System.Timers; //����ɼ�ʱ����
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

        // ���� ABBCollector �� ������ �������˽�б���    ˽�б����ķ���  get set
        private static ABBCollector collector;

        // ����Timer �� ������ ʱ���� aTimer ����
        private static Timer aTimer;
        private static readonly int timerInterval = Convert.ToInt32(ConfigurationManager.AppSettings.Get("collectFrequnencyWithUnitMs"));

        [MTAThread]
        static void Main(string[] args)
        {
            SetUp();

            // �򿪶�̬ɨ��ӿ�

            Console.WriteLine("Press any key to terminate");
            Console.ReadKey();

            // �趨 SetUp֮���ʱ�� stop�����Լ�Dispose ������
            aTimer.Stop();
            aTimer.Dispose();
        }

        // ����һ����ʼ������������
        private static void SetUp()
        {
            // ���� collector ����ABBCollector �� ʵ��ɨ���
            collector = new ABBCollector();

            // ���� SetTimer
            SetTimer(timerInterval);
        }

        // ���� SetTimer ����
        private static void SetTimer(int interval)
        {
            aTimer = new Timer(interval);

            // ����ʱ�䵽������Ķ���
            aTimer.Elapsed += OnTimedEvent;

            // �����Զ����ú�ʹ��
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            ABBDownLoad abbDownload = new ABBDownLoad
            // abbDownload ������ ���壨����Դ���أ�
            {
                ID = collector.SystemID,
                IP = collector.SystemIP,
                Name = collector.SystemName,
                TimeStamp = DateTime.Now,
                PositionInfo = collector.PositionInfo
            };
            // �������������ʽ  JsonConvert ��ʾJSON ��ʽ��ת��
            string ABBPositionJson = JsonConvert.SerializeObject(abbDownload);
            Console.WriteLine(ABBPositionJson.ToString());

        }

    }

        

 
}
