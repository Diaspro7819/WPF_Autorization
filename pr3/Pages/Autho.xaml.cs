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
using pr3.Model;
using HashPassword;
using System.Diagnostics.SymbolStore;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Threading;

namespace pr3.Pages
{
	/// <summary>
	/// Логика взаимодействия для Autho.xaml
	/// </summary>
	public partial class Autho : Page
	{
		int unsuccess = 0;
		public string captcha;
		private int count = 10;
		DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Normal);
		public Autho()
		{
			InitializeComponent();
			txtBlockCaptcha.Visibility = Visibility.Hidden;
			txtboxCaptcha.Visibility = Visibility.Hidden;
			txtBlockTimer.Visibility = Visibility.Hidden;
		}
		private void GetCaptcha()
		{
			captcha = SetCaptcha();
			txtBlockCaptcha.Text = $"Введите капчу: {captcha}";
			txtBlockCaptcha.Visibility = Visibility.Visible;
			txtboxCaptcha.Visibility = Visibility.Visible;

		}
		private string SetCaptcha()
		{
			string simbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			Random random = new Random();
			var lenght = random.Next(5, 7);
			var cBuild = new StringBuilder();
			for (int i = 0; i < lenght; i++)
			{
				cBuild.Append(simbols[random.Next(simbols.Length)]);
			}
			return cBuild.ToString();
		}
		private void LoadForm(string role, Workers workers)
		{
			switch (role)
			{
				case "Оператор":
					NavigationService.Navigate(new Operator(workers));
					break;
				case "Администратор":
					NavigationService.Navigate(new Admin(workers));
					break;
				case "Продавец-консультант":
					NavigationService.Navigate(new Trader(workers));
					break;
			}
		}
		private void Timer_Tick(object sender, EventArgs e)
		{
			count-=1;
			if(count!=0)
			{
				txtBlockTimer.Text = $"До конца блокировки осталось " + count.ToString() + " секунд";

			}
			else
			{
				timer.Stop();
				txtboxCaptcha.IsEnabled = true;
				txtbLogin.IsEnabled = true;
				pswdPassword.IsEnabled = true;
				btnEnter.IsEnabled = true;
				btnEnterGuests.IsEnabled = true;
				txtBlockTimer.Visibility = Visibility.Hidden;
				unsuccess = 0;
			}
		}
		private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
		{
			if (Time.Access() == true)
			{
				NavigationService.Navigate(new Client());
			}
			else
			{
				MessageBox.Show("Не рабочее время!");
			}
        }
		User user = new User();
		Workers workers = new Workers();
		private void btnEnter_Click(object sender, RoutedEventArgs e)
		{
			string login = txtbLogin.Text.Trim();
			string password = Hash.Pass(pswdPassword.Password.Trim());
			if (Time.Access() == true)
			{
				if (login.Length > 0 && password.Length > 0)
				{
					if (unsuccess >= 3)
					{
						txtboxCaptcha.IsEnabled = false;
						txtbLogin.IsEnabled = false;
						pswdPassword.IsEnabled = false;
						btnEnter.IsEnabled = false;
						btnEnterGuests.IsEnabled = false;
						txtBlockTimer.Visibility = Visibility.Visible;
						timer = new DispatcherTimer();
						timer.Tick += Timer_Tick;
						timer.Interval = TimeSpan.FromSeconds(1);
						count = 10;
						txtBlockTimer.Text = $"До конца блокировки осталось " + count.ToString() + " секунд";
						timer.Start();

					}
					else if (unsuccess == 0)
					{
						user = AuthHelp.GetContext().User.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
						int userCount = AuthHelp.GetContext().User.Where(u => u.Login == login && u.Password == password).Count();


						if (userCount > 0)
						{
							workers = AuthHelp.GetContext().Workers.Where(w => w.User_id == user.User_id).FirstOrDefault();
							MessageBox.Show("Вы вошли под ролью: " + user.Roles.Name.ToString());
							LoadForm(user.Roles.Name.ToString(), workers);
						}
						else
						{
							MessageBox.Show("Неверный логин или пароль!", "Warning", MessageBoxButton.OK);
							unsuccess++;
							GetCaptcha();
						}
					}
					else
					{
						user = AuthHelp.GetContext().User.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
						int userCount = AuthHelp.GetContext().User.Where(u => u.Login == login && u.Password == password).Count();

						if (userCount > 0 && captcha == txtboxCaptcha.Text)
						{
							unsuccess = 0;
						}
						else
						{
							MessageBox.Show("Введите данные заново!", "Warning", MessageBoxButton.OK);
							GetCaptcha();
							unsuccess++;
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Не рабочее время!");
			}
        }
    }
}
