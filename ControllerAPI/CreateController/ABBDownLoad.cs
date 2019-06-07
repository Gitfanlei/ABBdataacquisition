using ABB.Robotics.Controllers.RapidDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Robotics.Controllers;

namespace ControllerAPI
{
    class ABBDownLoad
    {
        // 定义相关属性成员：来实现对某些私有属性的访问和提取  与ABBCollecter中对应
        public string ID { set; get; }
        public string IP { set; get; }
        public string Name { set; get; }
        public DateTime TimeStamp { set; get; }
        public RobJoint PositionInfo { set; get; }
        public int speedRatio { set; get; }
        public string axisNum { set; get; }
        public string value { set; get; }
        public string level { set; get; }
        public string curUser { set; get; }
        public float Temperature { set; get; }
        public string contrllerName { set; get; }
        public string operationMode { set; get; }
        public string system { set; get; }
        public string text { set; get; }
        public int categoryId { set; get; }
    }
}
