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

using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Mujahed_Package.Layouts
{
    /// <summary>
    /// Interaction logic for ArchiveManager.xaml
    /// </summary>
    public partial class ArchiveManager : UserControl
    {
        public ArchiveManager()
        {
            InitializeComponent();

            //if (MujahedClasses.Validation.IsDate("")== true)
            //{

            //}
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            TwilioClient.Init("AC92894d676caa67af87f67495a896b832", "4797175b91a68490b0932a4655a12a05");


           
                string text = Console.ReadLine();
                var mess =await MessageResource.CreateAsync(from: "+12602543309", to: "+962790931724", body: txtMessage.Text);

            listMessage.Items.Add(txtMessage.Text);
            txtMessage.Text = "";
            txtMessage.Focus();
        }
    }
}
