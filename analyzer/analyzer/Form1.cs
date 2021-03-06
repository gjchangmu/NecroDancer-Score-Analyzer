﻿/*
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

		const string title = "NecroDancer Score Analyzer v0.3";
		const int oldbase = 0xB90000;
		const int oldstorage = 0x7B0000;

		List<int> codejumps;
		List<int> storagejumps;
		List<int> storageabs;
		IntPtr processHandle = IntPtr.Zero;
		IntPtr baseaddress;
		IntPtr storage;
		int lastguessFoes = 0;

		public Form1()
		{
			InitializeComponent();
			this.Text = title;

			codejumps = new List<int>();
			codejumps.Add(0xBC002);
			codejumps.Add(0xC0BE9);
			codejumps.Add(0x134800);
			codejumps.Add(0x147975);
			codejumps.Add(0x16BB48);
			codejumps.Add(0x16BA30);
			codejumps.Add(0x60227);
			codejumps.Add(0x6263C);
			codejumps.Add(0x5DF5D);
			codejumps.Add(0x15DCD3);
			codejumps.Add(0x15DD10);
			codejumps.Add(0x15DD4D);
			codejumps.Add(0x15DD8A);
			codejumps.Add(0x15DDC7);
			codejumps.Add(0x16229D);
			codejumps.Add(0x161DBD);
			codejumps.Add(0x1A14F8);
			codejumps.Add(0x1253CD);
			codejumps.Add(0x108BA3);
			codejumps.Add(0x177A1C);
			codejumps.Add(0xFD5A0);
			codejumps.Add(0x13E72C);
			codejumps.Add(0x13E796);
			codejumps.Add(0x183C21);

			storagejumps = new List<int>();
			storagejumps.Add(0xDC);
			storagejumps.Add(0xE1);
			storagejumps.Add(0x117);
			storagejumps.Add(0x13A);
			storagejumps.Add(0x15A);
			storagejumps.Add(0x15F);
			storagejumps.Add(0x17F);
			storagejumps.Add(0x184);
			storagejumps.Add(0x1A4);
			storagejumps.Add(0x1A9);
			storagejumps.Add(0x1CC);
			storagejumps.Add(0x1EF);
			storagejumps.Add(0x214);
			storagejumps.Add(0x257);
			storagejumps.Add(0x25C);
			storagejumps.Add(0x27C);
			storagejumps.Add(0x281);
			storagejumps.Add(0x2C3);
			storagejumps.Add(0x2C8);
			storagejumps.Add(0x2DD);
			storagejumps.Add(0x2E2);
			storagejumps.Add(0x2F3);
			storagejumps.Add(0x304);
			storagejumps.Add(0x315);
			storagejumps.Add(0x323+1);
			storagejumps.Add(0x329);
			storagejumps.Add(0x34E);
			storagejumps.Add(0x353);
			storagejumps.Add(0x371);
			storagejumps.Add(0x376);

			storageabs = new List<int>();
			storageabs.Add(0x236);  // push
			storageabs.Add(0x28C);  // cmp necrodancer.exe+FA116
			storageabs.Add(0x298);  // imul
			storageabs.Add(0x2EF);  // add ecx, necrodancer.exe+2F81E0
			storageabs.Add(0x300);  // add ecx, necrodancer.exe+2F81E0
			storageabs.Add(0x311);  // add ecx, necrodancer.exe+2F81E0

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			bool ret;
			int rn = 0;

			if (processHandle == IntPtr.Zero)
			{
				Process[] Ps = Process.GetProcessesByName("NecroDancer");
				if (Ps.Length == 0)
					return;
				System.Threading.Thread.Sleep(1000);
				processHandle = OpenProcess(0x0038, false, Ps[0].Id);
				if (processHandle == IntPtr.Zero)
					return;
				baseaddress = Ps[0].MainModule.BaseAddress;

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


				storage = VirtualAllocEx(processHandle, IntPtr.Zero, 2048, 0x3000, 0x40);
				if (storage == IntPtr.Zero)
				{
					int error = GetLastError();
					this.Text = "Alloc failed, " + error.ToString();
					return;
				}

				byte[] wbuff = new byte[0x1B0000];
				FileStream fs = new FileStream("storage.bin", FileMode.Open);
				if (fs == null)
					return;
				fs.Read(wbuff, 0, 2048);
				fs.Close();
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
				for (int i = 0; i < 2044; i++)
				{
					int oldadd = BitConverter.ToInt32(wbuff, i);
					if (oldadd >= oldstorage && oldadd < oldstorage + 2048)
					{
						int offsetadd = (int)storage - oldstorage;
						int newadd = oldadd + offsetadd;
						BitConverter.GetBytes(newadd).CopyTo(wbuff, i);
					}
				}
				ret = WriteProcessMemory(processHandle, storage, wbuff, 2048, ref rn);
				if (!ret)
				{
					this.Text = "Write to storage " + storage.ToString() + " failed.";
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
					this.Text = "Write to 0x50000 failed.";
					return;
				}

				this.Text = "NecroDancer game detected. storage=" + storage.ToString("X"); 
			}


			byte[] rbuff = new byte[64];
			ret = ReadProcessMemory(processHandle, (IntPtr)((int)baseaddress + 0x2F81E0), rbuff, 4, ref rn);
			if (!ret)
			{
				this.Text = title;
				processHandle = IntPtr.Zero;
				return;
			}
			int realTotal = BitConverter.ToInt32(rbuff, 0);

			ret = ReadProcessMemory(processHandle, storage, rbuff, 64, ref rn);
			if (!ret)
			{
				this.Text = title;
				processHandle = IntPtr.Zero;
				return;
			}
			int iBoss = BitConverter.ToInt32(rbuff, 0x00);
			lbBoss.Text = iBoss.ToString();
			int iFoes = BitConverter.ToInt32(rbuff, 0x04);
			//lbFoes.Text = iFoes.ToString();
			if (iFoes == 0)
				lastguessFoes = 0;
			int iGoldenwall = BitConverter.ToInt32(rbuff, 0x08);
			lbGoldenwall.Text = iGoldenwall.ToString();
			int iGoldendirt = BitConverter.ToInt32(rbuff, 0x0C);
			lbGoldendirt.Text = iGoldendirt.ToString();
			int iLying = BitConverter.ToInt32(rbuff, 0x10);
			lbLying.Text = iLying.ToString();
			int iCrate = BitConverter.ToInt32(rbuff, 0x14);
			lbCrate.Text = iCrate.ToString();
			int iLeprechaun = BitConverter.ToInt32(rbuff, 0x18);
			lbLeprechaun.Text = iLeprechaun.ToString();
			int iPawnbroker = BitConverter.ToInt32(rbuff, 0x1C);
			lbPawnbroker.Text = iPawnbroker.ToString();
			int iCrawn = BitConverter.ToInt32(rbuff, 0x24);
			lbCrawn.Text = iCrawn.ToString();
			int iShop = BitConverter.ToInt32(rbuff, 0x28);
			lbShop.Text = iShop.ToString();
			int iRiches = BitConverter.ToInt32(rbuff, 0x2C);
			lbRiches.Text = iRiches.ToString();
			//int iTotal = BitConverter.ToInt32(rbuff, 0x34);
			//lbTotal.Text = iTotal.ToString();
			int guessFoes = realTotal - iBoss - iGoldenwall - iGoldendirt - iLying - iCrate - iLeprechaun - iPawnbroker - iCrawn - iShop - iRiches;
			if (guessFoes > lastguessFoes)
				lastguessFoes = guessFoes;
            lbFoes.Text = Math.Max(guessFoes, lastguessFoes).ToString();
			int iTotal = iBoss + lastguessFoes + iGoldenwall + iGoldendirt + iLying + iCrate + iLeprechaun + iPawnbroker + iCrawn + iShop + iRiches;
			lbTotal.Text = iTotal.ToString();
		}
	}
}
