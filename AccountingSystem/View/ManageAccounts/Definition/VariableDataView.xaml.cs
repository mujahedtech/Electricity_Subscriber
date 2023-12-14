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

namespace AccountingSystem.View.ManageAccounts.Definition
{
    /// <summary>
    /// Interaction logic for VariableDataView.xaml
    /// </summary>
    public partial class VariableDataView : UserControl
    {
        public VariableDataView()
        {
            InitializeComponent();

            Loaded += VariableDataView_Loaded;
        }

        async void LoadVariable()
        {
            CobVariableName.ItemsSource = null;
            var Cat = await new Models.Repositories.VariableDataRepository().List();

            CobVariableName.ItemsSource = Cat;
        }
        private void VariableDataView_Loaded(object sender, RoutedEventArgs e)
        {
            LoadVariable();
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
                        txtMessage.Text = GetErrorMessage(Text);
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
                        txtMessage.Text = GetErrorMessage(Text);

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
                    txtMessage.Text = GetErrorMessage(Combo);
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
                    txtMessage.Text = GetErrorMessage(datePicker);
                    ValidCounter += 1;

                }



            }


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ValidCounter = 0;

            ValidControlData(CobVariableName);
            ValidControlData(txtVariableValue, true);


            if (ValidCounter != 0) return;


            var Data = (VariableDataTbl)CobVariableName.SelectedItem;

            if (Data != null) new Models.Repositories.VariableDataRepository().Update(Data);


            CobVariableName.SelectedIndex = -1;
            txtNote.Text = txtVariableValue.Text = "";





            LoadVariable();


        }
    }

}
