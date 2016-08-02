using AgendaNotas.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AgendaNotas.View
{
    public abstract class OperacaoMateria : ContentPage
    {
        protected Entry eNome = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand };
        protected Button bAction = new Button();
        protected Label lbLog = new Label { TextColor = Color.Red, VerticalOptions = LayoutOptions.End };
        protected Grid Grid = new Grid();

        public OperacaoMateria()
        {

            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 10,
                Children = {
                    //Add o stack horizontal
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            new Label { Text = "Nome: ", VerticalOptions = LayoutOptions.Center },
                            eNome
                        }
                    },                    
                    //Volta pro stack vertical
                    Grid,
                    bAction,
                    lbLog
                }
            };


        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            eNome.Focus();
        }

    }

        
    
    
}
