using System;
using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
	public class PersonAddRequest
	{
		[Required(ErrorMessage ="PersonName cannot be blank")]
		public string? PersonName { get; set; }
		[Required(ErrorMessage = "Email cannot be blank")]
		[EmailAddress(ErrorMessage = "Email should be valid email")]
		public string? Email { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public GenderOptions? Gender { get; set; }
		public Guid? CountryID { get; set; }
		public string? Address { get; set; }
		public bool ReceiveNewsLetters { get; set; }

		/// <summary>
		/// Convert PersonAddRequest to Person object.
		/// </summary>
		/// <returns></returns>
		public Person ToPerson()
		{
			return new Person(){
				PersonName = PersonName,
				Email = Email,
				DateOfBirth = DateOfBirth,
				Gender = Gender.ToString(),
				CountryID = CountryID,
				Address = Address,
				ReceiveNewsLetters = ReceiveNewsLetters };
	}
	}

	
}
