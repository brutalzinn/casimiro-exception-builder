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
        /// <summary>
        /// Um placeholder {LINK_STACKOVERFLOW} será aplicado na string utilizada. 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        ICasimiroErrorExceptionBuilder UsarStackOverFlow(bool stackOverFlow, string mensagemErro);
        ICasimiroErrorExceptionBuilder Juntar(HttpStatusCode statusCode);
        ICasimiroErrorExceptionBuilder AdicionarMensagens(IEnumerable<string> mensagem);
        string ObterMensagensRandom(HttpStatusCode statusCode);
    }
}
