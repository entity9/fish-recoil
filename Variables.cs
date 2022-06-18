using System;

namespace Fatal
{
	class Variables
	{
		public class Menu
		{
			// ENABLED //
			private static bool Enabled { get; set; } = false;
			public static bool isEnabled()
			{ return Enabled; }
			public static void setEnabled(bool boolean)
			{ Enabled = boolean; }

			public static bool Humanizer { get; set; } = false;

			public static bool isHumanizer()
			{ return Humanizer; }

			public static void setHumanizer(bool boolean)
			{ Humanizer = boolean; }
		}

		public class Weapon
		{
			// WEAPON NAME //
			private static string Name { get; set; } = string.Empty;
			public static string getName()
			{
				if (!String.IsNullOrEmpty(Name)) return Name;
				else return "None";
			}
			public static void setName(string newName)
			{ Name = newName; }

			// AMMO //
			private static int Ammo { get; set; } = 0;
			public static int getAmmo()
			{ return Ammo; }
			public static void setAmmo(int AmmoSize)
			{ Ammo = AmmoSize; }

			// RECOIL X / Y //
			private static int[,] ActiveRecoil { get; set; } = { { 0, 0 } };
			public static int getRecoilX(int Bullet)
			{ return ActiveRecoil[Bullet, 0]; }
			public static int getRecoilY(int Bullet)
			{ return ActiveRecoil[Bullet, 1]; }
			public static void setRecoilPattern(int[,] Pattern)
			{ ActiveRecoil = Pattern; }

			// DELAY //
			private static double[] Delay { get; set; } = { 0 };
			public static double getShotDelay(int Bullet)
			{ return Delay[Bullet]; }
			public static void setShotDelay(double[] Delays)
			{ Delay = Delays; }

			// SHOOTING MILLISECONDS //
			private static int ShootingMilliSec { get; set; } = 0;
			public static int getShootingMilliSec()
			{ return ShootingMilliSec; }
			public static void setShootingMilliSec(int MilliSec)
			{ ShootingMilliSec = MilliSec; }

			// SCOPES ATTACHMENTS //
			public static int scopeIndex { get; set; } = 0;
			private static string Scope { get; set; } = "None";
			private static double ScopeMulitplier { get; set; } = 1.0;
			public static string getActiveScope()
			{
				if (!string.IsNullOrEmpty(Scope) && Scope != "None")
					return Scope;
				else return "None";
			}
			public static void changeScope(int ScopeIndex)
			{
				if (ScopeIndex == 0)
				{
					Scope = "None";
					ScopeMulitplier = 1.0;
				}
				else if (ScopeIndex == 1)
				{
					Scope = "Simple scope";
					ScopeMulitplier = 0.8;
				}
				else if (ScopeIndex == 2)
				{
					Scope = "Holo scope";
					ScopeMulitplier = 1.2;
				}
				else if (ScopeIndex == 3)
				{
					Scope = "8x scope";
					ScopeMulitplier = 3.83721;
				}
				else if (ScopeIndex == 4)
				{
					Scope = "16x scope";
					ScopeMulitplier = 7.65116;
				};
			}

			public static double getScopeMulitplier()
			{ return ScopeMulitplier; }

			// BARREL ATTACHMENTS //
			public static int barrelIndex { get; set; } = 0;
			private static string Barrel { get; set; } = "None";
			private static double BarrelMultiplier { get; set; } = 1.0;
			public static string getActiveBarrel()
			{
				if (!string.IsNullOrEmpty(Barrel) && Barrel != "None")
					return Barrel;
				else return "None";
			}
			public static void changeBarrel(int barrelIndex)
			{
				if (barrelIndex == 0)
				{
					Barrel = "None";
					BarrelMultiplier = 1.0;
					MuzzleBoost = false;
				}
				else if (barrelIndex == 1)
				{
					Barrel = "Suppressor";
					BarrelMultiplier = 0.8;
					MuzzleBoost = false;
				}
				else if (barrelIndex == 2)
				{
					Barrel = "Muzzle boost";
					BarrelMultiplier = 1.0;
					MuzzleBoost = true;
				}
				else if (barrelIndex == 3)
				{
					Barrel = "Muzzle break";
					BarrelMultiplier = 0.5;
					MuzzleBoost = false;
				}
			}
			public static double getBarrelMultiplier()
			{ return BarrelMultiplier; }

			// MUZZLE BOOST //
			private static bool MuzzleBoost { get; set; } = false;
			public static double isMuzzleBoost(double MilliSec)
			{
				if (MuzzleBoost)
					return (MilliSec - (MilliSec * 0.1f));
				else
					return MilliSec;
			}

			// RANDOMNESS //
			private static double Randomness { get; set; } = 1.0;
			public static void setRandomness(int RandomnessIndex)
			{
				Randomness = RandomnessIndex;
			}
			public static double getRandomness()
			{ return Randomness; }
		}

		public class Settings
		{
			// SENSITIVITY //
			private static double Sensitivity { get; set; } = 1.0;
			public static void setSensitivity(double SensitivityIndex)
			{
				Sensitivity = SensitivityIndex;
			}
			public static double getSensitivity()
			{ return Sensitivity; }

			// FOV //
			private static int FOV { get; set; } = 90;
			public static void setFOV(int FOVIndex)
			{
				switch (FOVIndex)
				{
					case -1:
						if (71 < FOV)
							FOV -= 1;
						break;
					case 1:
						if (FOV < 90)
							FOV += 1;
						break;
				}
			}
			public static int getFOV()
			{ return FOV; }
		}
	}
}
