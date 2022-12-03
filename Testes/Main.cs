using CasimiroErrorException;
using Xunit;
using Xunit.Abstractions;

namespace Testes
{
    public class Main
    {
        private readonly ITestOutputHelper output;
        public Main(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CrioListaDeMensagensDadoUmStatusCode200_ChamoObterMensagens_RetornaUmaMensagemAleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .CriarMensagens(new List<string>()
            {
                "Daniel comeu pão na casa do João",
                "Quem eu?",
                "Tu sim.",
                "Eu não",
                "Então qeum foi?"
            });

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.OK);
            output.WriteLine(resultado);
        }

        [Fact]
        public void CrioListaDeMensagensDadoUmStatusCode400_ChamoObterMensagens_RetornaUmaMensagemAleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .CriarMensagens(new List<string>()
            {
                "Daniel comeu pão na casa do João",
                "Quem eu?",
                "Tu sim.",
                "Eu não",
                "Então quem foi?"
            });

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            output.WriteLine(resultado);
        }

        [Fact]
        public void CrioListaDeMensagensDadoUmStatusCode400DesignFluter_ChamoObterMensagens_RetornaUmaMensagemAleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .CriarMensagens(new List<string>()
            {
                "TESTE 1",
            })
            .CriarMensagens(new List<string>()
            {
                "TESTE 2",
            });

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            output.WriteLine(resultado);
        }
    }
}