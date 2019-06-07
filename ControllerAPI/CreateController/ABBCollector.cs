using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;


using System.Configuration;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using System.Windows.Forms;
using ABB.Robotics.Controllers.MotionDomain;
using ABB.Robotics.Controllers.EventLogDomain;


namespace ControllerAPI
{
    class ABBCollector
    {
        // 声明 控制器 与控制器信息的变量   信息类数据最好 修饰为私有变量  私有变量仅仅对 ABBCollector类公开
        public Controller ABBController;
        private ControllerInfo ABBControllerinfo;

        private EventArgs EventArgs;
        private EventLogMessageCollection eventLogMessages;
        private MessageWrittenEventArgs MessageWrittenEventArgs;
        private EventLog EventLog;
        private EventLogCategory EventLogCategory;
        private ControllerEventArgs ControllerEventArgs;
        private TextRange TextRange;
        private ProgramPosition ProgramPosition;




        // 创建一个布尔值 用以选择 扫描端口的类型  appsetting 很重要 可以设置 配置的信息
        private bool chooseSocket = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("chooseSocket"));

        public ABBCollector()
        {
            DynamicCreation();
        }

        // 声明一个私有变量 动态创建扫描接口   创建与控制器的连接
        public void DynamicCreation()
        {
            //  建立一个扫描接口的 引用变量 networkScanner
            NetworkScanner networkScanner = new NetworkScanner();

            // 扫描接口 虚拟接口还是实际接口  条件运算符
            ControllerInfo[] controllers = networkScanner.GetControllers(chooseSocket ? NetworkScannerSearchCriterias.Virtual : NetworkScannerSearchCriterias.Real);

            // 发现控制器后
            if (controllers.Length > 0)
            {
                // 确认controller 包含哪些信息
                Console.WriteLine(controllers);

                // 提取controller中的第默认序列第一的信息
                ABBControllerinfo = controllers[0];

                Console.WriteLine(ABBControllerinfo);


                // 根据控制器的信息创建一个实例   并且可以输出  其基本信息
                ABBController = ControllerFactory.CreateFrom(ABBControllerinfo);
                Console.WriteLine($"Found one ABB.System Name is:{SystemName} System ID is:{SystemID} System IP is:{SystemIP}");
                // $ 起到一个占位符的作用内容包含在 {} 中，可以用于获取{}中对应内容的信息
            }
            else
            {
                MessageBox.Show("No ABB Robot Found.");
            }
                        

        }

       
        // 建立一些变量 来表示controller中的信息(私有变量）——变量值的获取和设置方法
        public string SystemName
        {
            get
            {
                return ABBController.SystemName;
            }
        }

        public string SystemID
        {
            get
            {
                return ABBController.SystemId.ToString();
            }
        }

        public string SystemIP
        {
            get
            {
                return ABBController.IPAddress.ToString();
            }
        }

        public string SystemDateTime
        {
            get
            {
                return ABBController.DateTime.ToString();
            }
        }

        public RobJoint PositionInfo
        {
            get
            {
                return ABBController.MotionSystem.MechanicalUnits[0].GetPosition().RobAx;  // 轴位置
            }
        }

        public string AxisNum
        {
            get
            {
                return ABBController.MotionSystem.MechanicalUnits[0].NumberOfAxes.ToString();  // 轴数量
            }
        }

        public int SpeedRatio
        {
            get
            {
                return ABBController.MotionSystem.SpeedRatio;   // 速度比

            }
        }

        public string Value
        {
            get
            {
                return ABBController.Rapid.ToString(); // RAPID任务 数据

            }
        }

        public string runLevel
        {
            get
            {
                return ABBControllerinfo.RunLevel.ToString();  // 运行级别
            }
        }

        public string CurUser
        {
            get
            {
                return UserInfo.DefaultUser.Name;  //获取用户名
            }
        }



        public float Temperature
        {
            get
            {
                return ABBController.MainComputerServiceInfo.Temperature; // 主电脑服务器 温度
            }
        }

        public string ControllerName
        {
            get
            {
                return ABBController.Name;  // 控制器名称
            }
        }

        public string OperationMode
        {
            get
            {
                return ABBController.OperatingMode.ToString();   // 当前操作模式           
            }
        }


        public string Text
        {
            get
            {
                return null;
            }
        }

        public int categoryId
        {
            get
            {
                return 1;  // 
            }
        }

        //private void Eventlog(Controller controller)
        //{
        //    List<System.String> listEvent = new List<System.String>();
        //    EventLogMessageCollection eventLogMessages = null;
        //    EventLogCategory[] category = controller.EventLog.GetCategories();
        //    foreach (EventLogCategory e in category)
        //    {
        //        if (e.Type.ToString() != null)
        //        {
        //            eventLogMessages = e.Messages;
        //            MessageBox.Show(eventLogMessages.ToString());
        //        }
        //    }

        //}

        // 报警信息

        // 文本信息



        //ABBController.MotionSystem.MechanicalUnits[0].GetMechanicalUnitStatus().ToString();  // 系统机械单元状态  同步 或 非同步
    }

}

