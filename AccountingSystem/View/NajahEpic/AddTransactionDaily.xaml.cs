using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static AccountingSystem.ClassMujahed.DataValidation;

namespace AccountingSystem.View.NajahEpic
{
    public class DailySlaughterVM : Models.DailySlaughter
    {
        public double Total { get => Qty * Price; }

        public string CatName { get => App.CatList.Where(i => i.Id == CatId).FirstOrDefault().CatName; }


    }


    /// <summary>
    /// Interaction logic for AddTransactionDaily.xaml
    /// </summary>
    public partial class AddTransactionDaily : UserControl
    {


        public AddTransactionDaily()
        {
            InitializeComponent();

            Loaded += AddTransactionDaily_Loaded;

            GuidTrans = Guid.NewGuid();

            txtGuid.Text = GuidTrans.ToString();

            LocalDailyTran = new ObservableCollection<DailySlaughterVM>();

            ListDailySlaughter.ItemsSource = LocalDailyTran;

            InsertData = new Models.DailyTransaction();
        }


        public AddTransactionDaily(Models.DailyTransaction DailyTransaction)
        {
            InitializeComponent();

            Loaded += AddTransactionDaily_Loaded;





            InsertData = DailyTransaction;
            GuidTrans = InsertData.IdFromSlaughter;


            txtGuid.Text = GuidTrans.ToString();

            txtEndCashDaily.Text = DailyTransaction.EndCash.ToString();
            txtDate.SelectedDate = DailyTransaction.Date;


            var GetDailyTran = new Models.Repositories.DailySlaughterRepository().ListFromIDclass(InsertData.IdFromSlaughter);


            var COnvertList = from em in GetDailyTran.Result.ToList()

                              select new DailySlaughterVM
                              {
                                  IdForTransaction = em.IdForTransaction,
                                  CatId = em.CatId,
                                  Price = em.Price,
                                  Qty = em.Qty
                              };



            LocalDailyTran = new ObservableCollection<DailySlaughterVM>(COnvertList);

            ListDailySlaughter.ItemsSource = LocalDailyTran;

        }

        public List<Categories> CatList;
        Guid GuidTrans;
        Models.DailyTransaction InsertData = new Models.DailyTransaction();

        ObservableCollection<DailySlaughterVM> LocalDailyTran;

        private async void AddTransactionDaily_Loaded(object sender, RoutedEventArgs e)
        {
            CatList = await new Models.Repositories.CategoriesRepository().List();
            CobCat.ItemsSource = CatList;

        }



        int ValidCounter = 0;
        void DefaultMode()
        {


            CobCat.BorderBrush = Brushes.Gray;
            CobCat.ToolTip = null;

            txtQty.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtQty.ToolTip = null;

            txtPrice.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtPrice.ToolTip = null;

        }


        private void btnAddListDaily_Click(object sender, RoutedEventArgs e)
        {
            ValidCounter = 0;
            DefaultMode();

            if (txtPrice.Text.Length == 0 || Isdouble(txtPrice.Text) == false)
            {
                var TextBox = txtPrice;

                TextBox.FontFamily = new FontFamily(nameof(Validtion.Error));
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }

            if (txtQty.Text.Length == 0 || Isint(txtQty.Text) == false)
            {
                var TextBox = txtQty;

                TextBox.FontFamily = new FontFamily(nameof(Validtion.Error));
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }
            if (CobCat.SelectedIndex == -1)
            {
                var TextBox = CobCat;

                TextBox.BorderBrush = ErrorColor;
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }

            if (ValidCounter != 0) return;

            LocalDailyTran.Add(new DailySlaughterVM
            {
                CatId = CobCat.SelectedValue.GetHashCode(),
                IdForTransaction = GuidTrans,
                Price = double.Parse(txtPrice.Text),
                Qty = double.Parse(txtQty.Text)
            });

            CobCat.SelectedIndex = -1;
            txtPrice.Text = txtQty.Text = "";


        }

