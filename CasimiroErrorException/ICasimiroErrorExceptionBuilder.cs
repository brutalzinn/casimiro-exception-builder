using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CasimiroErrorException
{
    public interface ICasimiroErrorExceptionBuilder
    {
        ICasimiroErrorExceptionBuilder Juntar(HttpStatusCode statusCode);
        ICasimiroErrorExceptionBuilder AdicionarMensagens(IEnumerable<string> mensagem);
        string ObterMensagensRandom(HttpStatusCode statusCode);
    }
}
