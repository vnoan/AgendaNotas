using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
using AgendaNotas.ViewModel;
using Xamarin.Forms;
using System.Diagnostics;

namespace AgendaNotas.View
{
    public class ListaMateriaPage : ContentPage
    {
        private StackLayout _layout;
        private ListaMateriaVM _vm;
        private ListView lvMaterias = new ListView();
        private DataTemplate template = new DataTemplate(() =>
        {
            StackLayout stack = new StackLayout { Orientation = StackOrientation.Horizontal };
            Label lbNome = new Label { TextColor = Color.White, FontSize = 17, VerticalOptions = LayoutOptions.Center };
            Label lbMedia = new Label { HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center };
            lbNome.SetBinding(Label.TextProperty, "Nome");
            lbMedia.SetBinding(Label.TextProperty, "Media");
            stack.Children.Add(lbNome);
            stack.Children.Add(lbMedia);
            return new ViewCell { View = stack };
        });
        
        public ListaMateriaPage()
        {
            _vm = new ListaMateriaVM(this.Navigation);
            BindingContext = _vm;
            Padding = 10;
            ToolbarItems.Add(new ToolbarItem("+", null, _vm.AddMateria));
            
            _layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = {lvMaterias}
            };

            Content = lvMaterias;
            Title = "Materias";

            lvMaterias.ItemTemplate = template;
            lvMaterias.ItemSelected += ((sender, e) => lvMaterias.SelectedItem = null);
            lvMaterias.SetBinding(ListView.ItemsSourceProperty, "ListaMateria");
            lvMaterias.SetBinding(ListView.SelectedItemProperty, "materia");
          
        }
        
        
    }

}

