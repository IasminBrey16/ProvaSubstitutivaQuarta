using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly DataContext _context;
        public FuncionarioController(DataContext context){
            _context = context;
        }

        // POST: api/funcionario/create
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Funcionario funcionario){
            Funcionario cpfExists = _context.TabelaFuncionarios.FirstOrDefault(
                u => u.Cpf == funcionario.Cpf
            );
            if(cpfExists == null){
                _context.TabelaFuncionarios.Add(funcionario);
                _context.SaveChanges();
                return Created("", funcionario);
            }
            return NotFound($"ERRO: CPF {funcionario.Cpf} já existe no sistema.");
        }

        // GET: api/funcionario/list
        [HttpGet]
        [Route("list")]
        public IActionResult List(){
            return Ok(_context.TabelaFuncionarios.ToList());
        }

        //GET: api/funcionario/getbyid/1
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult GetById([FromRoute] int id){
            Funcionario funcionario = _context.TabelaFuncionarios.Find(id);
            if(funcionario == null){
                return NotFound($"ERRO: Funcionário com ID #{id} não encontrado.");
            }
            return Ok(funcionario);
        }

        // DELETE: api/funcionario/delete/cpf
        [HttpDelete]
        [Route("delete/{cpf}")]
        public IActionResult Delete([FromRoute] string cpf){
            Funcionario funcionario = _context.TabelaFuncionarios.FirstOrDefault(
                funcionario => funcionario.Cpf == cpf
            );
            if(funcionario == null){
                return NotFound($"ERRO: Usuário com CPF {cpf} não encontrado.");
            }
            _context.TabelaFuncionarios.Remove(funcionario);
            _context.SaveChanges();
            return Ok(_context.TabelaFuncionarios.ToList());
        }

        //PUT: api/funcionario/update
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] Funcionario funcionario){
            _context.TabelaFuncionarios.Update(funcionario);
            _context.SaveChanges();
            return Ok(funcionario);
        }

    }
}