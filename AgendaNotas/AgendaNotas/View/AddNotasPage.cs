using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
using Xamarin.Forms;
using System.Diagnostics;
using System.Globalization;

namespace AgendaNotas.View
{
	public class ProvasPage : ContentPage
	{
        Materia mat;
        Button bConfirma = new Button { Text = "Confirma" };
        Label lbNota = new Label { Text = "Nota: ", VerticalOptions = LayoutOptions.Center };
        Entry eNota = new Entry { Text = "0", Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.FillAndExpand };
        Label lbPeso = new Label { Text = "Peso: ", VerticalOptions = LayoutOptions.Center };
        Entry ePeso = new Entry { Text = "1", Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.FillAndExpand };
        Label lbLog = new Label { TextColor = Color.Red, VerticalOptions = LayoutOptions.End };

        public ProvasPage (Materia m)
		{
            mat = m;
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 30,
                Children = {
                    //Add o stack horizontal
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            lbNota,
                            eNota
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            lbPeso,
                            ePeso
                        }
                    },
                    //Volta pro stack vertica
                    bConfirma,
                    lbLog }
            };
            bConfirma.Clicked += BConfirma_Clicked;
		}

        private void BConfirma_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(eNota.Text) || string.IsNullOrEmpty(ePeso.Text) ||
                int.Parse(ePeso.Text) > 10 || int.Parse(ePeso.Text) <= 0 ||
                float.Parse(eNota.Text, CultureInfo.InvariantCulture) > 10 || float.Parse(eNota.Text, CultureInfo.InvariantCulture) <= 0)
            {
                Debug.Write(float.Parse(eNota.Text, CultureInfo.InvariantCulture));
                lbLog.Text = "PROVA NÃO ADICIONADA";
                return;
            }
            
            int peso = int.Parse(ePeso.Text);
            float nota = float.Parse(eNota.Text, CultureInfo.InvariantCulture);
            mat.addProva(nota, peso);
            Navigation.PopAsync();
        }
    }
}
