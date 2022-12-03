﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CasimiroErrorException
{
    /// <summary>
    /// Cria um CasimiroErrorExceptionBuilder
    /// </summary>
    public class CasimiroErrorExceptionBuilder : ICasimiroErrorExceptionBuilder
    {
        private List<MessageModel> Mensagens { get; set; }
        private HttpStatusCode HttpStatusCode { get; set; }
        private CasimiroErrorExceptionBuilder(HttpStatusCode statusCode)
        {
            HttpStatusCode = statusCode;
            Mensagens = new List<MessageModel>();
        }

        public static CasimiroErrorExceptionBuilder Criar(HttpStatusCode statusCode) => new CasimiroErrorExceptionBuilder(statusCode);

        public ICasimiroErrorExceptionBuilder CriarMensagens(IEnumerable<string> mensagem)
        {
            var messageModel = new MessageModel(HttpStatusCode, mensagem);

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
    }
}