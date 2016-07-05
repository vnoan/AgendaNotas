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
        public Entry eNome = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand };
        public Button bAction = new Button();
        public Label lbLog = new Label { TextColor = Color.Red, VerticalOptions = LayoutOptions.End };
        public Grid grid = new Grid();

        public OperacaoMateria()
        {
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
                            new Label { Text = "Nome: ", VerticalOptions = LayoutOptions.Center },
                            eNome
                        }
                    },                    
                    //Volta pro stack vertical
                    grid,
                    bAction,
                    lbLog
                }
            };
            BackgroundColor = Color.Gray;
            eNome.Focus();
            
            
        }

    }
    
	public class AddMateria : OperacaoMateria
    {
        Entry qtNota = new Entry { Text = "1" };
        public AddMateria() : base()
        {
            Title = "Adicionar Materias";
            grid.Children.Add(new Label { Text = "Quantidade de notas:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start }, 0, 0);
            grid.Children.Add(qtNota, 1, 0);

            bAction.Clicked += BAdd_Clicked;
            bAction.Text = "Adicionar Matéria";
        }

        private void BAdd_Clicked(object sender, EventArgs e)
        {
            foreach (Materia m in App.Materias)
            {
                if (string.IsNullOrEmpty(eNome.Text) || m.nome == eNome.Text)
                {
                    lbLog.Text = "MATÉRIA NÃO ADICIONADA";
                    return;
                }
            }

            App.Materias.Add(new Materia(eNome.Text, int.Parse(qtNota.Text)));
            Navigation.PopToRootAsync();
        }
    
    }

    public class RemoverMateria : OperacaoMateria
    {
        public RemoverMateria()
        {
            Title = "Remover Materias";
            bAction.Clicked += BRemove_Clicked;
            bAction.Text = "Remover Matéria";
        }

        private void BRemove_Clicked(object sender, EventArgs e)
        {
            foreach (Materia m in App.Materias)
            {
                if (m.nome.ToLower().Trim() == eNome.Text.ToLower().Trim())
                {
                    App.Materias.Remove(m);
                    Navigation.PopToRootAsync();
                    return;
                }
            }
            lbLog.Text = "MATÉRIA NÃO REMOVIDA";
        }
    }

    public class EditarMateria : OperacaoMateria
    {
        Materia m;
        public EditarMateria(Materia m)
        {
            this.m = m;
            eNome.Text = m.nome;
            Title = "Editar " + m.nome;
            bAction.Clicked += BConfirma_Clicked;
            bAction.Text = "Confirmar";
            ToolbarItems.Add(new ToolbarItem("+ Nota", null, addNota));

            int left = 0;
            int top = 0;
            foreach(Nota p in m.provas)
            {
                this.grid.Children.Add(new Label { Text = "Nota:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start }, left, top);
                this.grid.Children.Add(new Entry { Text = p.valor.ToString(), Keyboard = Keyboard.Numeric }, left + 1, top);
                this.grid.Children.Add(new Label { Text = "Peso:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start }, left+2, top);
                this.grid.Children.Add(new Entry { Text = p.peso.ToString(), Keyboard = Keyboard.Numeric }, left+3, top);
                top++;

            }
            
        }

        private void BConfirma_Clicked(object sender, EventArgs e)
        {
            //Falta salvar as alterações que o usuário faz nas notas!
            var mat = App.Materias.First(mt => mt.nome == m.nome);

            if (mat != null) { mat.nome = eNome.Text; }
            else { lbLog.Text = "NÃO EDITADO"; return;}

            Navigation.PopToRootAsync();
        }

        private void addNota()
        {
            Navigation.PushAsync(new ProvasPage(m));
        }
    }
}
