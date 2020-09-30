using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace CrossApp
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            Detail = new NavigationPage(new Start_Page());
            Appearing+=UpdateBar;
        }

        public void UpdateBar(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                List<BarElement> collection = new List<BarElement>();

                foreach (var element in App.Current.Properties)
                {
                    collection.Add(new BarElement { Title = element.Key, Module = (string)element.Value });
                }
                var result = from item in collection
                             orderby item.Title
                             select item;

                List<BarElement> bars = result.ToList();
                bars.Add(new BarElement { Title = "Добавить новый" });
                mainList.ItemsSource = bars;
            });
        }

        void Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var lw = sender as ListView;
            try
            {
                if (((BarElement)lw.SelectedItem).Title != "Добавить новый")
                {
                    EditMat(e);
                }
                else
                {
                    NewMat();
                }
                lw.SelectedItem = null;
            }

            catch { lw.SelectedItem = null; }
        }

        private void EditMat(SelectedItemChangedEventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new Material_Page((BarElement)e.SelectedItem, this));
        }

        private void NewMat()
        {
            IsPresented = false;
            Detail = new NavigationPage(new NewMaterial(this));

        }
    }
}
