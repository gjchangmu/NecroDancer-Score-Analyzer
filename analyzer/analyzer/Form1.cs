/*
NecroDancer Score Analyzor
by gjchangmu (gujianjiayi4@126.com)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		[DllImport("Kernel32.dll")]
		static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, ref int lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBufflpBuffer, int nSize, ref int lpNumberOfBytesWritten);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, int flAllocationType, int flProtect);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern int GetLastError();

		const int oldbase = 0x1210000;
		const int oldstorage = 0x0EF50000;

		List<int> codejumps;
		List<int> storagejumps;
		List<int> storageabs;
		List<int> randcalls;
		Process process;
		IntPtr processHandle = IntPtr.Zero;
		IntPtr baseaddress;
		IntPtr randaddress = IntPtr.Zero;
		IntPtr storage;
		int lastguessFoes = 0;

		public Form1()
		{
			InitializeComponent();

			// list here addresses of all jump command from inside the original game code to newmem
			codejumps = new List<int>();
			codejumps.Add(0X69C15);
			codejumps.Add(0x63B99);
			codejumps.Add(0X651AD);
			codejumps.Add(0X54CC1);
			codejumps.Add(0X54E33);
			codejumps.Add(0X55D9F);
			codejumps.Add(0X55EAC);
			codejumps.Add(0X55C37);

			// list here addresses of all jump command from newmem to inside the original game code
			storagejumps = new List<int>();
			storagejumps.Add(0x82);
			storagejumps.Add(0x1019);
			storagejumps.Add(0x2032);
			storagejumps.Add(0x3029);
			storagejumps.Add(0x4017);
			storagejumps.Add(0x501F);
			storagejumps.Add(0x6013);
			storagejumps.Add(0x6024);
			storagejumps.Add(0x70B0);
			storagejumps.Add(0x70BA);
			storagejumps.Add(0x70BF);
			storagejumps.Add(0x8041);


			// list here addresses of reference to inside the original game code in newmem
			storageabs = new List<int>();
			storageabs.Add(0x03);
			storageabs.Add(0x1D);
			storageabs.Add(0x23);
			storageabs.Add(0x29);
			storageabs.Add(0x2009);

			// list here all rand() calls
			randcalls = new List<int>();
			randcalls.Add(0x41);
			randcalls.Add(0x70);

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			bool ret;
			int rn = 0;

			if (processHandle == IntPtr.Zero)
			{
				Process[] Ps = Process.GetProcessesByName("Spelunky");
				if (Ps.Length == 0)
					return;
				System.Threading.Thread.Sleep(1000);
				process = Ps[0];
				processHandle = OpenProcess(0x0038, false, process.Id);
				if (processHandle == IntPtr.Zero)
					return;
				baseaddress = process.MainModule.BaseAddress;

				foreach (ProcessModule module in process.Modules)
				{
					Console.WriteLine(module.ModuleName);
					if (module.ModuleName.Contains("msvcrt.dll"))
					{
						randaddress = (IntPtr)((int)module.BaseAddress + 0x5c2e0);
						break;
					}
				}

				/*
				bool unknownversion = false;
				byte[] checkbuff = new byte[4];
				ret = ReadProcessMemory(processHandle, (IntPtr)((int)baseaddress + 0x10002), checkbuff, 4, ref rn);
				if (BitConverter.ToInt32(checkbuff, 0) != 0x44C7D08B)
					unknownversion = true;
				ret = ReadProcessMemory(processHandle, (IntPtr)((int)baseaddress + 0x1A14C5), checkbuff, 4, ref rn);
				if (BitConverter.ToInt32(checkbuff, 0) != 0x08418D50)
					unknownversion = true;
				ret = ReadProcessMemory(processHandle, (IntPtr)((int)baseaddress + 0x27DF92), checkbuff, 4, ref rn);
				if (BitConverter.ToInt32(checkbuff, 0) != 0x006F0063)
					unknownversion = true;
				if (unknownversion)
				{
					this.Text = "base:" + baseaddress.ToString("X");
					timer1.Enabled = false;
					MessageBox.Show("Unknown version of game detected. Please note that this version of analyzor may only supports steam v1.21 version of the game");
					return;
				}
				*/

				FileStream fs = new FileStream("newmem.bin", FileMode.Open);
				if (fs == null)
					return;
				int newmemlength = (int)fs.Length;
				byte[] wbuff = new byte[newmemlength];
				fs.Read(wbuff, 0, newmemlength);
				fs.Close();

				storage = VirtualAllocEx(processHandle, IntPtr.Zero, (uint)newmemlength, 0x3000, 0x40);
				if (storage == IntPtr.Zero)
				{
					int error = GetLastError();
					lbStatus.Text = "Alloc failed, " + error.ToString();
					return;
				}

				foreach (int jmp in storagejumps)
				{
					int wbuffp = jmp + 1;
					int oldadd = BitConverter.ToInt32(wbuff, wbuffp);
					int offsetadd = oldstorage - (int)storage + (int)baseaddress - oldbase;
					int newadd = oldadd + offsetadd;
					BitConverter.GetBytes(newadd).CopyTo(wbuff, wbuffp);
				}
				foreach (int jmp in storageabs)
				{
					int wbuffp = jmp;
					int oldadd = BitConverter.ToInt32(wbuff, wbuffp);
					int offsetadd = (int)baseaddress - oldbase;
					int newadd = oldadd + offsetadd;
					BitConverter.GetBytes(newadd).CopyTo(wbuff, wbuffp);
				}
				foreach (int rand in randcalls)
				{
					int wbuffp = rand + 1;
					//BitConverter.GetBytes((int)randaddress).CopyTo(wbuff, wbuffp);
				}
				for (int i = 0; i < newmemlength - 4; i++)
				{
					int oldadd = BitConverter.ToInt32(wbuff, i);
					if (oldadd >= oldstorage && oldadd < oldstorage + newmemlength)
					{
						int offsetadd = (int)storage - oldstorage;
						int newadd = oldadd + offsetadd;
						BitConverter.GetBytes(newadd).CopyTo(wbuff, i);
					}
				}
				ret = WriteProcessMemory(processHandle, storage, wbuff, newmemlength, ref rn);
				if (!ret)
				{
					lbStatus.Text = "Write to storage " + storage.ToString() + " failed.";
					return;
				}

				byte[] diff = new byte[2048];
				fs = new FileStream("dif.bin", FileMode.Open);
				if (fs == null)
					return;
				int difflength = fs.Read(diff, 0, 2048);
				fs.Close();
				for (int i = 0; i < difflength;)
				{
					int addr = BitConverter.ToInt32(diff, i);
					int len = BitConverter.ToInt32(diff, i + 4);
					for (int b = 0; b < len; b++)
						wbuff[b] = diff[i + 8 + b];
					ret = WriteProcessMemory(processHandle, (IntPtr)((int)baseaddress + addr), wbuff, len, ref rn);
					i += 8 + len;
				}
				foreach (int jmp in codejumps)
				{
					ReadProcessMemory(processHandle, (IntPtr)((int)baseaddress + jmp + 1), wbuff, 4, ref rn);
					int oldadd = BitConverter.ToInt32(wbuff, 0);
					int offsetadd = (int)storage - oldstorage + oldbase - (int)baseaddress;
					int newadd = oldadd + offsetadd;
					BitConverter.GetBytes(newadd).CopyTo(wbuff, 0);
					WriteProcessMemory(processHandle, (IntPtr)((int)baseaddress + jmp + 1), wbuff, 4, ref rn);
				}


				if (!ret)
				{
					lbStatus.Text = "Write to 0x50000 failed.";
					return;
				}

				lbStatus.Text = "Spelunky game detected. newmem=" + storage.ToString("X");
			}
			else
			{
				if (process.HasExited)
				{
					processHandle = IntPtr.Zero;
					process = null;
					lbStatus.Text = "Waiting for game process...";
				}
			}
		}

		private void cbRandom_CheckedChanged(object sender, EventArgs e)
		{
			byte[] wbuff = new byte[4];
			int rn = 0;
			if (cbRandom.Checked && processHandle != IntPtr.Zero)
			{
				BitConverter.GetBytes(1).CopyTo(wbuff, 0);
				WriteProcessMemory(processHandle, (IntPtr)((int)storage + 0x510), wbuff, 4, ref rn);
			}
			else if (!cbRandom.Checked && processHandle != IntPtr.Zero)
			{
				BitConverter.GetBytes(0).CopyTo(wbuff, 0);
				WriteProcessMemory(processHandle, (IntPtr)((int)storage + 0x510), wbuff, 4, ref rn);
			}
		}
	}
}
