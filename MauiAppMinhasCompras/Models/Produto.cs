using SQLite; // Atributos do SQLite (PrimaryKey etc.)

namespace MauiAppMinhasCompras.Models
{
    // Representa a tabela Produto no banco
    public class Produto
    {
        [PrimaryKey, AutoIncrement]     // Id único gerado pelo SQLite
        public int Id { get; set; }

        public string Descricao { get; set; } = ""; // Nome do produto
        public int Quantidade { get; set; }         // Qtd comprada
        public decimal Preco { get; set; }          // Preço unitário

        [Ignore]                         // Campo calculado (não vai para o banco)
        public decimal Total => Quantidade * Preco; // Total = qtd * preço
    }
}