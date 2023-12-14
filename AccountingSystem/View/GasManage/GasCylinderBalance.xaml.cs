using AccountingSystem.Models;
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
using static AccountingSystem.ClassMujahed.DataValidation;

namespace AccountingSystem.View.GasManage
{
    public class GasCylinderBalanceVM : GasCylinderBalanceTBL
    {

        public string AccountName { get; set; }
        public string InventoryName { get; set; }

    }
    /// <summary>
    /// Interaction logic for GasCylinderBalance.xaml
    /// </summary>
    public partial class GasCylinderBalance : UserControl
    {
        public GasCylinderBalance()
        {
            InitializeComponent();

            Loaded += GasCylinderBalance_Loaded;
        }

        List<GasCylinderBalanceTBL> TransList;
        private void GasCylinderBalance_Loaded(object sender, RoutedEventArgs e)
        {
            CobAccount.ItemsSource = App.AccountList.ToList().Where(i => i.idclassAccount == (int)App.AccountType.GasBalance);
            CobInv.ItemsSource = App.InventoryList.Where(i => i.IsGasInv).ToList();

            LoadData();
        }


        async void LoadData()
        {



            TransList = await new Models.Repositories.GasCylinderBalanceRepository().List();

            var results = from Trans in TransList.AsEnumerable()
                          join Accounts in App.AccountList.AsEnumerable() on Trans.IdAccount equals Accounts.Id
                          join Inventory in App.InventoryList on Trans.IdInv equals Inventory.Id
                          select new GasCylinderBalanceVM
                          {
                              Id = Trans.Id,
                              CylinderCount = Trans.CylinderCount,
                              Date = Trans.Date,
                              IdAccount = Trans.IdAccount,
                              IdInv = Trans.IdInv,
                              Note = Trans.Note,
                              Recipient = Trans.Recipient,
                              AccountName = Accounts.AccountName,
                              InventoryName = Inventory.InvName


                          };



            DataGridListTrans.ItemsSource = results;


        }

        int ValidCounter = 0;
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


        GasCylinderBalanceTBL InsertData = new GasCylinderBalanceTBL();
        private async void btnADD_Click(object sender, RoutedEventArgs e)
        {

            ValidCounter = 0;

            ValidControlData(txtCylinderCount, true);
            ValidControlData(txtRecipient);
            ValidControlData(CobInv);
            ValidControlData(CobAccount);
            ValidControlData(txtDate);







            if (ValidCounter != 0) return;

            InsertData.Note = txtNote.Text;
            InsertData.Date = txtDate.SelectedDate.Value;
            InsertData.Recipient = txtRecipient.Text;
            InsertData.CylinderCount = int.Parse(txtCylinderCount.Text);
            InsertData.IdInv = CobInv.SelectedValue.GetHashCode();
            InsertData.IdAccount = CobAccount.SelectedValue.GetHashCode();


            if (InsertData.Id == 0) await new Models.Repositories.GasCylinderBalanceRepository().Add(InsertData);

            else await new Models.Repositories.GasCylinderBalanceRepository().Update(InsertData);

            LoadData();

            CobAccount.SelectedIndex = -1;
            CobInv.SelectedIndex = -1;
            txtDate.SelectedDate = DateTime.Now;
            txtNote.Text =
            txtRecipient.Text =
            txtCylinderCount.Text = "";

        }

        private void DataGridListTrans_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridListTrans.SelectedItem == null) return;

            var item = (GasCylinderBalanceVM)DataGridListTrans.SelectedItem;


            InsertData = new Models.GasCylinderBalanceTBL
            {
                Id = item.Id,
                CylinderCount = item.CylinderCount,
                Date = item.Date,
                Recipient = item.Recipient,
                IdAccount = item.IdAccount,
                IdInv = item.IdInv,
                Note = item.Note
            };


            CobAccount.SelectedValue = InsertData.IdAccount;
            CobInv.SelectedValue = InsertData.IdInv;
            txtDate.SelectedDate = InsertData.Date;
            txtNote.Text = InsertData.Note;
            txtRecipient.Text = InsertData.Recipient;
            txtCylinderCount.Text = InsertData.CylinderCount.ToString();
        }
    }
}
