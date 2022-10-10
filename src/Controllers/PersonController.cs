using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

using src.Persistence;
using src.Models;


namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase
{
    private DatabaseContext _context { get; set; }

    //injecao de dependencia
    public PessoaController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<Pessoa>> Get()
    {
        var result = _context.Pessoas.Include(p => p.contratos).ToList();
        if (!result.Any()) return NoContent();// sem conteúdo, NoContentStatus.
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Pessoa> Post([FromBody] Pessoa pessoa)
    {
        try
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }


        return Created("criado", pessoa);
    }

    [HttpPut("{id}")]
    public ActionResult<Object> Update(
        [FromRoute] int id,
        [FromBody] Pessoa pessoa)
    {
        var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);
        if (result is null)
        {
            return NotFound(new
            {
                msg = "Registro não encontrado",
                status = HttpStatusCode.NotFound
            });
        }
        try
        {
            _context.Pessoas.Update(pessoa);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            return BadRequest(new
            {
                msg = $"Erro ao tentar atualizar o ID: {id} ",
                status = HttpStatusCode.OK
            });

        }
        return Ok(new
        {
            msg = $"Dados do ID: {id} atualizados com sucesso",
            status = HttpStatusCode.OK
        });
    }

    #region "Antigo Método Delete"    
    // [HttpDelete("{id}")]
    // public string Delete([FromRoute] int id)
    // {
    //     var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);
    //     _context.Pessoas.Remove(result);
    //     _context.SaveChanges();

    //     return "deletado pessoa de Id " + id;
    // }     
    #endregion

    [HttpDelete("{id}")]
    public ActionResult<Object> Delete([FromRoute] int id)
    {
        var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);
        if (result is null) return BadRequest(new
        {// igual o get só que para não listas. valor não valido
            msg = "Conteúdo inexistente, solicitação inválida.",
            status = HttpStatusCode.BadRequest
        });
        //action result
        _context.Pessoas.Remove(result);
        _context.SaveChanges();
        return Ok(new
        {
            msg = $"Deletada pessoa de ID: {id}",
            status = HttpStatusCode.OK
        });
    }
}
