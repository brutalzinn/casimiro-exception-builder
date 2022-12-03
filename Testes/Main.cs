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
    public class Main
    {
        private readonly ITestOutputHelper output;
        public Main(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CrioListaDeMensagensDadoUmStatusCode200_ChamoObterMensagens_RetornaUmaMensagemStatusCode200Aleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .AdicionarMensagens(new List<string>()
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
        public void CrioListaDeMensagensStatusCodeDiversos_ChamoObterMensagensParaStatusCode200EStatusCode400_RetornaUmaMensagemAleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .AdicionarMensagens(new List<string>()
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
            .AdicionarMensagens(new List<string>()
            {
                "NotFound Crio lista de strings para NotFound",
                "NotFound brasil perdeu para camarões",
                "NotFound WSL tá querendo morrer mas vida que segue."
            })
            .AdicionarMensagens(new List<string>()
            {
                "NotFound TESTE 2",
            })
            .Juntar(System.Net.HttpStatusCode.OK)
            .AdicionarMensagens(new List<string>()
            {
                "OK Agora é Status CODE OK",
                "OK Não é que deu OK Mesmo?",
            })
            .AdicionarMensagens(new List<string>()
            {
                "OK A gente pode criar infinitas listas para cada tipo de status code.",
                "OK E assim adicionar frases aleatórias de erro?",
                "OK Por que alguém faria isso? Porque estou esperando o hamburguer chegar mas já se passou uma hora"
            });

            var resultadoNotFound = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            output.WriteLine(resultadoNotFound);

            var resultadoOK = teste.ObterMensagensRandom(System.Net.HttpStatusCode.OK);
            output.WriteLine(resultadoOK);
        }
    }
}