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
        StackLayout layout;
        ListaMateriaVM vm;
        ListView lvMaterias = new ListView();
        DataTemplate template = new DataTemplate(() =>
        {
            StackLayout stack = new StackLayout { Orientation = StackOrientation.Horizontal };
            Label lbNome = new Label { TextColor = Color.White };
            lbNome.SetBinding(Label.TextProperty, "nome");
            stack.Children.Add(lbNome);
            return new ViewCell { View = stack };
        });
        
        public ListaMateriaPage()
        {
            vm = new ListaMateriaVM(this.Navigation);
            this.BindingContext = vm;
            ToolbarItems.Add(new ToolbarItem("+", null, addMateria));
            //ToolbarItems.Add(new ToolbarItem("-", null, removerMateria));
            
            
            layout = new StackLayout
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
        
        void addMateria() { Navigation.PushAsync(new AddMateria()); }
        //void removerMateria() { Navigation.PushAsync(new RemoverMateria()); }
        
    }

}

