﻿using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace CRUDExample.Controllers
{
	public class PersonsController : Controller
	{
		//private fields
		private readonly IPersonsService _personsService;

		//Constructor
		public PersonsController(IPersonsService personsService)
		{

			_personsService = personsService;	
		}

		[Route("persons/index")]
		[Route("/")]
		public IActionResult Index(string searchBy, string? searchString)
		{
			ViewBag.SearchFields = new Dictionary<string, string>()
			{
				{ nameof(PersonResponse.PersonName), "Person Name" },
				{ nameof(PersonResponse.Email), "Email" },
				{ nameof(PersonResponse.DateOfBirth), "Date Of Birth" },
				{ nameof(PersonResponse.Gender), "Gender" },
				{ nameof(PersonResponse.Country), "Country" },
				{ nameof(PersonResponse.Address), "Address" },

			};

			List<PersonResponse> persons = _personsService.GetAllPersons();
			return View(persons); //Views/Persons/Index.cshtml
		}
	}
}
