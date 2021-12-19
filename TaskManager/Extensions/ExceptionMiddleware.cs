using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TM.API.DTOs.Extensions;
using TM.Domain.Utilities;

namespace TM.API.Extensions
{
	public class ExceptionMiddleware
	{
		private readonly IWebHostEnvironment _env;

		public ExceptionMiddleware(IWebHostEnvironment env)
		{
			_env = env;
		}

		public async Task Invoke(HttpContext context)
		{
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

			// Find and hold onto any CORS related headers ...
			var corsHeaders = new HeaderDictionary();

			foreach (var pair in context.Response.Headers)
			{
				if (!pair.Key.ToLower().StartsWith("access-control-")) { continue; } // Not CORS related
				corsHeaders[pair.Key] = pair.Value;
			}
			var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
			if (ex == null)
				return;

			if (ex is HttpException || typeof(HttpException).IsAssignableFrom(ex.GetType()))
			{
				context.Response.StatusCode = (int)((HttpException)ex).StatusCode;
			}

			var headers = context.Response.Headers;
			// Ensure all CORS headers remain or else add them back in ...
			foreach (var pair in corsHeaders)
			{
				if (headers.ContainsKey(pair.Key)) { continue; } // Still there!
				headers.Add(pair.Key, pair.Value);
			}

			//
			// Log
			//Logger.LogException(ex);
			//
			// Convert to model
			var errorMessage = ex.Message;

			var error = new AppErrorModel(errorMessage, null, context.Response.StatusCode);
			//
			// Return as json
			context.Response.ContentType = "application/json; charset=utf-8";
			using (var writer = new StreamWriter(context.Response.Body))
			{
				var jsonSerializer = new JsonSerializer
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver(),
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					DateTimeZoneHandling = DateTimeZoneHandling.Local,
					Formatting = Formatting.Indented,
					DateFormatString = "MM/dd/yy H:mm:ss zzz"
				};

				jsonSerializer.Serialize(writer, error);
				await writer.FlushAsync().ConfigureAwait(false);
			}
		}
	}
}
