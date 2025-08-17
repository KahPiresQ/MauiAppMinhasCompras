using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        public ListaProduto()
        {
            InitializeComponent(); // Carrega XAML
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Carregar(forceRebind: true); // Carrega sempre que a tela aparece
        }

        // Busca dados do banco e atualiza a lista
        private async Task Carregar(bool forceRebind = false)
        {
            var dados = await App.Db.GetAll();

            var lista = this.FindByName<CollectionView>("Lista");
            var vazio = this.FindByName<Label>("Vazio");

            if (lista != null)
            {
                if (forceRebind) lista.ItemsSource = null; // Força refresh visual
                lista.ItemsSource = dados;
            }
            if (vazio != null) vazio.IsVisible = dados.Count == 0;
        }

        // Botão: ir para cadastro
        private async void OnAdicionarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }

        // Botão: recarregar lista
        private async void OnAtualizarClicked(object sender, EventArgs e)
        {
            await Carregar(forceRebind: true);
        }

        // Gesto: puxar para atualizar
        private async void OnRefreshing(object sender, EventArgs e)
        {
            await Carregar(forceRebind: true);
            var refresh = this.FindByName<RefreshView>("Refresh");
            if (refresh != null) refresh.IsRefreshing = false;
        }

        // Selecionar item: abre edição
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection?.FirstOrDefault() is Produto p)
            {
                ((CollectionView)sender).SelectedItem = null; // Limpa seleção
                await Navigation.PushAsync(new EditarProduto(p)); // Abre tela de editar
            }
        }

        // Swipe: excluir
        private async void OnExcluirSwipe(object sender, EventArgs e)
        {
            if (sender is SwipeItem swipe && swipe.CommandParameter is Produto p)
            {
                var ok = await DisplayAlert("Excluir", $"Excluir '{p.Descricao}'?", "Sim", "Não");
                if (ok)
                {
                    await App.Db.Delete(p.Id);
                    await Carregar(forceRebind: true);
                }
            }
        }
    }
}