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
        ICasimiroErrorExceptionBuilder CriarMensagens(IEnumerable<string> mensagem);
        string ObterMensagensRandom(HttpStatusCode statusCode);
    }
}
