using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 建立接口扫描类
            NetworkScanner networkScanner = new NetworkScanner();
            // 接口扫描
            networkScanner.Scan();
            // 建立控制器扫描接口变量
            ControllerInfoCollection controllers = networkScanner.Controllers;
            // 对于扫描到的每一个控制器执行
            foreach(ControllerInfo controller in controllers)
            {
                // listviewitem 是建立的控件中的单元 以系统SystemName为keyword 建立信息类
                ListViewItem item = new ListViewItem(controller.SystemName);
                //获取 IP version 信息
                item.SubItems.Add(controller.IPAddress.ToString());
                item.SubItems.Add(controller.Version.ToString());
                item.Tag = controller;
                // 指明 item 归属的控件
                this.lstControllersView.Items.Add(item);

            }
        }


        // linked 两个不同的控件对象链接在一起
        // button类 doubleClick事件触发
        private void lstControllersView_DoubleClick(object sender, EventArgs e)
        {
            // 建立表格view项目类    依赖于 lstcontrollerView 第一项
            ListViewItem itemView = lstControllersView.SelectedItems[0];
            // 判断项目视图
            if(itemView.Tag !=null)
            {

                ControllerInfo controllerInfo = (ControllerInfo)itemView.Tag;

                Controller ctrl = ControllerFactory.CreateFrom(controllerInfo);
                ctrl.Logon(UserInfo.DefaultUser);

                // 指明 item 归属的控件
                ListViewItem item = new ListViewItem(ctrl.RobotWare.ToString() + "" + ctrl.State.ToString() + "" + ctrl.OperatingMode.ToString());
                this.listOutput.Items.Add(item);

                ctrl.Logoff();
                ctrl.Dispose();

            }
        }

        private void listOutput_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
