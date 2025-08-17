using System.Collections.Generic;      // List<>
using System.Threading.Tasks;          // Task
using SQLite;                          // SQLiteAsyncConnection
using MauiAppMinhasCompras.Models;     // Produto

namespace MauiAppMinhasCompras.Helpers
{
    // Centraliza conexão e CRUD
    public class SQLiteDatabaseHelper
    {
        private readonly SQLiteAsyncConnection _conn; // Conexão fixa após o construtor

        public SQLiteDatabaseHelper(string path)      // path = arquivo .db3
        {
            _conn = new SQLiteAsyncConnection(path);  // Abre conexão
            _conn.CreateTableAsync<Produto>().Wait(); // Garante tabela criada
        }

        // CREATE: insere produto
        public Task<int> Insert(Produto p) =>
            _conn.InsertAsync(p);

        // READ: todos os produtos
        public Task<List<Produto>> GetAll() =>
            _conn.Table<Produto>().ToListAsync();

        // UPDATE: atualiza por Id (retorna linhas afetadas)
        public Task<int> Update(Produto p)
        {
            const string sql = @"UPDATE Produto
                                 SET Descricao = ?, Quantidade = ?, Preco = ?
                                 WHERE Id = ?";
            return _conn.ExecuteAsync(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
            // Alternativa simples:
            // return _conn.UpdateAsync(p);
        }

        // DELETE: exclui por Id
        public Task<int> Delete(int id) =>
            _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
    }
}