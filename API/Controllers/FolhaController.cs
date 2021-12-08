using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/folha")]
    public class FolhaController : ControllerBase
    {
        private readonly DataContext _context;
        public FolhaController(DataContext context){
            _context = context;
        }

        // POST: api/folha/create
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Folha folha){
            //VERIFICAR SE FUNCIONARIO EXISTE
            Funcionario funcionario = _context.TabelaFuncionarios.FirstOrDefault(
                l => l.Cpf == folha.CpfFuncionario);
            if(funcionario == null){
                //VERIFICAR SE MES EXISTE
                _context.TabelaFolhas.Find(folha.Mes);
                if(folha == null){
                    //VERIFICAR SE ANO EXISTE
                    _context.TabelaFolhas.Find(folha.Ano);
                    if(folha == null){
                    }
                }
            }

            folha.SalarioBruto = folha.HorasTrabalhadas * folha.ValorHora;

            if (folha.SalarioBruto < 1903.98) {
                folha.ImpostoRenda = 0.00;
            } else if (folha.SalarioBruto > 1903.99 && folha.SalarioBruto < 2826.65) {
                folha.ImpostoRenda = 142.80;
            } else if (folha.SalarioBruto > 2826.66 && folha.SalarioBruto < 3751.05) {
                folha.ImpostoRenda = 354.80;
            } else if (folha.SalarioBruto > 3751.06 && folha.SalarioBruto < 4664.68) {
                folha.ImpostoRenda = 636.13;
            } else {
                folha.ImpostoRenda = 869.36;
            }

            if (folha.SalarioBruto < 1659.38) {
                folha.Inss = folha.SalarioBruto * 0.08;
            } else if (folha.SalarioBruto > 1659.38 && folha.SalarioBruto < 2765.66) {
                folha.Inss = folha.SalarioBruto * 0.09;
            } else if (folha.SalarioBruto > 2765.67 && folha.SalarioBruto < 5531.31) {
                folha.Inss = folha.SalarioBruto * 0.011;
            } else {
                folha.Inss = 608.44;
            }

            folha.Fgts = folha.SalarioBruto - (folha.SalarioBruto * 0.08);

            folha.salarioLiquido = folha.SalarioBruto - folha.ImpostoRenda - folha.Inss;

            //CADASTRAR FOLHA
            _context.TabelaFolhas.Add(folha);
            _context.SaveChanges();
            return Created("", folha);
        }

        // GET: api/folha/list
        [HttpGet]
        [Route("list")]
        public IActionResult List(){
            return Ok(_context.TabelaFolhas.ToList());
        }

        //GET: api/folha/getbyid/1
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById([FromRoute] int id){
            Folha folha = _context.TabelaFolhas.Find(id);
            if(folha == null){
                return NotFound($"ERRO: Folha com ID #{id} não encontrado.");
            }
            return Ok(folha);
        }

        //DELETE: api/folha/delete/1
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete([FromRoute] int id){
            Folha folha = _context.TabelaFolhas.Find(id);
            if(folha == null){
                return NotFound($"ERRO: Folha com ID #{id} não encontrado.");
            }
            _context.TabelaFolhas.Remove(folha);
            _context.SaveChanges();
            return Ok(_context.TabelaFolhas.ToList());
        }

        //PUT: api/folha/update
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] Folha folha){
            _context.TabelaFolhas.Update(folha);
            _context.SaveChanges();
            return Ok(folha);
        }
    }
}