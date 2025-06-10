using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Sistema_Gerenciamento.DTO;
using Sistema_Gerenciamento.models;

namespace Sistema_Gerenciamento.Data
{
    public class Database
    {
        private readonly string? _conexaoString;

        public Database()
        {
            _conexaoString = Environment.GetEnvironmentVariable("DB-CONNECTION");
        }

        public async Task<MySqlConnection> GetConnectionAsync()
        {
            var conexao = new MySqlConnection(_conexaoString);
            await conexao.OpenAsync();
            return conexao;
        }

        public async Task<int> CadastrarCarroAsync(Carro carro)
        {
            using var conexao = await GetConnectionAsync();
            var sql = "INSERT INTO carro (marca, modelo, ano) VALUES (@Marca, @Modelo, @Ano)";

            var linhasAfetadas = await conexao.ExecuteAsync(sql, carro);
            return linhasAfetadas;
        }

        public async Task<IEnumerable<Carro>> ListarCarrosAsync()
        {
            const string sql = "SELECT * FROM carro";

            using var conexao = await GetConnectionAsync();
            var carros = await conexao.QueryAsync<Carro>(sql);
            return carros;
        }

        public async Task<Carro?> ObterCarroPorIdAsync(int id)
        {
            const string sql = "SELECT * FROM carro WHERE id = @Id";

            using var conexao = await GetConnectionAsync();
            var carro = await conexao.QueryFirstOrDefaultAsync<Carro>(sql, new { Id = id });
            return carro;
        }

        public async Task<bool> DeletarCarroAsync(int id)
        {
            const string sql = "DELETE FROM carro WHERE Id = @id";

            using var conexao = await GetConnectionAsync();
            var linhasAfetadas = await conexao.ExecuteAsync(sql, new { Id = id });
            return linhasAfetadas > 0;
        }

        public async Task<bool> AtualizarCarroAsync(int id, AtualizarCarroDTO atualizarCarroDTO)
        {
            const string sql = "UPDATE carro SET marca = @NovaMarca, modelo = @NovoModelo, ano = @NovoAno WHERE Id = @id";

            using var conexao = await GetConnectionAsync();
            var linhasAfetadas = await conexao.ExecuteAsync(sql, new { Id = id, NovaMarca = atualizarCarroDTO.NovaMarca, NovoModelo = atualizarCarroDTO.NovoModelo, NovoAno = atualizarCarroDTO.NovoAno });
            return linhasAfetadas > 0;
        }
    }
}