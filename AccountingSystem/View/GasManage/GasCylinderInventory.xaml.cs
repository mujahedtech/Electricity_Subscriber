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
    public class GasCylinderInventoryVM : GasCylinderInventoryTbl
    {

        public string AccountName { get; set; }
        public string InventoryName { get; set; }

    }
    /// <summary>
    /// Interaction logic for GasCylinderInventory.xaml
    /// </summary>
    public partial class GasCylinderInventory : UserControl
    {
        public GasCylinderInventory()
        {
            InitializeComponent();

            Loaded += GasCylinderInventory_Loaded;
        }


        List<GasCylinderInventoryTbl> TransList;
        private void GasCylinderInventory_Loaded(object sender, RoutedEventArgs e)
        {
            CobAccount.ItemsSource = App.AccountList.ToList().Where(i => i.idclassAccount == (int)App.AccountType.GasBalance);
            CobInv.ItemsSource = App.InventoryList.Where(i => i.IsGasInv).ToList();

            LoadData();
        }


        async void LoadData()
        {
            TransList = await new Models.Repositories.GasCylinderInventoryRepository().List();


            var results = from Trans in TransList.AsEnumerable()
                          join Accounts in App.AccountList.AsEnumerable() on Trans.IdAccount equals Accounts.Id
                          join Inventory in App.InventoryList on Trans.IdInv equals Inventory.Id
                          select new GasCylinderInventoryVM
                          {
                              Id = Trans.Id,
                              CylinderCount_Out = Trans.CylinderCount_Out,
                              CylinderCount_Receive = Trans.CylinderCount_Receive,
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


        GasCylinderInventoryTbl InsertData = new GasCylinderInventoryTbl();
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

        private async void btnADD_Click(object sender, RoutedEventArgs e)
        {
            ValidCounter = 0;

            ValidControlData(txtCylinderCount_Receive, true);
            ValidControlData(txtCylinderCount_Out, true);
            ValidControlData(txtRecipient);
            ValidControlData(CobInv);
            ValidControlData(CobAccount);
            ValidControlData(txtDate);







            if (ValidCounter != 0) return;







            InsertData.Date = txtDate.SelectedDate.Value;
            InsertData.IdAccount = CobAccount.SelectedValue.GetHashCode();
            InsertData.IdInv = CobInv.SelectedValue.GetHashCode();
            InsertData.Recipient = txtRecipient.Text;
            InsertData.CylinderCount_Receive = int.Parse(txtCylinderCount_Receive.Text);
            InsertData.CylinderCount_Out = int.Parse(txtCylinderCount_Out.Text);
            InsertData.Note = txtNote.Text;


            if (InsertData.Id == 0) await new Models.Repositories.GasCylinderInventoryRepository().Add(InsertData);

            else await new Models.Repositories.GasCylinderInventoryRepository().Update(InsertData);

            LoadData();

            CobAccount.SelectedIndex = -1;
            CobInv.SelectedIndex = -1;
            txtDate.SelectedDate = DateTime.Now;
            txtNote.Text =
            txtRecipient.Text =
            txtCylinderCount_Receive.Text =
            txtCylinderCount_Out.Text = "";

        }

        private void DataGridListTrans_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridListTrans.SelectedItem == null) return;

            var item = (GasCylinderInventoryVM)DataGridListTrans.SelectedItem;

            InsertData = new Models.GasCylinderInventoryTbl
            {
                Id = item.Id,
                CylinderCount_Out = item.CylinderCount_Out,
                CylinderCount_Receive = item.CylinderCount_Receive,
                Date = item.Date,
                IdAccount = item.IdAccount,
                IdInv = item.IdInv,
                Note = item.Note,
                Recipient = item.Recipient,
            };


            CobAccount.SelectedValue = InsertData.IdAccount;
            CobInv.SelectedValue = InsertData.IdInv;
            txtDate.SelectedDate = InsertData.Date;
            txtNote.Text = InsertData.Note;
            txtRecipient.Text = InsertData.Recipient;
            txtCylinderCount_Receive.Text = InsertData.CylinderCount_Receive.ToString();
            txtCylinderCount_Out.Text = InsertData.CylinderCount_Out.ToString();




        }
    }
}
