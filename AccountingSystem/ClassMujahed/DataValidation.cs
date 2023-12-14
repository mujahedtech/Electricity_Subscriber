using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace AccountingSystem.ClassMujahed
{
    public class DataValidation
    {
        //else if (MujahedClass.Isint(textBox.Text) == false)
        //{


        //     errorMsg = "Category Number is not Valid";
        //    uIElement.Focus();
        //}
        public enum Validtion { Error, Ok, Edit }

        public static bool Isint(string input)
        {
            int test = 0;
            return int.TryParse(input, out test);
        }

        public static bool Isdouble(string input)
        {
            double test = 0;
            return double.TryParse(input, out test);
        }
        public static bool IsDate(string input)
        {
            DateTime test = new DateTime();
            return DateTime.TryParse(input, out test);
        }


        public static Brush ErrorColor = Brushes.Red;
        public enum ControlsName
        {
            txtCatName, txtInvName, txtAccountName, txtMobile, cobAccountClass,
            txtAccountTypeCreate, CobInv, CobCat, CobVendor, txtQty, txtPrice,
            CobAccount, txtCashHouse, txtDate, txtAmount, CobCenterCost,
            txtEndCashDaily, txtRecipient, txtCylinderCount_Out, txtCylinderCount_Receive,
            txtCylinderCount, CobVariableName, txtVariableValue, txtNote
        }
        public static string GetErrorMessage(FrameworkElement uIElement)
        {
            TextBox textBox = new TextBox();
            if (uIElement is TextBox == true)
            {
                textBox = uIElement as TextBox;
            }

            string errorMsg = String.Empty;
            switch (uIElement.Name)
            {

                #region General



                case nameof(ControlsName.txtCatName):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم الصنف فارغ";
                        uIElement.Focus();
                    }
                    break;
                case nameof(ControlsName.CobCat):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم الصنف فارغ";
                        uIElement.Focus();
                    }
                    break;
                case nameof(ControlsName.txtInvName):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم المستودع فارغ";
                        uIElement.Focus();
                    }
                    break;
                case nameof(ControlsName.CobInv):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم المستودع فارغ";
                        uIElement.Focus();
                    }
                    break;
                case nameof(ControlsName.txtAccountName):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم الحساب فارغ";
                        uIElement.Focus();
                    }
                    break;
                case nameof(ControlsName.txtMobile):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك رقم الهاتف فارغ";
                        uIElement.Focus();
                    }
                    else if (Isint(textBox.Text) == false)
                    {

                        errorMsg = "ادخل رقم الهاتف بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;


                case nameof(ControlsName.cobAccountClass):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك تصنيف الحساب فارغ";
                        uIElement.Focus();
                    }
                    break;

                case nameof(ControlsName.txtAccountTypeCreate):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك تصنيف الحساب فارغ";
                        uIElement.Focus();
                    }
                    break;

                case nameof(ControlsName.CobVendor):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم المورد فارغ";
                        uIElement.Focus();
                    }
                    break;

                #endregion


                case nameof(ControlsName.txtQty):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك الكمية فارغ";
                        uIElement.Focus();
                    }
                    else if (Isint(textBox.Text) == false)
                    {

                        errorMsg = "ادخل الكمية بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;

                case nameof(ControlsName.txtPrice):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك السعر فارغ";
                        uIElement.Focus();
                    }
                    else if (Isdouble(textBox.Text) == false)
                    {

                        errorMsg = "ادخل السعر بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;

                case nameof(ControlsName.CobAccount):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم الحساب فارغ";
                        uIElement.Focus();
                    }

                    break;

                case nameof(ControlsName.txtCashHouse):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم الصندوق فارغ";
                        uIElement.Focus();
                    }

                    break;
                case nameof(ControlsName.CobCenterCost):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك مركز الكلفة فارغ";
                        uIElement.Focus();
                    }

                    break;
                case nameof(ControlsName.txtDate):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك التاريخ فارغ";
                        uIElement.Focus();
                    }

                    break;


                case nameof(ControlsName.txtAmount):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك القيمة فارغ";
                        uIElement.Focus();
                    }
                    else if (Isdouble(textBox.Text) == false)
                    {

                        errorMsg = "ادخل القيمة بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;

                case nameof(ControlsName.txtEndCashDaily):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك قيمة الغلة فارغ";
                        uIElement.Focus();
                    }
                    else if (Isdouble(textBox.Text) == false)
                    {

                        errorMsg = "ادخل قيمة الغلة بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;


                case nameof(ControlsName.txtRecipient):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك اسم المستلم فارغ";
                        uIElement.Focus();
                    }

                    break;
                case nameof(ControlsName.txtCylinderCount_Out):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك تسليم اسطوانة فارغ";
                        uIElement.Focus();
                    }
                    else if (Isdouble(textBox.Text) == false)
                    {

                        errorMsg = "ادخل تسليم اسطوانة بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;
                case nameof(ControlsName.txtCylinderCount_Receive):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك استلام اسطوانة فارغ";
                        uIElement.Focus();
                    }
                    else if (Isdouble(textBox.Text) == false)
                    {

                        errorMsg = "ادخل استلام اسطوانة بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;
                case nameof(ControlsName.txtCylinderCount):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك عدد الاسطوانات فارغ";
                        uIElement.Focus();
                    }
                    else if (Isdouble(textBox.Text) == false)
                    {

                        errorMsg = "ادخل عدد الاسطوانات بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;

                case nameof(ControlsName.CobVariableName):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك نوع المتغير فارغ";
                        uIElement.Focus();
                    }

                    break;
                case nameof(ControlsName.txtVariableValue):

                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        errorMsg = "لا يمكن ترك قيمة المتغير فارغ";
                        uIElement.Focus();
                    }
                    else if (Isdouble(textBox.Text) == false)
                    {

                        errorMsg = "ادخل قيمة المتغير بشكل صحيح";
                        uIElement.Focus();
                    }
                    break;


            }

            return errorMsg;

        }

    }

}
