using System;
using System.Runtime.Serialization;

namespace CurrencyChecker.External.Api.Hnb
{
	[Serializable]
	public class ApiUnavailable : Exception
	{
		public ApiUnavailable()
		{
		}

		public ApiUnavailable(string message) : base(message)
		{
		}

		public ApiUnavailable(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ApiUnavailable(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}