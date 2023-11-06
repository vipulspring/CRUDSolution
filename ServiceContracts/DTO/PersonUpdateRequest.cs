﻿using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{

	/// <summary>
	/// It is Used to update the the details of Person
	/// </summary>
	public class PersonUpdateRequest
	{
		[Required (ErrorMessage = "Person ID can't be blank")]
		public Guid PersonID { get; set; }

		[Required(ErrorMessage = "PersonName cannot be blank")]
		public string? PersonName { get; set; }
		[Required(ErrorMessage = "Email cannot be blank")]
		[EmailAddress(ErrorMessage = "Email should be valid email")]
		public string? Email { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public GenderOptions? Gender { get; set; }
		public Guid? CountryID { get; set; }
		public string? Address { get; set; }
		public bool ReceiveNewsLetters { get; set; }

		public Person ToPerson()
		{
			return new Person()
			{
				PersonID = PersonID,
				PersonName = PersonName,
				Email = Email,
				DateOfBirth = DateOfBirth,
				Gender = Gender.ToString(),
				CountryID = CountryID,
				Address = Address,
				ReceiveNewsLetters = ReceiveNewsLetters
			};
		}
	}
}
