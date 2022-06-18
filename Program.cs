using System;
using System.Windows.Forms;
using System.Threading;
using static Fatal.Variables;
using System.Management;
using System.Net;

namespace Fatal
{
    static class Program
    {
		public static string authID = "";
		[STAThread]
		static void Main()
		{

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
        }

		
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// The GetWindowThreadProcessId function retrieves the identifier of the thread
		// that created the specified window and, optionally, the identifier of the
		// process that created the window.
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Returns the name of the process owning the foreground window.
		private static string GetForegroundProcessName()
		{
			IntPtr hwnd = GetForegroundWindow();

			// The foreground window can be NULL in certain circumstances, 
			// such as when a window is losing activation.
			if (hwnd == null)
				return "Unknown";

			uint pid;
			GetWindowThreadProcessId(hwnd, out pid);

			foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
			{
				if (p.Id == pid)
					return p.ProcessName;
			}

			return "Unknown";
		}

		private static bool IsKeyDown(Keys key)
		{ return 0 != (DLLImports.GetAsyncKeyState((int)key) & 0x8000); }

		public static void Discord()
		{
			while (true)
			{
				if (GetForegroundProcessName() == "RustClient")
				{
					if (Variables.Menu.isEnabled() && !string.IsNullOrEmpty(Weapon.getName()))
					{
						while (IsKeyDown(Keys.LButton) && IsKeyDown(Keys.RButton))
						{
							for (int i = 0; i <= Weapon.getAmmo() - 1; i++)
							{
								if (!IsKeyDown(Keys.LButton)) break;
								if (Variables.Menu.isHumanizer() == true)
								{
									double astolfo = GetRandomNumber(1, 100);
									if (astolfo >= 95)
									{
										Smoothing(Weapon.isMuzzleBoost(Weapon.getShootingMilliSec()),
										Weapon.isMuzzleBoost(Weapon.getShotDelay(i)),
										(int)(((((Weapon.getRecoilX(i) + GetRandomNumber(-(Weapon.getRandomness()), Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()) /( (GetRandomNumberInt(2, 3))/2)),
										(int)(((((Weapon.getRecoilY(i) + GetRandomNumber(-(Weapon.getRandomness()), Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()) /( (GetRandomNumberInt(2, 3))/2)));
									}
									else if (astolfo >= 90)
									{
										Smoothing(Weapon.isMuzzleBoost(Weapon.getShootingMilliSec()),
										Weapon.isMuzzleBoost(Weapon.getShotDelay(i)),
										(int)(((((Weapon.getRecoilX(i) + GetRandomNumber(-(Weapon.getRandomness()), Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()) * ((GetRandomNumberInt(2, 3)) / 2)),
										(int)(((((Weapon.getRecoilY(i) + GetRandomNumber(-(Weapon.getRandomness()), Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()) * ((GetRandomNumberInt(2, 3)) / 2)));
									}
									else
									{
										Smoothing(Weapon.isMuzzleBoost(Weapon.getShootingMilliSec()),
										Weapon.isMuzzleBoost(Weapon.getShotDelay(i)),
										(int)((((Weapon.getRecoilX(i) + GetRandomNumber(-(Weapon.getRandomness()), Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()),
										(int)((((Weapon.getRecoilY(i) + GetRandomNumber(-(Weapon.getRandomness()), Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()));
									}

									DLLImports.mouse_event(0x0001, 0, 0, 0, 0);
								}
								else
								{
									Smoothing(Weapon.isMuzzleBoost(Weapon.getShootingMilliSec()),
									Weapon.isMuzzleBoost(Weapon.getShotDelay(i)),
									(int)((((Weapon.getRecoilX(i) + GetRandomNumber(-(Weapon.getRandomness()), Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()),
									(int)((((Weapon.getRecoilY(i) + GetRandomNumber(-(Weapon.getRandomness()), Weapon.getRandomness())) / 4) / Settings.getSensitivity()) * Weapon.getScopeMulitplier() * Weapon.getBarrelMultiplier()));
									DLLImports.mouse_event(0x0001, 0, 0, 0, 0);
								}
							}
						}
					}
				}
				Thread.Sleep(2/3);
			}
		}

		private static double GetRandomNumber(double minimum, double maximum)
		{
			Random random = new Random();
			return random.NextDouble() * (maximum - minimum) + minimum;
		}

		private static double GetRandomNumberInt(int minimum, int maximum)
		{
			int random_number = new Random().Next(minimum, maximum);
			return random_number;
		}

		static private void Smoothing(double MS, double ControlledTime, int X, int Y)
		{
			int oldX = 0, oldY = 0, oldT = 0;
			for (int i = 1; i <= (int)ControlledTime; ++i)
			{
				int newX = i * X / (int)ControlledTime;
				int newY = i * Y / (int)ControlledTime;
				int newTime = i * (int)ControlledTime / (int)ControlledTime;
				DLLImports.mouse_event(1, newX - oldX, newY - oldY, 0, 0);
				PerciseSleep(newTime - oldT);
				oldX = newX; oldY = newY; oldT = newTime;
			}
			PerciseSleep((int)MS - (int)ControlledTime);
		}

		static private void PerciseSleep(int ms)
		{
			DLLImports.QueryPerformanceFrequency(out long timerResolution);
			timerResolution /= 1000;

			DLLImports.QueryPerformanceCounter(out long currentTime);
			long wantedTime = currentTime / timerResolution + ms;
			currentTime = 0;
			while (currentTime < wantedTime)
			{
				DLLImports.QueryPerformanceCounter(out currentTime);
				currentTime /= timerResolution;
			}
		}
	}
}
