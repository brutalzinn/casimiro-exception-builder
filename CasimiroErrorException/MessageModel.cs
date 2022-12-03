using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CasimiroErrorException
{
    public class MessageModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Mensagens { get; set; }
        public MessageModel(HttpStatusCode status, IEnumerable<string> mensagens)
        {
            StatusCode = status;
            Mensagens = mensagens;
        }
    }
}
