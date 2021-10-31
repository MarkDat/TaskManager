using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Utilities
{
	public class HttpException : Exception
	{
		public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.BadRequest;

		public HttpException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base()
		{
			StatusCode = statusCode;
		}

		public HttpException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
		{
			StatusCode = statusCode;
		}

		public HttpException(string message, Exception ex, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, ex)
		{
			StatusCode = statusCode;
		}
	}
}
