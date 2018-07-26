using System;
using System.Runtime.Serialization;

namespace CircleOfWealth.Entities
{
	[Serializable]
	public class MissingPlayerNamesException : Exception
	{
		public MissingPlayerNamesException() { }
		public MissingPlayerNamesException(string message) : base(message) { }
		public MissingPlayerNamesException(string message, Exception innerException) : base(message, innerException) { }
		protected MissingPlayerNamesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
