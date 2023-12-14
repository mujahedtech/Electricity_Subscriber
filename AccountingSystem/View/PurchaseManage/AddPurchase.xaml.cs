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

namespace AccountingSystem.View.PurchaseManage
{
    public class PurchaseListVM : Models.PurchaseList
    {
        public double Total { get => Qty * Price; }

        public string CatName { get => App.CatList.Where(i => i.Id == CatId).FirstOrDefault().CatName; }



    }
    /// <summary>
    /// Interaction logic for AddPurchase.xaml
    /// </summary>
    public partial class AddPurchase : UserControl
    {
        Guid GuidTrans;

        ObservableCollection<PurchaseListVM> LocalPurchase;
        TransactionAccounting InsertData;

        public AddPurchase()
        {
            InitializeComponent();

            Loaded += AddPurchase_Loaded;

            LocalPurchase = new ObservableCollection<PurchaseListVM>();
            GuidTrans = Guid.NewGuid();

            ListPurchaseDataGrid.ItemsSource = LocalPurchase;

            InsertData = new TransactionAccounting();
        }
        public AddPurchase(TransactionAccounting transaction)
        {
            InitializeComponent();

            Loaded += AddPurchase_Loaded;


            var GetDailyTran = new Models.Repositories.PurchaseListRepository().ListFromIDTrans(transaction.TransId);


            var COnvertList = from em in GetDailyTran.Result.ToList()

                              select new PurchaseListVM
                              {
                                  Id = em.Id,
                                  DateEntered = em.DateEntered,
                                  IdForTransaction = em.IdForTransaction,
                                  VendorId = em.VendorId,
                                  InventoryId = em.InventoryId,
                                  CatId = em.CatId,
                                  Qty = em.Qty,
                                  Price = em.Price,
                              };



            LocalPurchase = new ObservableCollection<PurchaseListVM>(COnvertList);
            GuidTrans = transaction.TransId;

            ListPurchaseDataGrid.ItemsSource = LocalPurchase;

            InsertData = transaction;

            MessageBox.Show("حركة تعديل");



        }

        public List<Categories> CatList;



        private void AddPurchase_Loaded(object sender, RoutedEventArgs e)
        {

            CatList = App.CatList.ToList();

            CobCat.ItemsSource = CatList;

            CobInv.ItemsSource = App.InventoryList.ToList();


            CobVendor.ItemsSource = App.AccountList.Where(i => i.idclassAccount == (int)App.AccountType.Vendor).ToList();




            CobInv.SelectedValue = InsertData.InventoryId;
            CobVendor.SelectedValue = InsertData.VendorId;
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


        private void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            ValidCounter = 0;
            DefaultMode();


            ValidControlData(txtPrice, true);
            ValidControlData(txtQty, true);
            ValidControlData(CobCat);

            ValidControlData(CobVendor);
            ValidControlData(CobInv);




            if (ValidCounter != 0) return;

            LocalPurchase.Add(new PurchaseListVM
            {
                CatId = CobCat.SelectedValue.GetHashCode(),
                IdForTransaction = GuidTrans,
                Price = double.Parse(txtPrice.Text),
                Qty = double.Parse(txtQty.Text),
                InventoryId = CobInv.SelectedValue.GetHashCode(),
                VendorId = CobVendor.SelectedValue.GetHashCode(),
            });

            CobCat.SelectedIndex = -1;
            txtPrice.Text = txtQty.Text = "";


        }



        int ValidCounter = 0;
        void DefaultMode()
        {
            CobInv.BorderBrush = Brushes.Gray;
            CobInv.ToolTip = null;

            CobCat.BorderBrush = Brushes.Gray;
            CobCat.ToolTip = null;

            CobVendor.BorderBrush = Brushes.Gray;
            CobVendor.ToolTip = null;


            txtQty.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtQty.ToolTip = null;

            txtPrice.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtPrice.ToolTip = null;

        }


        private async void InsertPurchase_Click(object sender, RoutedEventArgs e)
        {
            if (LocalPurchase.Count == 0) { MessageBox.Show("يرجى ادخال على الاصل صنف واحد للذبح"); return; }


            ValidCounter = 0;
            DefaultMode();



            if (CobVendor.SelectedIndex == -1)
            {
                var TextBox = CobVendor;

                TextBox.BorderBrush = ErrorColor;
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }

            if (CobInv.SelectedIndex == -1)
            {
                var TextBox = CobInv;

                TextBox.BorderBrush = ErrorColor;
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }



            if (ValidCounter != 0) return;


            InsertData.Note = txtNote.Text;
            InsertData.AccountId = CobVendor.SelectedValue.GetHashCode();
            InsertData.TransId = GuidTrans;
            InsertData.Amount = LocalPurchase.Sum(i => i.Total);
            InsertData.TransType = App.AccountTrans.Purchase.GetHashCode();
            InsertData.InventoryId = CobInv.SelectedValue.GetHashCode();
            InsertData.VendorId = CobVendor.SelectedValue.GetHashCode();



            if (InsertData.Id == 0)
            {
                await new Models.Repositories.TransactionAccountingRepository().Add(InsertData);
            }
            else
            {
                await new Models.Repositories.TransactionAccountingRepository().Update(InsertData);
            }



            await new DbContext()._database.ExecuteAsync($"delete from PurchaseList where IdForTransaction ='{GuidTrans.ToString()}'");



            var Convert = from em in LocalPurchase.ToList()

                          select new PurchaseList
                          {
                              Id = em.Id,
                              IdForTransaction = em.IdForTransaction,
                              VendorId = em.VendorId,
                              InventoryId = em.InventoryId,
                              CatId = em.CatId,
                              Qty = em.Qty,
                              Price = em.Price,

                          };



            await new Models.Repositories.PurchaseListRepository().Add(Convert.ToList());

            MainPurchase.Instance.AddContent.Content = null;

        }

        private void ListPurchaseDataGrid_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                LocalPurchase.Remove((PurchaseListVM)ListPurchaseDataGrid.SelectedItem);
                ListPurchaseDataGrid.ItemsSource = LocalPurchase;
            }
        }
    }
}
