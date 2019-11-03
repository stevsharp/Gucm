using System.Collections.Generic;
using System.Linq;

namespace Gucm.Api.Models
{
    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(IEnumerable<string> messages) => Messages = messages.ToArray();

        public string[] Messages { get; set; }
    }
}
