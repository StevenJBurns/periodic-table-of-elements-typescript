using System;
using System.Windows;
using Science.Chemistry;

namespace Science
	{
	class App : Application
		{
		public App()
			{
			}
		
		[STAThread]
		static void Main()
			{
			App app = new App();
			app.Run(new WindowPeriodicTable());
			}
		
		}
		
	}
