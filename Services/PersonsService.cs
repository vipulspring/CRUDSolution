using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class PersonsService : IPersonsService
	{
		private readonly List<Person> _persons;
		//Insert country service dependency to person so that we can property of Country in person
		// /////////////////////////////////////////////
		private readonly ICountriesService _countriesService;
		// ////////////////////////////////////////////

		public PersonsService(bool initialize = true)
		{
			_persons = new List<Person>();
			_countriesService = new CountriesService();

			if(initialize)
			{
				
				
				
				
				
				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("91656F83-DC7A-4C84-BD74-BEDEBFCA8257"),
					PersonName = "Fredi",
					Email = "faldam0@japanpost.jp",
					DateOfBirth = Convert.ToDateTime("2018-02-13"),
					Gender = "Male",
					Address = "71968 Oxford Alley",
					ReceiveNewsLetters = false,
					CountryID = Guid.Parse("C9B6C39A-051A-4CA6-BBC3-827FFFD8ED00")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("BA5C534A-E317-49C7-944D-830FB2A83AC1"),
					PersonName = "Billye",
					Email = "bmorl1@comsenz.com",
					DateOfBirth = Convert.ToDateTime("1995-03-31"),
					Gender = "Female",
					Address = "324 Susan Crossing",
					ReceiveNewsLetters = true,
					CountryID = Guid.Parse("5E000C9C-0853-431D-A15B-44BF941BA658")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("F5B3F9CE-BD1D-4DDA-A60A-7871D011F7E4"),
					PersonName = "Waylan",
					Email = "wkeene2@gizmodo.com",
					DateOfBirth = Convert.ToDateTime("1993-01-25"),
					Gender = "Male",
					Address = "8841 Pennsylvania Park",
					ReceiveNewsLetters = true,
					CountryID = Guid.Parse("455413DC-3698-40A1-9ADB-70AA896A3FA8")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("6D4292C6-8C72-4741-845D-99FB9E68DCC4"),
					PersonName = "Major",
					Email = "mdower3@behance.net",
					DateOfBirth = Convert.ToDateTime("2017-12-03"),
					Gender = "Male",
					Address = "9237 Kipling Hill",
					ReceiveNewsLetters = true,
					CountryID = Guid.Parse("DE3FF7F7-7D54-45C2-906C-760387EBB2C8")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("831A60A1-64D9-42F5-B169-CB223E37A468"),
					PersonName = "Stevana",
					Email = "sstorrier4@discuz.net",
					DateOfBirth = Convert.ToDateTime("2003-01-08"),
					Gender = "Female",
					Address = "0 West Point",
					ReceiveNewsLetters = true,
					CountryID = Guid.Parse("BB2A4D1E-39B6-4793-A1B6-BC27501F8247")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("0FED8F31-514E-4AE4-B015-08E548B343E6"),
					PersonName = "Aleta",
					Email = "aabyss5@google.com.au",
					DateOfBirth = Convert.ToDateTime("1995-01-26"),
					Gender = "Female",
					Address = "76392 Clemons Road",
					ReceiveNewsLetters = true,
					CountryID = Guid.Parse("DE3FF7F7-7D54-45C2-906C-760387EBB2C8")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("3D928DD6-E1F8-4542-BD3E-CBCB7FE3DCB4"),
					PersonName = "Lazaro",
					Email = "lorable6@phpbb.com",
					DateOfBirth = Convert.ToDateTime("2010-02-21"),
					Gender = "Male",
					Address = "73941 Hovde Lane",
					ReceiveNewsLetters = true,
					CountryID = Guid.Parse("C9B6C39A-051A-4CA6-BBC3-827FFFD8ED00")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("A735D28C-3A28-4741-AC00-6AE521828912"),
					PersonName = "Lazaro",
					Email = "lorable6@phpbb.com",
					DateOfBirth = Convert.ToDateTime("2010-02-21"),
					Gender = "Male",
					Address = "73941 Hovde Lane",
					ReceiveNewsLetters = true,
					CountryID = Guid.Parse("BB2A4D1E-39B6-4793-A1B6-BC27501F8247")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("F332E5B3-3976-45F0-8F60-83A6731ED031"),
					PersonName = "Cletus",
					Email = "ccoger7@nasa.gov",
					DateOfBirth = Convert.ToDateTime("2016-03-04"),
					Gender = "Male",
					Address = "1818 Red Cloud Plaza",
					ReceiveNewsLetters = true,
					CountryID = Guid.Parse("5E000C9C-0853-431D-A15B-44BF941BA658")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("D3BD5C16-9897-41BC-9140-11204534A633"),
					PersonName = "Whit",
					Email = "wdenyer8@i2i.jp",
					DateOfBirth = Convert.ToDateTime("1999-03-31"),
					Gender = "Female",
					Address = "44309 Talisman Junction",
					ReceiveNewsLetters = false,
					CountryID = Guid.Parse("455413DC-3698-40A1-9ADB-70AA896A3FA8")
				});

				_persons.Add(new Person()
				{
					PersonID = Guid.Parse("68F0E1DF-5B9B-4184-894A-BDA5E4B16C34"),
					PersonName = "Quent",
					Email = "qstratz9@chron.com",
					DateOfBirth = Convert.ToDateTime("2015-12-05"),
					Gender = "Male",
					Address = "3516 Kipling Place",
					ReceiveNewsLetters = false,
					CountryID = Guid.Parse("DE3FF7F7-7D54-45C2-906C-760387EBB2C8")
				});
				

			}
		}

		private PersonResponse ConvertPersonToPersonResponse(Person person)
		{
			PersonResponse personResponse = person.ToPersonResponse();
			personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;

			return personResponse;
		}

		public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
		{
			if (personAddRequest == null) throw new ArgumentNullException(nameof(personAddRequest));


			//Model Validation
			ValidationHelper.ModelValidaton(personAddRequest);

			Person person = personAddRequest.ToPerson();
			
			
			//Generate PersonID
			person.PersonID = Guid.NewGuid();

			//Adding Person Object to Person List
			_persons.Add(person);

			//Convert Person object to PersonResponse Type

			/*PersonResponse personResponse =  person.ToPersonResponse();	
			personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;*/
			//?. Null collasing operator	                                               
			//or
			return ConvertPersonToPersonResponse(person);

		}

		public List<PersonResponse> GetAllPersons()
		{
			return _persons.Select(temp => temp.ToPersonResponse()).ToList();
		}

		public PersonResponse? GetPersonByPersonId(Guid? personId)
		{
			if(personId == null)
			{
				return null;
			}

			Person person =  _persons.FirstOrDefault(temp => temp.PersonID == personId);
			if(person == null)
			{
				return null;
			}
			return	person.ToPersonResponse();
		}

		public List<PersonResponse> GetFilteredPerson(string? searchBy, string? searchName)
		{
			List<PersonResponse> allPersons = GetAllPersons();
			List<PersonResponse> matchingPersons = allPersons;

			if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchName))
			{
				return matchingPersons;
			}

			switch (searchBy)
			{
				case nameof(Person.PersonName):
					matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.PersonName) 
					? temp.PersonName.Contains(searchName, StringComparison.OrdinalIgnoreCase) : true)).ToList();
					break;


				case nameof(Person.Email):
					matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Email)
					? temp.Email.Contains(searchName, StringComparison.OrdinalIgnoreCase) : true)).ToList();
					break;

				case nameof(Person.DateOfBirth):
					matchingPersons = allPersons.Where(temp => (temp.DateOfBirth != null)
					? temp.DateOfBirth.Value.ToString("dd MMM yyyy").Contains(searchName, StringComparison.OrdinalIgnoreCase) : true).ToList();
					break;

				case nameof(Person.Gender):
					matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Gender)
					? temp.Gender.Contains(searchName, StringComparison.OrdinalIgnoreCase) : true)).ToList();
					break;

				case nameof(Person.CountryID):
					matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Country)
					? temp.Country.Contains(searchName, StringComparison.OrdinalIgnoreCase) : true)).ToList();
					break;

				case nameof(Person.Address):
					matchingPersons = allPersons.Where(temp => (!string.IsNullOrEmpty(temp.Address)
					? temp.Address.Contains(searchName, StringComparison.OrdinalIgnoreCase) : true)).ToList();
					break;

				default: matchingPersons = allPersons; break;
			}

			return matchingPersons;
		}

		public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrderOption)
		{
			if (string.IsNullOrEmpty(sortBy))
			{
				return allPersons;
			}

			List<PersonResponse> sortedPersons = (sortBy, sortOrderOption)
				switch
			{
				(nameof(PersonResponse.PersonName), SortOrderOptions.ASC)
				=> allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.PersonName), SortOrderOptions.DESC)
				=> allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Email), SortOrderOptions.ASC)
				=> allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Email), SortOrderOptions.DESC)
				=> allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC)
				=> allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

				(nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC)
				=> allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

				(nameof(PersonResponse.Age), SortOrderOptions.ASC)
				=> allPersons.OrderBy(temp => temp.Age).ToList(),

				(nameof(PersonResponse.Age), SortOrderOptions.DESC)
				=> allPersons.OrderByDescending(temp => temp.Age).ToList(),

				(nameof(PersonResponse.Gender), SortOrderOptions.ASC)
				=> allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Gender), SortOrderOptions.DESC)
				=> allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Country), SortOrderOptions.ASC)
				=> allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Country), SortOrderOptions.DESC)
				=> allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Address), SortOrderOptions.ASC)
				=> allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.Address), SortOrderOptions.DESC)
				=> allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

				(nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC)
				=> allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

				(nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC)
				=> allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

				_=> allPersons
			};

			return sortedPersons;
		}

		public PersonResponse UpdateRequest(PersonUpdateRequest personUpdateRequest)
		{
			if(personUpdateRequest == null)
			{
				throw new ArgumentNullException(nameof(personUpdateRequest));
			}

			//Validation

			ValidationHelper.ModelValidaton(personUpdateRequest);

			//Get the matching person object to update
			Person? matchingPerson =  _persons.FirstOrDefault(temp=> temp.PersonID == personUpdateRequest.PersonID);

			if(matchingPerson == null)
			{
				throw new ArgumentException("Given person id doesn't exist");
			}

			//Update all details 
			matchingPerson.PersonName = personUpdateRequest.PersonName;
			matchingPerson.Email = personUpdateRequest.Email;
			matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
			matchingPerson.Gender = personUpdateRequest.Gender.ToString();
			matchingPerson.Address = personUpdateRequest.Address;
			matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

			return matchingPerson.ToPersonResponse();
		}

		public bool DeletePerson(Guid? personId)
		{
			if(personId == null)
			{
				throw new ArgumentNullException(nameof(personId));
			}	

			Person person = _persons.FirstOrDefault(temp => temp.PersonID == personId);
			if(person == null)
			{
				return false;
			}

			_persons.RemoveAll(temp => temp.PersonID == personId);

			return true;
		}
	}
}
