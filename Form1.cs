using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PVZ
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, uint lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        private const uint PROCESS_ALL_ACCESS = 0x1F0FFF;

        // 1.0.0.1051 英文原版 正确地址
        private const uint ADDR_SUN = 0x0041E7D8;
        private const uint ADDR_CD = 0x0041484C;
        private const uint ADDR_PLANT_HEALTH = 0x0041593C;
        private const uint ADDR_COIN = 0x004184E0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Enabled = false;
        }

        private bool WriteMemory(uint address, byte[] data, int size)
        {
            try
            {
                IntPtr hWnd = FindWindow("MainWindow", "Plants vs. Zombies");
                if (hWnd == IntPtr.Zero) return false;

                uint pid;
                GetWindowThreadProcessId(hWnd, out pid);

                IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
                if (hProcess == IntPtr.Zero) return false;

                int written;
                bool ok = WriteProcessMemory(hProcess, address, data, size, out written);

                CloseHandle(hProcess);
                return ok;
            }
            catch
            {
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 无限阳光（必须持续写）
            if (inf_sun.Checked)
            {
                byte[] sun = BitConverter.GetBytes(9990);
                WriteMemory(ADDR_SUN, sun, 4);
            }

            // 无冷却
            if (no_cd.Checked)
            {
                byte[] cd = BitConverter.GetBytes(0);
                WriteMemory(ADDR_CD, cd, 4);
            }
        }

        private void inf_sun_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = inf_sun.Checked || no_cd.Checked;
        }

        private void no_cd_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = inf_sun.Checked || no_cd.Checked;
        }

        // 植物无敌（安全写入，绝不崩溃）
        private void plant_inf_health_CheckedChanged(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            data[0] = plant_inf_health.Checked ? (byte)1 : (byte)0;
            WriteMemory(ADDR_PLANT_HEALTH, data, 1);
        }

        // 金币
        private void inf_coin_Click(object sender, EventArgs e)
        {
            byte[] coin = BitConverter.GetBytes(999999);
            bool ok = WriteMemory(ADDR_COIN, coin, 4);
            if (ok) MessageBox.Show("金币修改成功！");
        }
    }
}