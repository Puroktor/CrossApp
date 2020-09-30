using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossApp
{
	public partial class Material_Page : TabbedPage
    {
        BarElement br;
        MainPage tmp;
		public Material_Page(BarElement barElement,MainPage mp)
        {
            InitializeComponent();
            Device.BeginInvokeOnMainThread(() => 
            {
                br = barElement;
                tmp = mp;
                module.Text = br.Module;
                module1.Text = br.Module;
                module2.Text = br.Module;
                module3.Text = br.Module;
                picker.SelectedIndex = 0;
                picker1.SelectedIndex = 0;
                picker3.SelectedIndex = 0;
            });
        }


        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picker.SelectedIndex == 0)
            {
                a.IsVisible = true;
                a.Placeholder = "Введите значение S";
                b.IsVisible = false;
            }
            else if (picker.SelectedIndex == 1)
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                fource.Text =((ToDemical(module.Text)*ToDemical(delta.Text)*CountS())/ToDemical(length.Text)).ToString();
                error.Text = "";
            }
            catch
            {
                error.Text = "Введите все знчаения";
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

        private void picker1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picker1.SelectedIndex == 0)
            {
                a1.IsVisible = true;
                a1.Placeholder = "Введите значение S";
                b1.IsVisible = false;
            }
            else if (picker1.SelectedIndex == 1)
            {
                a1.IsVisible = true;
                a1.Placeholder = "Введите ширину, а";
                b1.IsVisible = true;
                b1.Placeholder = "Введите длину, b";
            }
            else
            {
                a1.IsVisible = true;
                a1.Placeholder = "Введите радиус, R";
                b1.IsVisible = false;
            }
        }

        private void picker3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picker3.SelectedIndex == 0)
            {
                a3.IsVisible = true;
                a3.Placeholder = "Введите значение S";
                b3.IsVisible = false;
            }
            else if (picker3.SelectedIndex == 1)
            {
                a3.IsVisible = true;
                a3.Placeholder = "Введите ширину, а";
                b3.IsVisible = true;
                b3.Placeholder = "Введите длину, b";
            }
            else
            {
                a3.IsVisible = true;
                a3.Placeholder = "Введите радиус, R";
                b3.IsVisible = false;
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                length1.Text = ((ToDemical(module1.Text) * ToDemical(delta1.Text) * CountS1()) / ToDemical(fource1.Text)).ToString();
                error1.Text = "";
            }
            catch
            {
                error1.Text = "Введите все знчаения";
            }
        }

        private decimal CountS1()
        {
            if (picker1.SelectedIndex == 0)
            {
                return ToDemical(a1.Text);
            }
            else if (picker1.SelectedIndex == 1)
            {
                return ToDemical(a1.Text) * ToDemical(b1.Text);
            }
            else
            {
                return (decimal)Math.PI * ToDemical(a1.Text) * ToDemical(a1.Text);
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            try
            {
                picker2.Text = ((ToDemical(fource2.Text)*ToDemical(length2.Text))/(ToDemical(module2.Text)*ToDemical(delta2.Text))).ToString();
                error2.Text = "";
            }
            catch
            {
                error2.Text = "Введите все знчаения";
            }
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            try
            {
                delta3.Text = ((ToDemical(fource3.Text) * ToDemical(length3.Text)) / (ToDemical(module3.Text) * CountS3())).ToString();
                error3.Text = "";
            }
            catch
            {
                error3.Text = "Введите все знчаения";
            }
        }

        private decimal CountS3()
        {
            if (picker3.SelectedIndex == 0)
            {
                return ToDemical(a3.Text);
            }
            else if (picker3.SelectedIndex == 1)
            {
                return ToDemical(a3.Text) * ToDemical(b3.Text);
            }
            else
            {
                return (decimal)Math.PI * ToDemical(a3.Text) * ToDemical(a3.Text);
            }
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            App.Current.Properties.Remove(br.Title);
            tmp.UpdateBar(this,new EventArgs());
            tmp.Detail = new NavigationPage(new Start_Page());
        }

               async void Button_Clicked_5(object sender, EventArgs e)
               {
                    fource.Text = ((ToDemical(module.Text) * ToDemical(delta.Text) * CountS()) / ToDemical(length.Text)).ToString();
                    string filename = br.Title + ".csv";
                    if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
                    {
                        bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                        if (isRewrited == false) return;
                    }
                    string path = await DependencyService.Get<IFileWorker>().SaveTextAsync(filename, "E, Па ; S, м; Δl, м; F, м; l, m \n"+br.Module+";"+ CountS()+ ";" + ToDemical(delta.Text) + ";" + ToDemical(fource.Text) + ";" + ToDemical(length.Text));
                    error.Text = path;
               }

        async void Button_Clicked_6(object sender, EventArgs e)
        {
            try
            {
                length1.Text = ((ToDemical(module1.Text) * ToDemical(delta1.Text) * CountS1()) / ToDemical(fource1.Text)).ToString();
                string filename = br.Title + ".csv";
                if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
                {
                    bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                    if (isRewrited == false) return;
                }
                string path = await DependencyService.Get<IFileWorker>().SaveTextAsync(filename, "E, Па ; S, м; Δl, м; F, м; l, m \n" + br.Module + ";" + CountS1() + ";" + ToDemical(delta1.Text) + ";" + ToDemical(fource1.Text) + ";" + ToDemical(length1.Text));
                error.Text = path;
            }
            catch
            {
                error1.Text = "Введите все знчаения";
            }
        }

        async void Button_Clicked_7(object sender, EventArgs e)
        {
            try
            {
                picker2.Text = ((ToDemical(fource2.Text) * ToDemical(length2.Text)) / (ToDemical(module2.Text) * ToDemical(delta2.Text))).ToString();
                string filename = br.Title + ".csv";
                if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
                {
                    bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                    if (isRewrited == false) return;
                }
                string path = await DependencyService.Get<IFileWorker>().SaveTextAsync(filename, "E, Па ; S, м; Δl, м; F, м; l, m \n" + br.Module + ";" + picker2.Text + ";" + ToDemical(delta2.Text) + ";" + ToDemical(fource2.Text) + ";" + ToDemical(length2.Text));
                error.Text = path;
            }
            catch
            {
                error2.Text = "Введите все знчаения";
            }
        }

        async void Button_Clicked_8(object sender, EventArgs e)
        {
            try
            {
                delta3.Text = ((ToDemical(fource3.Text) * ToDemical(length3.Text)) / (ToDemical(module3.Text) * CountS3())).ToString();
                string filename = br.Title + ".csv";
                if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
                {
                    bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                    if (isRewrited == false) return;
                }
                string path = await DependencyService.Get<IFileWorker>().SaveTextAsync(filename, "E, Па ; S, м; Δl, м; F, м; l, m \n" + br.Module + ";" + CountS3() + ";" + ToDemical(delta3.Text) + ";" + ToDemical(fource3.Text) + ";" + ToDemical(length3.Text));
                error3.Text = path;
            }
            catch
            {
                error3.Text = "Введите все знчаения";
            }
        }
    }
}