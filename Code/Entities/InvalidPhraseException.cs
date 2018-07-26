using System;
using System.Runtime.Serialization;

namespace CircleOfWealth.Entities
{
	[Serializable]
	public class InvalidPhraseException : Exception
	{
		public InvalidPhraseException() { }
		public InvalidPhraseException(string message) : base(message) { }
		public InvalidPhraseException(string message, Exception innerException) : base(message, innerException) { }
		protected InvalidPhraseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
