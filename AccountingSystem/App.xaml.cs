using AccountingSystem.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AccountingSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

      


        //نوع الحساب عند تعريفة بشجرة الحسابات

        //                          0      1        2        3          4              5            6
        public enum AccountType { Zero, Vendor, Customer, Expense, Acc_Account, PeronalAccount, GasBalance }

        //نوع الحركة مثال شراء مصاريف سند قبض

        //A_R يعني قيد دين حدا اخذ من الملحمة مثلا على الدين

        //                           0       1        2        3       4    5
        public enum AccountTrans { Zero, Purchase, Expense, Revenue, A_R ,Sales}


        public static string GetAccountTransStr(int id)
        {
            string str = string.Empty;
            switch (id)
            {

                case 1:
                    str = "شراء";

                    break;
                case 2:
                    str = "صرف";

                    break;
                case 3:
                    str = "قبض";

                    break;
                case 4:
                    str = "شراء بالدين";

                    break;
                case 5:
                    str = "مبيعات";

                    break;
            }
            return str;
        }

        public static List<Categories> CatList;
        public static List<AccountsTable> AccountList;
        public static List<Inventory> InventoryList;
        protected override async void OnStartup(StartupEventArgs e)
        {
           



            var AccountClass = await new Models.Repositories.AccountClassRepository().List();
            var VariableData = await new Models.Repositories.VariableDataRepository().List();


            if (AccountClass.Count == 0)
            {

                List<AccountClass> AccountClassDefult = new List<AccountClass>();



                AccountClassDefult.Add(new AccountClass { Id = 1, DateEntered = DateTime.Now, ClassName = "موردين" });
                AccountClassDefult.Add(new AccountClass { Id = 2, DateEntered = DateTime.Now, ClassName = "زبائن" });
                AccountClassDefult.Add(new AccountClass { Id = 3, DateEntered = DateTime.Now, ClassName = "مصاريف" });
                AccountClassDefult.Add(new AccountClass { Id = 4, DateEntered = DateTime.Now, ClassName = "حسابات مالية" });
                AccountClassDefult.Add(new AccountClass { Id = 5, DateEntered = DateTime.Now, ClassName = "حسابات شخصية" });
                AccountClassDefult.Add(new AccountClass { Id = 6, DateEntered = DateTime.Now, ClassName = "حسابات غاز" });

                await new Models.Repositories.AccountClassRepository().Add(AccountClassDefult);


                //new Models.Repositories.AccountClassRepository(App.dbContext).Add(new AccountClass {Id=1, DateEntered=DateTime.Now,ClassName="موردين"});
                //new Models.Repositories.AccountClassRepository(App.dbContext).Add(new AccountClass { Id = 2, DateEntered =DateTime.Now,ClassName="زبائن"});
                //new Models.Repositories.AccountClassRepository(App.dbContext).Add(new AccountClass { Id = 3,DateEntered = DateTime.Now,ClassName="مصاريف"});
                //new Models.Repositories.AccountClassRepository(App.dbContext).Add(new AccountClass { Id = 4,DateEntered = DateTime.Now,ClassName="حسابات مالية"});
            }

            if (VariableData.Count == 0) await new Models.Repositories.VariableDataRepository().Add(new VariableDataTbl { VariableName = "عدد الغنم الكبيرة", VairableValue = 0 });

            CatList = await new Models.Repositories.CategoriesRepository().List();
            AccountList = await new Models.Repositories.AccountsTableRepository().List();
            InventoryList = await new Models.Repositories.InventoryRepository().List();


            if (InventoryList.ToList().Count == 0)
            {
                List<Inventory> InventoryDefault = new List<Inventory>();



                InventoryDefault.Add(new Inventory { Id = 1, InvName = "ملحمة النجاح" });


                await new Models.Repositories.InventoryRepository().Add(InventoryDefault);
            }

            

            base.OnStartup(e);
        }


        public static async  void UpdateList()
        {
            CatList = await new Models.Repositories.CategoriesRepository().List();
            AccountList = await new Models.Repositories.AccountsTableRepository().List();
            InventoryList = await new Models.Repositories.InventoryRepository().List();

        }
    }
}
