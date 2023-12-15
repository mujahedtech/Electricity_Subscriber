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

namespace AccountingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            Loaded += MainWindow_Loaded;
        }





        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {



        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void btnMainButtons(object sender, RoutedEventArgs e)
        {

            int index = int.Parse(((Button)e.Source).Uid);


            //GridCursor.Margin = new Thickness(5 + (140 * index), 0, 0, 0);



            switch (index)
            {
                case 0:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new View.ManageAccounts.MainAccounting());



                    break;
                case 1:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new View.ManageAccounts.AccountingTransaction());

                    break;
                case 2:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new View.NajahEpic.NajahMain());
                    break;
                case 3:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new View.GasManage.MainGasManage());
                    break;
                case 4:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new View.SheepManage.MainSheep());

                    break;
                case 5:
                    GridMain.Children.Clear();
                    break;
                case 6:
                    GridMain.Children.Clear();

                    break;
            }

        }
        private void btnTopButtons(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                case 0:
                    if (WindowState == WindowState.Maximized)
                    {
                        WindowState = WindowState.Normal;
                        PackIconWindowsState.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
                    }
                    else if (WindowState != WindowState.Maximized)
                    {
                        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                        WindowState = WindowState.Maximized;
                        PackIconWindowsState.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowRestore;
                    }

                    break;
                case 1:
                    Application.Current.Shutdown();
                    break;
                case 2:
                    WindowState = WindowState.Minimized;
                    break;
                case 3:
                    App.UpdateList();

                    break;
                case 4:
                    btnUpload_Clicked();
                    break;
            }
        }



        private async void btnUpload_Clicked()
        {



            try
            {

                ////await App.dbContext.BackupDB();



                //ProgressBar progressBar = new ProgressBar();
                //progressBar.Foreground = Brushes.LightGreen;
                //progressBar.Height = 20;

                //progressBar.BorderBrush = Brushes.White;
                //progressBar.BorderThickness = new Thickness(1);
                //TextBlock percentStr = new TextBlock { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 15, Foreground = Brushes.Black };
                //progressBar.Maximum = 100;
                //progressBar.Minimum = 0;

                //GridProgress.Children.Clear();
                //GridProgress.Children.Add(progressBar);
                //GridProgress.Children.Add(percentStr);



                //var path = DbContext.BackupName;

                //var stream = File.Open(path, FileMode.Open);
                //var deviceName = System.Environment.MachineName;

                //var task = new Firebase.Storage.FirebaseStorage("khiratserv.appspot.com",
                //    new FirebaseStorageOptions
                //    {
                //        ThrowOnCancel = true
                //    })
                //      .Child(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)
                //    .Child(deviceName)
                //    .Child(DbContext.Path)
                //    .PutAsync(stream);

                //task.Progress.ProgressChanged += (s, args) =>
                //{
                //    progressBar.Value = args.Percentage;
                //    percentStr.Text = progressBar.Value.ToString() + " %";
                //};

                //var downloadlink = await task;


                //var progressHide = new Progress<int>(
                //ValueProgress =>
                //{
                //    GridProgress.Children.Clear();

                //    MessageBox.Show("تم الرفع بنجاح");
                //});


                //await Task.Run(() => { HideProgress(10, progressHide); });
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message, "مشكلة في رفع قاعدة البيانات");
                GridProgress.Children.Clear();
            }



        }
        void HideProgress(int Time, IProgress<int> progress)
        {

            System.Threading.Thread.Sleep(Time * 100);
            progress.Report(0);

        }

    }

}
