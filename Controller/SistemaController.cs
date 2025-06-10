using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sistema_Gerenciamento.Data;
using Sistema_Gerenciamento.DTO;
using Sistema_Gerenciamento.models;

namespace Sistema_Gerenciamento.Controller
{
    [ApiController]
    [Route("api/")]
    public class SistemaController : ControllerBase
    {
        private readonly Database _database;

        public SistemaController(Database database)
        {
            _database = database;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastrarCarro([FromBody] Carro carro)
        {
            try
            {
                var resultado = await _database.CadastrarCarroAsync(carro);

                if (resultado > 0)
                {
                    return Ok("Carro cadastrado no banco de dados com sucesso!");
                }
                else
                {
                    return BadRequest("Falha ao cadastrar carro no banco de dados!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarCarrosCadastrados()
        {
            try
            {
                var carros = await _database.ListarCarrosAsync();
                if (!carros.Any())
                {
                    return NotFound("Nenhum carro cadastrado");
                }

                return Ok(carros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var carro = await _database.ObterCarroPorIdAsync(id);
                if (carro == null)
                {
                    return NotFound($"Nenhum carro encontrado com o Id fornecido: {id}");
                }
                return Ok(carro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPorId(int id)
        {
            try
            {
                var sucess = await _database.DeletarCarroAsync(id);
                if (sucess)
                {
                    return Ok("Carro deletado com sucesso!");
                }
                return NotFound("Carro não encontrado ou não cadastrado na tabela");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar carro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCarroAsync(int id, [FromBody] AtualizarCarroDTO atualizarCarroDTO)
        {
            try
            {
                var sucess = await _database.AtualizarCarroAsync(id, atualizarCarroDTO);
                if (sucess)
                {
                    return Ok($"Carro atualizado com sucesso!");
                }
                return NotFound("Carro não encontrado ou não cadastrado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar no banco de dados: {ex.Message}");
            }
        }
    }
}