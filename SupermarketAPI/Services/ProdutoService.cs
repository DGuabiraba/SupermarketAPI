using Microsoft.Extensions.Logging;
using SupermarketAPI.Models;
using SupermarketAPI.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<ProdutoService> _logger;

        public ProdutoService(IProdutoRepository produtoRepository, ILogger<ProdutoService> logger)
        {
            _produtoRepository = produtoRepository;
            _logger = logger;
        }

        public IEnumerable<ProdutoResponseDTO> GetAll()
        {
            _logger.LogInformation("Obtendo todos os produtos");
            return _produtoRepository.GetAll()
                .Select(p => new ProdutoResponseDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Quantidade = p.Quantidade
                }).ToList();
        }

        public ProdutoResponseDTO? GetById(int id)
        {
            _logger.LogInformation($"Obtendo o produto de ID: {id}");
            var produto = _produtoRepository.GetById(id);
            if (produto == null)
            {
                _logger.LogWarning($"Produto de ID {id} não encontrado");
                return null;
            }

            return new ProdutoResponseDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade
            };
        }

        public void Create(ProdutoDTO produtoDTO)
        {
            _logger.LogInformation("Criando um novo produto");
            var produto = new Produto
            {
                Nome = produtoDTO.Nome,
                Preco = produtoDTO.Preco,
                Quantidade = produtoDTO.Quantidade
            };
            _produtoRepository.Create(produto);
        }

        public void Update(int id, ProdutoDTO produtoDTO)
        {
            _logger.LogInformation($"Atualizando o produto de ID: {id}");
            var produto = _produtoRepository.GetById(id);
            if (produto == null)
            {
                _logger.LogWarning($"Produto de ID {id} não encontrado");
                return;
            }

            produto.Nome = produtoDTO.Nome;
            produto.Preco = produtoDTO.Preco;
            produto.Quantidade = produtoDTO.Quantidade;

            _produtoRepository.Update(produto);
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Deletando o produto de ID: {id}");
            _produtoRepository.Delete(id);
        }
    }
}
