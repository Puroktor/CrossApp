using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossApp
{
	public partial class NewMaterial : ContentPage
	{
        bool isRasch = false;
        MainPage mp;

		public NewMaterial(MainPage mp)
		{
            InitializeComponent();
            Device.BeginInvokeOnMainThread(() => 
            {
                this.mp = mp;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                picker.SelectedIndex = 0;
            });           
        }       

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            try
            {
                string s = entry.Text;
                ToDemical(s);
                entry.BackgroundColor = Color.Transparent;
            }
            catch
            {
                entry.BackgroundColor = Color.LightPink;
            }
        }

        private decimal ToDemical(string s)
        {
                while (true)
                {
                    if (s == s.Replace(',', '.'))
                        break;
                    else
                        s = s.Replace(',', '.');
                }
                return decimal.Parse(s);
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picker.SelectedIndex == 0)
            {
                a.IsVisible = true;
                a.Placeholder = "Введите значение S";
                b.IsVisible = false;
            }
            else if(picker.SelectedIndex == 1)
            {
                a.IsVisible = true;
                a.Placeholder = "Введите ширину, а";
                b.IsVisible = true;
                b.Placeholder = "Введите длину, b";
            }
            else
            {
                a.IsVisible = true;
                a.Placeholder = "Введите радиус, R";
                b.IsVisible = false;
            }
        }

        private decimal CountS()
        {
            if (picker.SelectedIndex == 0)
            {
                return ToDemical(a.Text);
            }
            else if (picker.SelectedIndex == 1)
            {
                return ToDemical(a.Text) * ToDemical(b.Text);
            }
            else
            {
                return (decimal)Math.PI * ToDemical(a.Text) * ToDemical(a.Text);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            object nameVal = "";
            if (isRasch && name.Text!="" && !(App.Current.Properties.TryGetValue(name.Text, out nameVal)))
            {
                App.Current.Properties.Add(name.Text, module.Text);
                error.Text = "Готово!";
                mp.UpdateBar(this,new EventArgs());
            }
            else
            {
                try
                {
                    if (App.Current.Properties.TryGetValue(name.Text, out nameVal))
                        error.Text = "Материал с данным именем уже имеется";
                    else
                        error.Text = "Сначала рассчитайте модуль Юнга";
                }
                catch
                {
                    error.Text = "Материал с данным именем уже имеется";
                }
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                if (name.Text == null || name.Text == "")
                    error.Text = "Не удалось распознать значения";
                else
                {
                    module.Text = ((ToDemical(fource.Text) * ToDemical(length.Text)) / (ToDemical(delta.Text) * CountS())).ToString();
                    error.Text = "";
                    isRasch = true;
                }
            }
            catch
            {
                error.Text = "Не удалось распознать значения";
            }
        }
    }
}