        void ValidControlData(Control Control, bool NumberValid = false)
        {
            if (Control is TextBox)
            {
                Control.FontFamily = new FontFamily(nameof(Validtion.Ok));
                Control.ToolTip = null;

                var Text = (TextBox)Control;

                if (NumberValid)
                {
                    if (Text.Text.Length == 0 || Isint(Text.Text) == false)
                    {

                        Text.FontFamily = new FontFamily(nameof(Validtion.Error));
                        Text.ToolTip = GetErrorMessage(Text);
                        ValidCounter += 1;

                    }
                }
                else
                {
                    if (Text.Text.Length == 0)
                    {
                        Text.FontFamily = new FontFamily(nameof(Validtion.Error));
                        Text.ToolTip = GetErrorMessage(Text);
                        ValidCounter += 1;

                    }
                }
            }
            else if (Control is ComboBox)
            {
                var Combo = (ComboBox)Control;

                Combo.BorderBrush = Brushes.Gray;
                Combo.ToolTip = null;

                if (Combo.SelectedIndex == -1)
                {
                    Combo.BorderBrush = ErrorColor;
                    Combo.ToolTip = GetErrorMessage(Combo);
                    ValidCounter += 1;
                }
            }
            else if (Control is DatePicker)
            {
                var datePicker = (DatePicker)Control;

                datePicker.BorderBrush = Brushes.Gray;
                datePicker.ToolTip = null;

                if (IsDate(datePicker.Text) == false)
                {
                    datePicker.BorderBrush = ErrorColor;
                    datePicker.ToolTip = GetErrorMessage(datePicker);
                    ValidCounter += 1;

                }



            }


        }


        private async void BtnSaveTrans_Click(object sender, RoutedEventArgs e)
        {

            if (LocalDailyTran.Count == 0) { MessageBox.Show("يرجى ادخال على الاصل صنف واحد للذبح"); return; }


            ValidCounter = 0;
            DefaultMode();

            ValidControlData(txtEndCashDaily, true);
            ValidControlData(txtDate);


            if (ValidCounter != 0) return;

            //var Daily = await new Models.Repositories.DailyTransactionRepository().List();
            //if (Daily.Where(i => i.Date.Date == DateTime.Now.Date).Count() > 0 && InsertData.Id == 0)
            //{
            //    MessageBox.Show("لا يمكن اضافة اكثر من ذبحة في نفس اليوم يرجى الذهاب الى ذبحة اليوم و التعديل");
            //    return;
            //}


            var COnvertList = from em in LocalDailyTran.ToList()

                              select new Models.DailySlaughter
                              {
                                  IdForTransaction = em.IdForTransaction,
                                  CatId = em.CatId,
                                  Price = em.Price,
                                  Qty = em.Qty
                              };

            await new DbContext()._database.ExecuteAsync($"delete from DailySlaughter where IdForTransaction ='{GuidTrans.ToString()}'");


            await new Models.Repositories.DailySlaughterRepository().Add(COnvertList.ToList());


            InsertData.IdFromSlaughter = GuidTrans;
            InsertData.EndCash = double.Parse(txtEndCashDaily.Text);
            InsertData.Date = txtDate.SelectedDate.Value;


            if (InsertData.Id == 0)
            {
                await new Models.Repositories.DailyTransactionRepository().Add(InsertData);
            }
            else
            {
                await new Models.Repositories.DailyTransactionRepository().Update(InsertData);
            }



            View.NajahEpic.DailyTransaction.Instance.LoadData();


            View.NajahEpic.DailyTransaction.Instance.AddContent.Content = null;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            View.NajahEpic.DailyTransaction.Instance.LoadData();


            View.NajahEpic.DailyTransaction.Instance.AddContent.Content = null;
        }

        private void ListDailySlaughter_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                LocalDailyTran.Remove((DailySlaughterVM)ListDailySlaughter.SelectedItem);
                ListDailySlaughter.ItemsSource = LocalDailyTran;
            }
        }


    }
}
