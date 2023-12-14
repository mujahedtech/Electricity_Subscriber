using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mujahed_Package.Layouts
{



    /// <summary>
    /// Interaction logic for ReceiveCash.xaml
    /// </summary>
    /// 
    public class CounterMatch
    {
        public int Billno { get; set; }

        public string BillAmount { get; set; }
        public string BillState { get; set; }

        public string BillStateColor { get; set; }

        public string CashAmount { get; set; }

        public int CashNo { get; set; }
    }
    public class CounterZakah
    {
        public string ZakahAmount { get; set; }
        public string ZakahNote { get; set; }
        public string TextColor { get; set; }
    }


    //كلاس عرض المعلقات النقدية على المناديب مع سبب التعليق
    public class CashNotComplete
    {
        public string CashAmount { get; set; }
        public string CashMsg { get; set; }
    }


    public partial class ReceiveCash : UserControl
    {


        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (btnFastKey.IsChecked == false)
            {
                if (e.Key == Key.Space)
                {
                    e.Handled = true;
                }
            }
            base.OnPreviewKeyDown(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (btnFastKey.IsChecked == false)
            {
                if (e.Key == Key.Z)
                {
                    txtZakah.Focus();
                }
                else if (e.Key == Key.C)
                {
                    txtcash50.Focus();
                }
                else if (e.Key == Key.R)
                {
                    txtcashneeded.Focus();
                }
                else if (e.Key == Key.S)
                {
                    salesmanID.Focus();
                }

                else if (e.Key == Key.N)
                {
                    RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
                    btnNextCash.RaiseEvent(newEventArgs);
                }

                else if (e.Key == Key.B)
                {
                    RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
                    btnLastCash.RaiseEvent(newEventArgs);
                }
            }


            base.OnKeyDown(e);
        }



        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            GridMessage.Visibility = Visibility.Hidden;

            btnAllowReEnter.Visibility = Visibility.Visible;

            txtcashneeded.Focus();



        }
        void Message(string message)
        {
            GridMessage.Visibility = Visibility.Visible;
            MsgError.Text = message;
            btnOk.Focus();
        }

        void EnterNote(string Note)
        {

            GridZakahNote.Visibility = Visibility.Visible;
            txtNoteZakah.Text = Note;
            txtNoteZakah.Focus();

        }
        //تجهيز تقرير يعرض لي الكاش الحقيقي و هو مجموع المطلوب من الغلة + الفواتير النقدية

        System.Data.DataTable DTChecks = new System.Data.DataTable();

        System.Data.DataTable DTClient = new System.Data.DataTable();

        public System.Data.DataTable DTCustomData = new System.Data.DataTable();

        public ReceiveCash()
        {
            InitializeComponent();


            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += CurrentDomain_UnhandledException;


            lblserial.Text = new CL.SalesCash().GetLastSerialID().ToString();


        }



        bool UpdateMode { get; set; } = false;

        public ReceiveCash(bool Update)
        {
            InitializeComponent();


            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += CurrentDomain_UnhandledException;



            UpdateMode = Update;

        }



        string UpdateIDSerial = "";
        public ReceiveCash(string IDSerial)
        {
            InitializeComponent();


            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += CurrentDomain_UnhandledException;

            UpdateIDSerial = IDSerial;



            PrepareView();

            UpdateMode = true;

            lblserial.Text = IDSerial;



            GetDataFromTextNumber();

        }

        System.Data.DataTable DT = new System.Data.DataTable();


        void PrepareView()
        {
            NavigationMode = false;
            string IDSerial = UpdateIDSerial;

            DT = new CL.SalesCash().GetCashPerID(IDSerial);

            DTChecks = new CL.SalesCash().GetZakahPerID(IDSerial);
            UpdateCheckList();
            //                     0       1         2           3           4
            //string Columns = " IDNumber,DateSales,SalesManName,TotalZakah,TotalRequiredCash," +
            //                 //   5                 6          7     8      9     10    11     12     13
            //                 "TotalCashOnHand,TotalEndCash,Cash50,Cash20,Cash10,Cash5,Cash1,CashNon,SalesNote";



            CobNameClient.ItemsSource = DTClient.DefaultView;






            lblserial.Text = DT.Rows[0][0].ToString();
            lblDate.Text = DT.Rows[0][1].ToString();
            CobNameClient.SelectedValue = DT.Rows[0][15].ToString();
            txtcashneeded.Text = DT.Rows[0][4].ToString();
            txttotalcashonhand.Text = DT.Rows[0][5].ToString();
            txtendcash.Text = DT.Rows[0][6].ToString();

            txtcash50.Text = DT.Rows[0][7].ToString();
            txtcash20.Text = DT.Rows[0][8].ToString();
            txtcash10.Text = DT.Rows[0][9].ToString();
            txtcash5.Text = DT.Rows[0][10].ToString();
            txtcash1.Text = DT.Rows[0][11].ToString();
            txtcashnon.Text = DT.Rows[0][12].ToString();
            txtnote.Text = DT.Rows[0][13].ToString();
            lblserial.Text = CL.PassParameters.IDSerial;


            salesmanID.Text = DT.Rows[0][15].ToString();
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillClient();



            if (DTChecks.Columns.Count == 0)
            {
                DTChecks.Columns.Add("ZakahAmount", typeof(decimal));
                DTChecks.Columns.Add("ZakahNote", typeof(string));
            }


            salesmanID.Focus();



            //ThicknessAnimation ThickAnimation = new ThicknessAnimation();
            //ThickAnimation.From = new Thickness(txtNotification.ActualWidth, 0, 0, 0);
            //ThickAnimation.To = new Thickness(0, 0, 0, 0);
            //ThickAnimation.RepeatBehavior = RepeatBehavior.Forever;
            //ThickAnimation.Duration = new Duration(TimeSpan.FromSeconds(15));
            //txtNotification.BeginAnimation(TextBox.PaddingProperty, ThickAnimation);



        }

        //handle All Error In Message
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.ExceptionObject as Exception;
                MessageBox.Show(ex.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Application Will ShutDown !!!", "ShutDown", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catch (Exception)
            {


            }

        }


        public bool IsTextAllowed(string str)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[0-9/.]");

            return regex.IsMatch(str);
        }


        public Task<int> Fill_Client()
        {
            return Task.Run(() =>
            {

                DTClient = new CL.SalesCash().GetSalesmanName();



                return 0;

            });
        }



        void FillClient()
        {


            DTClient = new CL.SalesCash().GetSalesmanName();

            CobNameClient.ItemsSource = DTClient.DefaultView;




        }
        void InsertZakah()
        {

            if (DTChecks.Columns.Count == 0)
            {
                DTChecks.Columns.Add("ZakahAmount", typeof(string));
                DTChecks.Columns.Add("ZakahNote", typeof(string));
            }

            if (CL.Validations.Isdecimal(txtZakah.Text) == false)
            {
                GridMessage.Visibility = Visibility.Visible;
                btnAllowReEnter.Visibility = Visibility.Collapsed;
                MsgError.Text = "الرجاء ادخال قيمة المصروف بشكل صحيح";
                return;
            }

            DTChecks.Rows.Add(txtZakah.Text, "تبرعات");


            UpdateCheckList();
            CountCash();
            IndexZakahNote = DTChecks.Rows.Count - 1;
            EnterNote(DTChecks.Rows[IndexZakahNote][1].ToString());
        }



        void UpdateCheckList()
        {

            List<CounterZakah> checks = new List<CounterZakah>();
            if (DTChecks.Rows.Count > 0 && DTChecks.Columns.Count > 0)
            {

                for (int i = 0; i < DTChecks.Rows.Count; i++)
                {
                    CounterZakah check = new CounterZakah();
                    check.ZakahAmount = DTChecks.Rows[i][0].ToString();
                    check.ZakahNote = DTChecks.Rows[i][1].ToString();
                    if (DTChecks.Rows[i][1].ToString() == "")
                    {
                        check.TextColor = "Red";
                    }
                    else if (DTChecks.Rows[i][1].ToString() != "")
                    {
                        check.TextColor = "Green";
                    }
                    checks.Add(check);
                }
                listZakah.ItemsSource = null;
                listZakah.ItemsSource = checks;

            }
            if (DTChecks.Rows.Count == 0)
            {
                listZakah.ItemsSource = null;
            }

            lblsumzakah.Text = DTChecks.Compute("sum(ZakahAmount)", string.Empty).ToString();

        }


        System.Data.DataTable DTLessCash;
        //دالة تقوم بتحديث المعلقات على المناديب
        void UpdateCashLessList(string Name)
        {
            if (CobNameClient.SelectedIndex > -1)
            {

                string MessageUser = "";

                txtKron.Inlines.Clear();
                txtKron2.Inlines.Clear();

                DTLessCash = new CL.SalesCash().GetLessCash(Name);
                List<CashNotComplete> LessCashs = new List<CashNotComplete>();
                if (DTLessCash.Rows.Count > 0 && DTLessCash.Columns.Count > 0)
                {
                    for (int i = 0; i < DTLessCash.Rows.Count; i++)
                    {
                        CashNotComplete lesscash = new CashNotComplete();
                        lesscash.CashAmount = DTLessCash.Rows[i][1].ToString();
                        lesscash.CashMsg = DTLessCash.Rows[i][2].ToString();

                        LessCashs.Add(lesscash);

                        MessageUser += DTLessCash.Rows[i][3].ToString() + $"=> ({DTLessCash.Rows[i][1].ToString()})+ | ";


                        txtKron.Inlines.Add(new Run(DTLessCash.Rows[i][3].ToString()) { Foreground = Brushes.Green });
                        txtKron.Inlines.Add(new Run("=> (") { Foreground = Brushes.DodgerBlue });
                        txtKron.Inlines.Add(new Run($"{DTLessCash.Rows[i][1].ToString()}") { Foreground = Brushes.Red });
                        txtKron.Inlines.Add(new Run(")+ @@ ") { Foreground = Brushes.DodgerBlue });


                        txtKron2.Inlines.Add(new Run(DTLessCash.Rows[i][3].ToString()) { Foreground = Brushes.Green });
                        txtKron2.Inlines.Add(new Run("=> (") { Foreground = Brushes.DodgerBlue });
                        txtKron2.Inlines.Add(new Run($"{DTLessCash.Rows[i][1].ToString()}") { Foreground = Brushes.Red });
                        txtKron2.Inlines.Add(new Run(")+ @@ ") { Foreground = Brushes.DodgerBlue });



                    }


                    //StartTicker(MessageUser);

                    LessCashList.ItemsSource = null;
                    LessCashList.ItemsSource = LessCashs;

                }
                if (DTLessCash.Rows.Count == 0)
                {
                    LessCashList.ItemsSource = null;
                }
            }

            //DTLessCash = new CL.SalesCash().GetLessCash(CobNameClient.Text);
            //for (int i = 0; i < DTLessCash.Rows.Count; i++)
            //{
            //    LessCashList.Items.Add(DTLessCash.Rows[i][0].ToString());
            //}
        }


        void StartTicker(string message)
        {

            //DoubleAnimation doubleAnimation = new DoubleAnimation();
            //doubleAnimation.From = canMain.ActualWidth;
            //doubleAnimation.To = 0;
            //doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            //doubleAnimation.AutoReverse = true;
            //doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:11"));
            //Viewboxtbmarquee.BeginAnimation(Canvas.RightProperty, doubleAnimation);

        }


        private void TxtZakah_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                txtNoteZakah.Focus();
                if (txtZakah.Text.Length > 0)
                {
                    //GridZakahNote.Visibility = Visibility.Visible;
                    txtNoteZakah.Focus();
                    InsertZakah();
                    txtZakah.Clear();
                }



            }
            else if (e.Key == Key.RightShift)
            {
                GridZakahNote.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.F12)
            {
                DTChecks.Rows.Clear();
                UpdateCheckList();
                CountCash();
            }
            else if (e.Key == Key.F2)
            {
                ViewCashOutInText();
                gridnote.Visibility = Visibility.Visible;
                txtnote.Focus();
            }
            else if (e.Key == Key.F9)
            {
                gridimage.Visibility = Visibility.Visible;

            }

        }
        void ViewCashOutInText()
        {
            txtOutCash.Text = "";
            string CashOut = "";
            for (int i = 0; i < DTChecks.Rows.Count; i++)
            {
                CashOut += $"{DTChecks.Rows[i][0].ToString()}--{DTChecks.Rows[i][1].ToString()}" + System.Environment.NewLine;
            }
            txtOutCash.Text = CashOut;
        }
        private void CobNameClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var uie = e.OriginalSource as UIElement;
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));



            }

            else if (e.Key == Key.F1)
            {
                if (txttotalcashonhand.Visibility == Visibility.Visible)
                {
                    txttotalcashonhand.Visibility = Visibility.Hidden;
                }
                else if (txttotalcashonhand.Visibility == Visibility.Hidden)
                {
                    txttotalcashonhand.Visibility = Visibility.Visible;
                }
            }
            else if (e.Key == Key.F2)
            {
                ViewCashOutInText();
                gridnote.Visibility = Visibility.Visible;
                txtnote.Focus();
            }
            else if (e.Key == Key.RightShift)
            {
                RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
                saveData.RaiseEvent(newEventArgs);

            }


            else if (e.Key == Key.F12)
            {


                int Cash = 0;

                Cash++;


                if (txtAddsCash.Text.Length == 0) txtAddsCash.Text = Cash.ToString();

                else if (txtAddsCash.Text.Length > 0) txtAddsCash.Text = (int.Parse(txtAddsCash.Text) + Cash).ToString();

                cardplusCash.Visibility = Visibility.Hidden;
            }

        }
        void CountCash()
        {
            decimal cash50 = 0, cash20 = 0, cash10 = 0, cash5 = 0, cash1 = 0, cashnon = 0;
            if (txtcash50.Text != "")
            {

                cash50 = decimal.Parse(txtcash50.Text);
            }
            if (txtcash20.Text != "")
            {
                cash20 = decimal.Parse(txtcash20.Text);
            }
            if (txtcash10.Text != "")
            {
                cash10 = decimal.Parse(txtcash10.Text);
            }
            if (txtcash5.Text != "")
            {
                cash5 = decimal.Parse(txtcash5.Text);
            }
            if (txtcash1.Text != "")
            {
                cash1 = decimal.Parse(txtcash1.Text);
            }
            if (txtcashnon.Text != "")
            {
                cashnon = decimal.Parse(txtcashnon.Text);
            }






            if (txtcash50.Text == "")
            {
                cash50 = 0;
            }
            if (txtcash20.Text == "")
            {
                cash20 = 0;
            }
            if (txtcash10.Text == "")
            {
                cash10 = 0;
            }
            if (txtcash5.Text == "")
            {
                cash5 = 0;
            }
            if (txtcash1.Text == "")
            {
                cash1 = 0;
            }
            if (txtcashnon.Text == "")
            {
                cashnon = 0;
            }

            txttotalcashonhand.Text = (cash50 + cash20 + cash10 + cash5 + cash1 + cashnon).ToString("0.00");


            decimal totalCash = 0, cashneeded = 0, ZakahAmount = 0, PlusCash = 0;



            if (txtcashneeded.Text != "")
            {

                cashneeded = decimal.Parse(txtcashneeded.Text);
            }
            if (txttotalcashonhand.Text != "")
            {
                totalCash = decimal.Parse(txttotalcashonhand.Text);
            }
            if (DTChecks.Rows.Count > 0)
            {
                ZakahAmount = decimal.Parse(DTChecks.Compute("sum(ZakahAmount)", string.Empty).ToString());

            }
            if (txtAddsCash.Text != "")
            {
                PlusCash = decimal.Parse(txtAddsCash.Text);
            }


            txtendcash.Text = ((totalCash + ZakahAmount) - (cashneeded + PlusCash)).ToString("0.00");

            txtNetRequire.Text = ((cashneeded + PlusCash) - ZakahAmount).ToString("0.00");


            TotalCashRequired = (cashneeded + PlusCash).ToString("0.00");
        }

        string TotalCashRequired;

        private void Txtcashneeded_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                txtNotification.Text = "";
                CountCash();
            }
            catch (Exception m)
            {
                txtNotification.Text = m.Message;
            }


        }

        string SYSID = "";

        string image = "Null";
        private void SaveData_Click(object sender, RoutedEventArgs e)
        {

            if (txtcashneeded.Text == "")
            {
                txtcashneeded.Text = "0";

            }
            string MsgOverCash = $"Are you sure the cash is over {txtendcash.Text} JD";


            if (imagecash.Source != null)
            {
                image = System.IO.Path.GetFullPath(imagecash.Source.ToString().Replace(@"file:///", ""));

            }
            try
            {



                if (new CL.SalesCash().GetCashPerID(lblserial.Text).Rows.Count > 0)
                {
                    UpdateMode = true;
                }


                if (UpdateMode == false)
                {

                    if (AllowReEnter == false)
                    {
                        if (new CL.SalesCash().CheckDuplicate(CobNameClient.Text, lblDate.Text).Rows.Count > 0)
                        {
                            Message("تم ادخال غلة لهذا المندوب في هذا اليوم لا يمكن الادخال مرة اخرى");


                            txtcashneeded.Focus();
                            btnOk.Focus();
                            return;
                        }
                    }

                    if (double.Parse(txtendcash.Text) > 1)
                    {
                        GridDiscardData.Visibility = Visibility.Visible;
                        MsgYesNo.Text = MsgOverCash;
                        btnYes.Focus();
                        return;
                    }



                    AddData();



                }
                else if (UpdateMode == true)
                {
                    if (double.Parse(txtendcash.Text) > 1)
                    {
                        GridDiscardData.Visibility = Visibility.Visible;
                        MsgYesNo.Text = MsgOverCash;
                        btnYes.Focus();
                        return;
                    }


                    UpdateData();

                }

                txtKron.Inlines.Clear();
                txtKron2.Inlines.Clear();


            }
            catch (Exception m)
            {
                Message(m.Message);

            }

        }

        void AddData()
        {


            DateTime dateTime = Convert.ToDateTime(lblDate.Text);
            new CL.SalesCash().InsertNewCash(lblserial.Text,
           dateTime.ToString(),
            CobNameClient.Text,
            DTChecks.Compute("sum(ZakahAmount)", string.Empty).ToString(),
TotalCashRequired,
            txttotalcashonhand.Text,
            txtendcash.Text,
            txtcash50.Text,
            txtcash20.Text,
            txtcash10.Text,
            txtcash5.Text,
            txtcash1.Text,
            txtcashnon.Text,
            txtnote.Text,
            image,
            CobNameClient.SelectedValue.GetHashCode());

            if (DTChecks.Rows.Count > 0)
            {
                for (int i = 0; i < DTChecks.Rows.Count; i++)
                {
                    new CL.SalesCash().InsertZakah(lblserial.Text, DTChecks.Rows[i][0].ToString(), DTChecks.Rows[i][1].ToString());
                }

            }

            ClearText();
            lblserial.Text = new CL.SalesCash().GetLastSerialID().ToString();
            LessCashList.ItemsSource = null;




            //foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            //{
            //    if (window.GetType() == typeof(SecoundWindow))
            //    {
            //        RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            //        (window as SecoundWindow).btnNew.RaiseEvent(newEventArgs);

            //    }
            //}



        }
        void UpdateData()
        {
            try
            {


                DateTime dateTime = Convert.ToDateTime(lblDate.Text);

                new CL.SalesCash().UpdateCash(lblserial.Text,
                       dateTime.ToString(),
                       CobNameClient.Text,
                       DTChecks.Compute("sum(ZakahAmount)", string.Empty).ToString(),
                      TotalCashRequired,
                       txttotalcashonhand.Text,
                       txtendcash.Text,
                       txtcash50.Text,
                       txtcash20.Text,
                       txtcash10.Text,
                       txtcash5.Text,
                       txtcash1.Text,
                       txtcashnon.Text,
                       txtnote.Text,
                       image,
                       CobNameClient.SelectedValue.GetHashCode());
                new CL.SalesCash().DeleteZakah(lblserial.Text);
                if (DTChecks.Rows.Count > 0)
                {

                    for (int i = 0; i < DTChecks.Rows.Count; i++)
                    {
                        new CL.SalesCash().InsertZakah(lblserial.Text, DTChecks.Rows[i][0].ToString(), DTChecks.Rows[i][1].ToString());
                    }

                }
                ClearText();
                LessCashList.ItemsSource = null;

                lblmessageuser.Text = "Edit Success";
                lblserial.Focus();
            }
            catch (Exception m)
            {
                Message(m.Message);

            }

        }

        void ClearText()
        {
            CobNameClient.SelectedIndex = -1;
            txtcashneeded.Clear();
            txttotalcashonhand.Clear();
            txtendcash.Clear();
            txtcash50.Clear();
            txtcash20.Clear();
            txtcash10.Clear();
            txtcash5.Clear();
            txtcash1.Clear();
            txtAddsCash.Clear();
            txtZakah.Clear();
            txtnote.Clear();
            txtendcash.Clear();
            image = "Null";
            imagecash.Source = null;
            txtcashnon.Clear();
            DTChecks.Rows.Clear();
            lblmessageuser.Text = "";
            UpdateCheckList();

            salesmanID.Clear();

            salesmanID.Focus();
            AllowReEnter = false;
            StartReport();

            txtNetRequire.Text = "";


        }


        private void Txtendcash_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtendcash.Text.Length > 0)
            {




                decimal endcash = decimal.Parse(txtendcash.Text);

                if (endcash > 0)
                {
                    txtendcash.Foreground = Brushes.Green;
                    lblmessageuser.Foreground = Brushes.Green;

                    //lblmessageuser.Text = "هناك زيادة في الغلة";
                }
                else if (endcash == 0)
                {
                    txtendcash.Foreground = Brushes.Black;
                    lblmessageuser.Foreground = Brushes.Black;
                    //lblmessageuser.Text = "لا يوجد مشكلة";
                }
                else if (endcash < 0)
                {
                    txtendcash.Foreground = Brushes.Red;
                    lblmessageuser.Foreground = Brushes.Red;
                    //lblmessageuser.Text = "يوجد نقص";
                }
                else
                {
                    txtendcash.Foreground = Brushes.Black;
                    lblmessageuser.Foreground = Brushes.Black;
                    //lblmessageuser.Text = "";
                }
            }
        }

        private void TxtZakah_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {



            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TxtZakah_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Btnnoteclose_Click(object sender, RoutedEventArgs e)
        {
            gridnote.Visibility = Visibility.Collapsed;
        }

        private void BtnImageClose_Click(object sender, RoutedEventArgs e)
        {
            gridimage.Visibility = Visibility.Collapsed;
        }





        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {


            System.Windows.Forms.OpenFileDialog FBD = new System.Windows.Forms.OpenFileDialog();
            FBD.Title = "Select Database";
            FBD.Filter = "png|*.png|jpg|*.jpg|All Files (*.*)|*.*";

            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = FBD.FileName;

                imagecash.Source = Fill_Image(selectedFileName);

            }
        }


        public BitmapImage Fill_Image(string image)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(image);
            bitmap.EndInit();
            return bitmap;
        }




        //select all Text
        private void SelectAddress(object sender, RoutedEventArgs e)

        {

            TextBox tb = (sender as TextBox);

            if (tb != null)

            {

                tb.SelectAll();

            }

        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)

        {

            TextBox tb = (sender as TextBox);

            if (tb != null)

            {

                if (!tb.IsKeyboardFocusWithin)

                {

                    e.Handled = true;

                    tb.Focus();

                }

            }

        }

        private void Lblserial_TextChanged(object sender, TextChangedEventArgs e)
        {


        }


        //دالة من خلالها يتم الانتقال الى الفئة التي لا يوجد ادخال اي فيمة بها
        void GOEmptyCash()
        {
            if (txtcash50.Text == "0.00")
            {
                txtcash50.Focus();
            }
            else if (txtcash20.Text == "0.00")
            {
                txtcash20.Focus();
            }
            else if (txtcash10.Text == "0.00")
            {
                txtcash10.Focus();
            }
            else if (txtcash5.Text == "0.00")
            {
                txtcash5.Focus();
            }
            else if (txtcash1.Text == "0.00")
            {
                txtcash1.Focus();
            }
        }

        void GetDataFromTextNumber()
        {

            lblserial.SelectAll();
            DT = new CL.SalesCash().GetCashPerID(lblserial.Text);
            if (DT.Rows.Count > 0)
            {
                GOEmptyCash();
                Cardlblserial.Background = (Brush)new BrushConverter().ConvertFrom("#B4BAB4");

                UpdateMode = true;




                DTChecks = new CL.SalesCash().GetZakahPerID(lblserial.Text);

                UpdateCheckList();
                //                     0       1         2           3           4
                //string Columns = " IDNumber,DateSales,SalesManName,TotalZakah,TotalRequiredCash," +
                //                 //   5                 6          7     8      9     10    11     12     13
                //                 "TotalCashOnHand,TotalEndCash,Cash50,Cash20,Cash10,Cash5,Cash1,CashNon,SalesNote";



                lblserial.Text = DT.Rows[0][0].ToString();
                lblDate.Text = DT.Rows[0][1].ToString();
                CobNameClient.Text = DT.Rows[0][2].ToString();
                txtcashneeded.Text = DT.Rows[0][4].ToString();
                txttotalcashonhand.Text = DT.Rows[0][5].ToString();
                txtendcash.Text = DT.Rows[0][6].ToString();

                txtcash50.Text = DT.Rows[0][7].ToString();
                txtcash20.Text = DT.Rows[0][8].ToString();
                txtcash10.Text = DT.Rows[0][9].ToString();
                txtcash5.Text = DT.Rows[0][10].ToString();
                txtcash1.Text = DT.Rows[0][11].ToString();
                txtcashnon.Text = DT.Rows[0][12].ToString();
                txtnote.Text = DT.Rows[0][13].ToString();

                salesmanID.Text = DT.Rows[0][15].ToString();

                try
                {
                    imagecash.Source = Fill_Image((DT.Rows[0][14].ToString()));
                }
                catch (Exception)
                {


                }
            }

            else if (DT.Rows.Count <= 0)
            {
                ClearText();
                Cardlblserial.Background = (Brush)new BrushConverter().ConvertFrom("#FFEAEAEA");
                UpdateMode = false;
            }
        }
        private void Lblserial_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {

                GetDataFromTextNumber();


            }
        }



        private void btnLastCash_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationMode == true)
            {
                lblserial.Text = (int.Parse(lblserial.Text) - 1).ToString();
                GetDataFromTextNumber();
                lblserial.Focus();


            }
            else if (NavigationMode == false)
            {
                SelectIndexDT -= 1;
                if (SelectIndexDT <= 0)
                {

                    SelectIndexDT = 0;
                }
                lblserial.Text = DTCustomData.Rows[SelectIndexDT][0].ToString();
                GetDataFromTextNumber();
            }
        }

        private void btnNextCash_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationMode == true)
            {
                lblserial.Text = (int.Parse(lblserial.Text) + 1).ToString();
                GetDataFromTextNumber();
                lblserial.Focus();

            }
            else if (NavigationMode == false)
            {
                int lastdtindex = DTCustomData.Rows.Count - 1;
                SelectIndexDT += 1;
                if (SelectIndexDT > lastdtindex)
                {
                    SelectIndexDT = lastdtindex;

                }
                lblserial.Text = DTCustomData.Rows[SelectIndexDT][0].ToString();
                GetDataFromTextNumber();
            }
        }

        private void btnplusday(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(lblDate.Text);
            dt = dt.AddDays(1);
            lblDate.SelectedDate = dt;
        }

        private void btnlessday_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(lblDate.Text);
            dt = dt.AddDays(-1);
            lblDate.SelectedDate = dt;
        }



        private void txtcash50_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Down)
            {
                var uie = e.OriginalSource as UIElement;
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));


                TextBox tb = (sender as TextBox);

                if (tb != null)

                {

                    tb.SelectAll();

                }

            }
            if (e.Key == Key.Up)
            {
                var uie = e.OriginalSource as UIElement;
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));


                TextBox tb = (sender as TextBox);

                if (tb != null)

                {

                    tb.SelectAll();

                }

            }
            if (e.Key == Key.Delete)
            {
                TextBox tb = (sender as TextBox);

                if (tb != null)

                {

                    tb.SelectAll();

                }

            }

            else if (e.Key == Key.Home)
            {
                e.Handled = true;
                TextBox tb = (sender as TextBox);

                if (tb != null)

                {
                    if (CL.Validations.Isdecimal(tb.Text))
                    {

                        tb.Text = (decimal.Parse(tb.Text) * decimal.Parse(tb.Uid.ToString())).ToString();
                    }
                    else
                    {
                        Message("ادخل قيمة رقمية ليس نصية");
                    }



                }

            }
        }

        private void txtcash50_GotFocus(object sender, RoutedEventArgs e)
        {

        }



        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            GridDiscardData.Visibility = Visibility.Hidden;

        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            GridDiscardData.Visibility = Visibility.Hidden;
            if (UpdateMode == false)
            {

                AddData();
            }
            else if (UpdateMode == true)
            {
                UpdateData();

            }
        }

        private void txttotalcashonhand_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void CobNameClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CobNameClient.SelectedIndex != -1)
            {
                string Name = DTClient.Rows[CobNameClient.SelectedIndex][0].ToString();
                UpdateCashLessList(Name);
                StartReport();
            }

        }

        private void lblserial_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void salesmanID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift)
                if (GridMessage.Visibility == Visibility.Visible)
                    GridMessage.Visibility = Visibility.Collapsed;




            if (e.Key == Key.Enter)
            {
                if (salesmanID.Text.Length > 0)
                {
                    CobNameClient.Text = new CL.SalesCash().GetSalesmanNameBySYSID(salesmanID.Text);

                    SYSID = salesmanID.Text;

                    if (UpdateMode == false)
                    {
                        lblserial.Text = new CL.SalesCash().GetLastSerialID().ToString();
                    }


                }
                if (CobNameClient.SelectedIndex != -1)
                {

                    txtcashneeded.Focus();
                    if (CobNameClient.Text == "اخرى")
                    {
                        txtnote.Text = "سوق عمان";
                    }
                }

            }
        }

        private void chk1_Checked(object sender, RoutedEventArgs e)
        {

            txtnote.Text = "سوق عمان";

        }

        private void chk1_Unchecked(object sender, RoutedEventArgs e)
        {
            txtnote.Clear();
        }

        private void chkLessOnManSales_Unchecked(object sender, RoutedEventArgs e)
        {
            txtnote.Clear();
        }

        private void chkLessOnManSales_Checked(object sender, RoutedEventArgs e)
        {
            if (txtendcash.Text.Length > 0)
            {
                if (CobNameClient.SelectedIndex != -1 && decimal.Parse(txtendcash.Text) < 0)
                {
                    txtnote.Text = String.Concat("نقص على المندوب", " ", CobNameClient.Text, " ", "مبلغ", " ", (decimal.Parse(txtendcash.Text) * -1).ToString(), " ", "دينار");

                }
                else
                {
                    chkLessOnManSales.IsChecked = false;
                }
            }
            else
            {
                chkLessOnManSales.IsChecked = false;
            }

        }


        //متغير من اجل تحديد ما هو وضع التنقل هل حسب التسلسل ام حسب البيانات التي تم فلترتها
        bool NavigationMode = true;
        //متغير يتم حفظ فيه تسلسل القيمة المعروضة من اجل سهولة التنقل بالبيانات
        public int SelectIndexDT;

        private void Togglebtn1_Checked(object sender, RoutedEventArgs e)
        {
            NavigationMode = false;
        }

        private void Togglebtn1_Unchecked(object sender, RoutedEventArgs e)
        {
            NavigationMode = true;

        }




        bool ReadyToSave()
        {
            int counter = 0;

            bool StateReturn = false;

            if (CobNameClient.SelectedIndex != -1 && CobNameClient.SelectedIndex >= 0)
            {
                counter++;
            }


            if (counter == 1)
            {
                StateReturn = true;
            }
            return StateReturn;
        }

        private void GridMessage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if (GridMessage.Visibility==Visibility.Visible)
            //{
            //    GRIDMAINCONTROLL.IsEnabled = false;
            //}
        }

        int IndexZakahNote;

        private void listZakah_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (listZakah.SelectedIndex != -1)
                {
                    IndexZakahNote = listZakah.SelectedIndex;
                    EnterNote(DTChecks.Rows[listZakah.SelectedIndex][1].ToString());

                }

            }

            if (e.ChangedButton == MouseButton.Right)
            {
                if (listZakah.SelectedIndex != -1)
                {
                    DTChecks.Rows.RemoveAt(listZakah.SelectedIndex);
                    UpdateCheckList();
                    CountCash();

                }

            }

        }

        private void listZakah_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterNote(DTChecks.Rows[listZakah.SelectedIndex][0].ToString());
            }
        }

        private void btnZakahOk_Click(object sender, RoutedEventArgs e)
        {
            GridZakahNote.Visibility = Visibility.Collapsed;
            txtZakah.Focus();

            btnFastKey.IsChecked = false;

            DTChecks.Rows[IndexZakahNote][1] = txtNoteZakah.Text;
        }




        void btnCashOutButtons(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            txtNoteZakah.Focus();


            switch (index)
            {

                case 0:
                    txtNoteZakah.Text = "تبرعات";

                    break;
                case 1:
                    txtNoteZakah.Text = "نقص مندوب";
                    break;
                case 2:
                    txtNoteZakah.Text = "بنشر";
                    break;
                case 3:
                    txtNoteZakah.Text = "دفعه";
                    break;
                case 4:
                    txtNoteZakah.Text = "صيانة محل ريش";
                    break;
                case 5:
                    txtNoteZakah.Text = "صيانة سيارة";
                    break;
                case 6:
                    txtNoteZakah.Text = "صيانة محل خير";
                    break;
                case 7:
                    txtNoteZakah.Text = "اجار محل دواجن";
                    break;
            }
            txtNoteZakah.CaretIndex = txtNoteZakah.Text.Length;
        }

        private void txtNoteZakah_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.RightShift)
            {



                txtNoteZakah.Text = "تبرعات";

                RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
                btnZakahOk.RaiseEvent(newEventArgs);

                txtZakah.Focus();
            }



        }




        private void ClosebtnZakahNote_Click(object sender, RoutedEventArgs e)
        {
            GridZakahNote.Visibility = Visibility.Collapsed;
        }

        private void txtAddsCash_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (cardplusCash.Visibility == Visibility.Visible)
            {
                cardplusCash.Visibility = Visibility.Hidden;
                return;
            }
            cardplusCash.Visibility = Visibility.Visible;
        }



        System.Data.DataTable DTSalesManIn;//جدول يتك وضع فيه اسماء المناديب الذين وصلة للشركة
        void StartReport()
        {

            System.Data.DataTable DTSalesMan = new CL.SalesCash().GetSalesmanName();

            DateTime dateTime = Convert.ToDateTime(lblDate.Text);



            System.Data.DataTable DTCashs = new CL.SalesCash().GetCashForReport(dateTime.ToString(CL.PassParameters.DateFormat));

            string code = "";


            int counter_2 = DTCashs.Rows.Count;
            for (int i = 0; i < DTCashs.Rows.Count; i++)
            {
                if (counter_2 != i + 1)
                {
                    code += DTCashs.Rows[i][0] + ",";
                }
                else if (counter_2 == i + 1)
                {
                    code += DTCashs.Rows[i][0];

                }

            }
            if (code == "")
            {
                code = "null";
            }


            DTSalesManIn = new CL.SalesCash().GetSalesmanNameReport(code);



            updatelistIn();
        }

        void updatelistIn()
        {
            DTSalesManIn = new CL.SalesCash().GetCashForReport(lblDate.Text);

            List<SalesManClass> checks = new List<SalesManClass>();
            if (DTSalesManIn.Rows.Count > 0 && DTSalesManIn.Columns.Count > 0)
            {
                for (int i = 0; i < DTSalesManIn.Rows.Count; i++)
                {
                    SalesManClass check = new SalesManClass();
                    check.SalesMan = DTSalesManIn.Rows[i][0].ToString();
                    checks.Add(check);
                }
                //listsalesmanIn.ItemsSource = null;
                //listsalesmanIn.ItemsSource = checks;

            }
            if (DTSalesManIn.Rows.Count == 0)
            {
                //listsalesmanIn.ItemsSource = null;
            }




        }

        //متغير من خلاله يتم السماح للمسخدم بادخال اكثر من حركة خلال يوم واحد
        bool AllowReEnter = false;
        private void btnAllowReEnter_Click(object sender, RoutedEventArgs e)
        {
            AllowReEnter = true;
            GridMessage.Visibility = Visibility.Collapsed;
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
            saveData.RaiseEvent(newEventArgs);
        }

        private void txtAddsCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddsCash.Text = Evaluate(txtAddsCash.Text).ToString();
                ((TextBox)sender).SelectAll();
            }
        }

        static double findSum(String str)
        {
            // A temporary string
            String temp = "0";

            // holds sum of all numbers
            // present in the string
            double sum = 0;

            // read each character in input string
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];



                // if current character is a digit
                if (char.IsDigit(ch))
                {
                    temp += ch;


                }

                // if current character is an alphabet
                else
                {

                    // increment sum by number found earlier
                    // (if any)
                    sum += double.Parse(temp);

                    // reset temporary string to empty
                    temp = "0";
                }
            }

            // atoi(temp.c_str()) takes care of trailing
            // numbers
            return sum + double.Parse(temp);
        }

        static double Evaluate(string expression)
        {

            if (expression.Substring(expression.Length - 1) == "+")
            {
                expression = expression.Remove(expression.Length - 1, 1);
            }





            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (double)(loDataTable.Rows[0]["Eval"]);
        }

        private void SelectAddress(object sender, KeyboardFocusChangedEventArgs e)
        {

            TextBox tb = (sender as TextBox);

            if (tb != null)

            {

                tb.SelectAll();

            }
        }

        private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cardplusCash.Visibility == Visibility.Visible) cardplusCash.Visibility = Visibility.Hidden;
            else cardplusCash.Visibility = Visibility.Visible;
        }
    }
}
