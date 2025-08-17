using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class EditarProduto : ContentPage
    {
        private Produto _produto; // Item sendo editado

        public EditarProduto(Produto produto)
        {
            InitializeComponent();
            _produto = produto; // Guarda a referência

            // Preenche os campos
            TxtDescricao.Text = _produto.Descricao;
            TxtQuantidade.Text = _produto.Quantidade.ToString();
            TxtPreco.Text = _produto.Preco.ToString();
        }

        // Atualiza no banco
        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtDescricao.Text) ||
                !int.TryParse(TxtQuantidade.Text, out var qtd) ||
                !decimal.TryParse(TxtPreco.Text, out var preco))
            {
                await DisplayAlert("Atenção", "Preencha os campos corretamente.", "OK");
                return;
            }

            _produto.Descricao = TxtDescricao.Text!.Trim();
            _produto.Quantidade = qtd;
            _produto.Preco = preco;

            var linhas = await App.Db.Update(_produto); // Chama UPDATE
            if (linhas > 0)
            {
                await DisplayAlert("OK", "Produto atualizado.", "Fechar");
                await Navigation.PopAsync(); // Volta para a lista
            }
            else
            {
                await DisplayAlert("Erro", "Não foi possível atualizar.", "OK");
            }
        }
    }
}