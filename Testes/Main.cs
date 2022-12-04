using CasimiroErrorException;
using Xunit;
using Xunit.Abstractions;

namespace Testes
{
    /// <summary>
    /// Usando Xunit mas não tô testando nada. Go Horse Go Horse!
    /// Projeto criado 03/12/2022 durante a espera do meu hamburguer
    /// Que apesar de tudo, chegou.
    /// </summary>
    /// 

    public class Main
    {
        private List<string> FrasesCasimiroValidacao = new List<string>()
        {
            "Meteu essa?", "Isso que dá gastar dinheiro com merda!",
            "DENTROOOOO! Só que não, né doidão?!",
            "Nerdola meteu o dado errado. kkkkkkk",
            "Cartão amarelo, doidão. Faz o teu que dá certo."
        };

        private List<string> FrasesCasimiroErrosLinkStackOverflow = new List<string>()
        {
            "hmmmmm, que papinho, hein?! Tô te entendendo não. Quebrou o servidor bonito. Toma um link do StackOverFlow: {LINK_STACKOVERFLOW}",
            "Porra mané, nem se eu fosse um corsa capotava assim. Toma um link do StackOverFlow: {LINK_STACKOVERFLOW}"
        };

        private List<string> FrasesCasimiroNaoEncontrado = new List<string>()
        {
            "Caraca doidão. Não encontrei nada.",
            "Nada que não possa piorar. Não encontrei isso.",
            "Capotou celta. Encontrei nada.",
            "EITAAAAAAAAAAAAA, rodou, mané!. Toma um 404 aí.",
        };

        private List<string> FrasesCasimiroSucesso = new List<string>()
        {
            "Deu bom.",
            "VAI VASCO DA GAMAAA!",
            "Boa, mané!",
            "Que papinhooooo. Bem que disseram que funcionava mesmo",
            "Que isso, melhor que celta é quando o request dá certo."
        };


        private readonly ITestOutputHelper output;
        public Main(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CrioListaDeMensagensDadoUmStatusCode400_ChamoObterMensagens_RetornaUmaMensagemStatusCode400Aleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.BadRequest)
            .AdicionarMensagens(FrasesCasimiroValidacao);

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.BadRequest);
            Assert.Contains(FrasesCasimiroValidacao, item => item.Equals(resultado));
        }

        [Fact]
        public void CrioListaDeMensagensDadoUmStatusCode200_ChamoObterMensagens_RetornaUmaMensagemStatusCode200Aleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .AdicionarMensagens(FrasesCasimiroNaoEncontrado);

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            Assert.Contains(FrasesCasimiroNaoEncontrado, item => item.Equals(resultado));
        }

        [Fact]
        public void CrioListaDeMensagensStatusCodeDiversos_ChamoObterMensagensParaStatusCode200EStatusCode400_RetornaUmaMensagemAleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .AdicionarMensagens(FrasesCasimiroNaoEncontrado);

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            Assert.Contains(FrasesCasimiroNaoEncontrado, item => item.Equals(resultado));
        }

        [Fact]
        public void CrioListaDeMensagensDeDiversosStatusCode_ChamoObterMensagens_RetornaUmaMensagemAleatoriaDadoUmStatusCode()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .AdicionarMensagens(FrasesCasimiroNaoEncontrado)
            .Juntar(System.Net.HttpStatusCode.OK)
            .AdicionarMensagens(FrasesCasimiroSucesso);

            var resultadoNotFound = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            Assert.Contains(FrasesCasimiroNaoEncontrado, item => item.Equals(resultadoNotFound));

            var resultadoOK = teste.ObterMensagensRandom(System.Net.HttpStatusCode.OK);
            Assert.Contains(FrasesCasimiroSucesso, item => item.Equals(resultadoOK));
        }

        [Fact]
        public void CrioListaDeMensagensStatusCode500_ChamoObterMensagens_RetornaUmaMensagemAleatoriaComLinkStackOverflowDadoUmStatusCode()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.InternalServerError)
            .UsarStackOverFlow(true, "Nginx large file content error")
            .AdicionarMensagens(FrasesCasimiroErrosLinkStackOverflow);
            var resultadoInternalServerError = teste.ObterMensagensRandom(System.Net.HttpStatusCode.InternalServerError);
            Assert.Contains("https://stackoverflow.com/search?q=Nginx+large+file+content+error", resultadoInternalServerError);
            output.WriteLine(resultadoInternalServerError);
        }
    }
}