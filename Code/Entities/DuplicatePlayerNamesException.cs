using System;
using System.Runtime.Serialization;

namespace CircleOfWealth.Entities
{
	[Serializable]
	public class DuplicatePlayerNamesException : Exception
	{
		public DuplicatePlayerNamesException() { }
		public DuplicatePlayerNamesException(string message) : base(message) { }
		public DuplicatePlayerNamesException(string message, Exception innerException) : base(message, innerException) { }
		protected DuplicatePlayerNamesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
