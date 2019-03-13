using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCompleteExemplo.ViewModel;
using Xamarin.Forms;
using dotMorten.Xamarin.Forms;

namespace AutoCompleteExemplo
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel ViewModel => (MainViewModel)BindingContext;

        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new MainViewModel();
        }

        //Evento de AutoComplete
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            AutoSuggestBox box = (AutoSuggestBox)sender;

            if (e.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                    box.ItemsSource = null;
                else
                {
                    var suggestions = ViewModel.ObterSugestoes(box.Text);
                    box.ItemsSource = suggestions.ToList();
                }
            }
        }

        //Sugestao Selecionada
        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs e)
        {
           var itemselecionado = e.SelectedItem;
            ViewModel.Selecionado = $"Item Selecionado: {itemselecionado}";
        }

        //Evento para pegar se foi utilizada a opçao do usuario ou do auto complete
        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            if (e.ChosenSuggestion == null)
            {
                ViewModel.Pesquisa = $"Pesquisa: {e.QueryText} ";
                ViewModel.Selecionado = "Item Selecionado: Nenhum";
            }
            else
                ViewModel.Pesquisa = $"Sugestao: {e.ChosenSuggestion}";
        }
    }
}
