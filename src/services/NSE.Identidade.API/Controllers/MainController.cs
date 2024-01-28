using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.Identidade.API.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Erros = new List<string>();
    protected ActionResult CustomResponse(object result = null)
    {
        if (OperacaoValida())
            return Ok(result);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Mensagens", Erros.ToArray()}
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
            AdicionarErrosProcessamento(erro.ErrorMessage);

        return CustomResponse(erros);
    }

    protected bool OperacaoValida()
        => !Erros.Any();

    protected void AdicionarErrosProcessamento(string Erro)
        => Erros.Add(Erro);

    protected void LimparErrosProcessamento()
        => Erros.Clear();
}