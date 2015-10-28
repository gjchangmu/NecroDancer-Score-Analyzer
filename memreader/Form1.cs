using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace memreader
{
	public partial class Form1 : Form
	{
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		[DllImport("Kernel32.dll")]
		static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, ref int lpNumberOfBytesRead);
		IntPtr processHandle = IntPtr.Zero;
		IntPtr baseaddress;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Process[] Ps = Process.GetProcessesByName(tbProcess.Text);
			if (Ps.Count<Process>() == 0)
				return;
			//processHandle = OpenProcess(0x0010, false, Ps[0].Id);
			//if (processHandle == IntPtr.Zero)
			//	return;
			baseaddress = Ps[0].MainModule.BaseAddress;
			tbBase.Text = baseaddress.ToString("X");
		}

		private void tbProcess_TextChanged(object sender, EventArgs e)
		{
			Process[] Ps = Process.GetProcessesByName(tbProcess.Text);
			if (Ps.Count<Process>() == 0)
				return;
			//processHandle = OpenProcess(0x0010, false, Ps[0].Id);
			//if (processHandle == IntPtr.Zero)
			//	return;
			baseaddress = Ps[0].MainModule.BaseAddress;
			tbBase.Text = baseaddress.ToString("X");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Process[] Ps = Process.GetProcessesByName(tbProcess.Text);
			if (Ps.Count<Process>() == 0)
				return;
			baseaddress = Ps[0].MainModule.BaseAddress;
			tbBase.Text = baseaddress.ToString("X");
			int address = int.Parse(tbAddress.Text, System.Globalization.NumberStyles.HexNumber);
			int length = int.Parse(tbLength.Text, System.Globalization.NumberStyles.HexNumber);
			if (length == 0)
				return;
			IntPtr readaddress;
			if (cbRelative.Checked)
				readaddress = baseaddress + address;
			else
				readaddress = (IntPtr)address;
				

			processHandle = OpenProcess(0x0010, false, Ps[0].Id);
			if (processHandle == IntPtr.Zero)
				return;
			
            byte[] rbuff = new byte[length];
			int rn = 0;
			bool ret = ReadProcessMemory(processHandle, readaddress, rbuff, length, ref rn);
			if (ret)
			{
				FileStream fs = new FileStream("mem.bin", FileMode.Create);
				fs.Write(rbuff, 0, length);
				fs.Close();
			}
			else
			{
				MessageBox.Show("Failed");
			}
		}
	}
}
