using StringPlaceholder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace CasimiroErrorException
{
    /// <summary>
    /// Cria um CasimiroErrorExceptionBuilder
    /// </summary>
    public class CasimiroErrorExceptionBuilder : ICasimiroErrorExceptionBuilder
    {
        private List<MessageModel> Mensagens { get; set; }
        private HttpStatusCode HttpStatusCode { get; set; }
        private bool StackOverFlow { get; set; }
        private string MensagemErro { get; set; }
        private CasimiroErrorExceptionBuilder(HttpStatusCode statusCode)
        {
            HttpStatusCode = statusCode;
            Mensagens = new List<MessageModel>();
        }
        private MessageModel? ObterListaRegistrada(HttpStatusCode statusCode)
        {
            return Mensagens.FirstOrDefault(x => x.StatusCode == statusCode);
        }

        public static CasimiroErrorExceptionBuilder Criar(HttpStatusCode statusCode) => new CasimiroErrorExceptionBuilder(statusCode);
        public ICasimiroErrorExceptionBuilder Juntar(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            return this;
        }
        public ICasimiroErrorExceptionBuilder AdicionarMensagens(IEnumerable<string> mensagem)
        {
            var listaRegistrada = ObterListaRegistrada(HttpStatusCode);
            if (listaRegistrada != null)
            {
                listaRegistrada.Mensagens = listaRegistrada.Mensagens.Concat(InjetarStackOverFlowCasoAtivo(mensagem));
                return this;
            }
            var messageModel = new MessageModel(HttpStatusCode, InjetarStackOverFlowCasoAtivo(mensagem));
            Mensagens.Add(messageModel);
            return this;
        }

        public string ObterMensagensRandom(HttpStatusCode statusCode)
        {
            var mensagemModel = Mensagens.FirstOrDefault(m => m.StatusCode == statusCode);
            var tamanhoListaModel = mensagemModel.Mensagens.Count();
            var randomIndex = new Random().Next(0, tamanhoListaModel);
            return mensagemModel.Mensagens.ElementAt(randomIndex);
        }

        public ICasimiroErrorExceptionBuilder UsarStackOverFlow(bool stackOverFlow, string mensagemErro = "")
        {
            StackOverFlow = stackOverFlow;
            MensagemErro = mensagemErro;
            return this;
        }

        private IEnumerable<string> InjetarStackOverFlowCasoAtivo(IEnumerable<string> mensagens)
        {
            if (StackOverFlow)
            {
                return InjetarPlaceHolder(mensagens);
            }
            return mensagens;
        }
        private IEnumerable<string> InjetarPlaceHolder(IEnumerable<string> mensagens)
        {
            var pattern = @"\{(.*?)\}";
            var stringPlaceholder = new PlaceholderCreator();
            var listaExecutors = new List<StringExecutor>()
            {
                new StringExecutor("LINK_STACKOVERFLOW", ()=> GerarUrlStackOverFlow(MensagemErro)),
            };
            mensagens = mensagens.Select(x =>
            {
                return stringPlaceholder.Creator(x, listaExecutors, pattern);
            });
            return mensagens;
        }
        private string GerarUrlStackOverFlow(string objParams)
        {
            const string STACKOVERFLOW_URL = "https://stackoverflow.com/search?q=";
            return STACKOVERFLOW_URL + HttpUtility.UrlEncode(objParams);
        }
    }


}

