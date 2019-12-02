using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Cripto_Lab4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static string HashCode(string s, string opt)
        {
            HashAlgorithm h;

            switch (opt)
            {
                case "MD5": h = MD5.Create(); break;
                case "SHA1": h = SHA1.Create(); break;
                case "SHA256": h = SHA256.Create(); break;
                case "SHA384": h = SHA384.Create(); break;
                case "SHA512": h = SHA512.Create(); break;
                default: throw new Exception("Optiune invalida!");
            }

            byte[] result = h.ComputeHash(Encoding.UTF8.GetBytes(s));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                sb.Append(result[i].ToString("x2"));

            return sb.ToString();
        }

        public static async Task<string> GetFile(string file)
        {
            string aux;
            using (StreamReader sr = new StreamReader(file))
            {
                aux = await sr.ReadToEndAsync();
            }
            return aux;
        }

        public MainPage()
        {
            this.InitializeComponent();
            string[] options = { "MD5", "SHA1", "SHA256", "SHA384", "SHA512" };
            HashTypeSelector.ItemsSource = options;
        }

        private async void HashBtn_Click(object sender, RoutedEventArgs e)
        {
            string filepath, filecontent;
            filepath = Input.Text;
            filecontent = await GetFile(filepath);
            Output.Text = HashCode(filecontent, (string)HashTypeSelector.SelectedItem);
        }
    }
}
