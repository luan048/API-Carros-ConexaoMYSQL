using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
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
    }
}