﻿using pr3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr3.Pages
{
	/// <summary>
	/// Логика взаимодействия для Admin.xaml
	/// </summary>
	public partial class Admin : Page
	{
		Time time = new Time();
		public Admin(Workers workers)
		{
			InitializeComponent();
			Hello.Text = $"{time.get_time()} {workers.Name} {workers.Surname}";
			OK.Visibility = Visibility.Visible;
			Hello.Visibility = Visibility.Visible;
		}

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			OK.Visibility = Visibility.Hidden;
			Hello.Visibility = Visibility.Hidden;
		}
	}
}
