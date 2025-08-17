using MauiAppMinhasCompras.Models;  // Produto

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent(); // Carrega XAML
        }

        // Valida entradas e insere no banco
        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            var desc = TxtDescricao.Text?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(desc))
            {
                await DisplayAlert("Atenção", "Informe a descrição.", "OK");
                return;
            }
            if (!int.TryParse(TxtQuantidade.Text, out var qtd))
            {
                await DisplayAlert("Atenção", "Quantidade inválida.", "OK");
                return;
            }
            if (!decimal.TryParse(TxtPreco.Text, out var preco))
            {
                await DisplayAlert("Atenção", "Preço inválido.", "OK");
                return;
            }

            var p = new Produto { Descricao = desc, Quantidade = qtd, Preco = preco };

            var linhas = await App.Db.Insert(p);     // Salva no SQLite
            if (linhas > 0)
            {
                await DisplayAlert("OK", "Produto salvo.", "Fechar");
                await Navigation.PopAsync();         // Volta para a lista
            }
            else
            {
                await DisplayAlert("Erro", "Não foi possível salvar.", "OK");
            }
        }
    }
}