namespace TestServer.Domain.Models
{
	using System.Runtime.Serialization;

	[DataContract]
	public class Address
	{
		[DataMember(Order = 1)]
		public string Number { get; set; }

		[DataMember(Order = 2)]
		public string Street { get; set; }

		[DataMember(Order = 3)]
		public string Suburb { get; set; }

		[DataMember(Order = 4)]
		public string City { get; set; }

		[DataMember(Order = 5)]
		public string Country { get; set; }
	}
}
