using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CRUDTest
{
	public class PersonsServicesTest
	{
		private readonly IPersonsService _personsService;
		private readonly ICountriesService _countriesService;
		private readonly ITestOutputHelper _testOutputHelper;

		public PersonsServicesTest(ITestOutputHelper testOutputHelper)
		{
			_personsService = new PersonsService();
			_countriesService = new CountriesService(false);
			_testOutputHelper = testOutputHelper;
		}

		#region AddPerson
		[Fact]
		//When we supply null value as PersonAddRequest, it shold throw ArgumentNullException
		public void AddPerson_NullPerson()
		{
			//Arrange
			PersonAddRequest? personAddRequest = null;

			//Act
			Assert.Throws<ArgumentNullException>(() =>
			{
				_personsService.AddPerson(personAddRequest);
			});
		}

		[Fact]
		public void AddPerson_PersonNameIsNull()
		{
			//Arrange 
			PersonAddRequest? personAddRequest = new PersonAddRequest()
			{
				PersonName = null
			};

			//Act
			Assert.Throws<ArgumentException>(() =>
			{
				_personsService.AddPerson(personAddRequest);
			});
		}

		[Fact]
		public void AddPerson_ProperPersonDetails()
		{
			//Arrange
			PersonAddRequest? personAddRequest = new PersonAddRequest()
			{
				PersonName = "Vipul",
				Email = "Vipul@gmail.com",
				Address = "Bj368 Sector21, Kolkata",
				CountryID = Guid.NewGuid(),
				DateOfBirth = DateTime.Parse("1990-09-21"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			//Act
			PersonResponse person =  _personsService.AddPerson(personAddRequest);
			List<PersonResponse> personList = _personsService.GetAllPersons();

			//Assert
			Assert.True(person.PersonID != Guid.Empty);
			Assert.Contains(person, personList); 
		}

		#endregion


		#region GetPersonByPersonID

		//If we supply null as PersonID, it should return null as PersonResponse
		[Fact]
		public void GetPersonByPersonID_NullPersonID()
		{
			//Arrange
			Guid? personID = null;

			//Act
			PersonResponse? person = _personsService.GetPersonByPersonId(personID);

			//Assert
			Assert.Null(person);
		}

		//If we supply valid personID we should return the valid person details as PersonResponse object
		[Fact]
		public void GetPersonByPersonID_ValidPersonID()
		{
			//Arrange

			CountryAddRequest countryAddRequest = new CountryAddRequest()
			{
				CountryName = "India"
			};
			CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);



			PersonAddRequest? personAddRequest = new PersonAddRequest()
			{
				PersonName = "Vipul",
				Email = "Vipul@gmail.com",
				Address = "Bj368 Sector21, Kolkata",
				CountryID = countryResponse.CountryId,
				DateOfBirth = DateTime.Parse("1990-09-21"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			//Act
			PersonResponse personResponse_from_add =  _personsService.AddPerson(personAddRequest);

			PersonResponse personResponse_from_get =  _personsService.GetPersonByPersonId(personResponse_from_add.PersonID);


			//Assert
			Assert.Equal(personResponse_from_add, personResponse_from_get);
		}


		#endregion


		#region GetAllPersons

		//The GetAllPersons() should return an empty list by default
		[Fact]
		public void GetAllPersons_EmptyList()
		{
			//Act
			List<PersonResponse> person_from_get = _personsService.GetAllPersons();

			//Assert
			Assert.Empty(person_from_get);
		}

		//First we will add few persons; and then when we call GetAllPersons(),
		//it should return the same persons that were added
		[Fact]
		public void GetAllPersons_AddFewpersons()
		{
			//Arrange
			CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "India" };
			CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Nepal" };

			CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
			CountryResponse countryResponse2 =  _countriesService.AddCountry(countryAddRequest2);


			PersonAddRequest personAddRequest1 = new PersonAddRequest()
			{
				PersonName = "Vipul",
				Email = "Vipul@gmail.com",
				Address = "Bj368 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1990-09-21"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonAddRequest personAddRequest2 = new PersonAddRequest()
			{
				PersonName = "Mukul",
				Email = "Mukul@gmail.com",
				Address = "Bj369 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1994-01-29"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonAddRequest personAddRequest3 = new PersonAddRequest()
			{
				PersonName = "Riya",
				Email = "Riya@gmail.com",
				Address = "Bj370 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1995-04-15"),
				Gender = GenderOptions.Female	,
				ReceiveNewsLetters = true
			};

			List<PersonAddRequest> person_requests =
				new List<PersonAddRequest> { personAddRequest1, personAddRequest2, personAddRequest3 };

			List<PersonResponse> person_response = new List<PersonResponse>();

			foreach (PersonAddRequest personAddRequest in person_requests)
			{
				PersonResponse person =  _personsService.AddPerson(personAddRequest);

				person_response.Add(person);
			}

			//Act
			List<PersonResponse> persons_list_from_get = _personsService.GetAllPersons();

			//Assert
			foreach(PersonResponse personResponsefromAdd in person_response)
			{
				Assert.Contains(personResponsefromAdd, persons_list_from_get);
			}
		}


		#endregion

		#region GetFilteredPersons
		[Fact]
		public void GetFilteredPerson_EmptySearchText()
		{
			//Arrange
			//Arrange
			CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "India" };
			CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Nepal" };

			CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
			CountryResponse countryResponse2 = _countriesService.AddCountry(countryAddRequest2);


			PersonAddRequest personAddRequest1 = new PersonAddRequest()
			{
				PersonName = "Vipul",
				Email = "Vipul@gmail.com",
				Address = "Bj368 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1990-09-21"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonAddRequest personAddRequest2 = new PersonAddRequest()
			{
				PersonName = "Mukul",
				Email = "Mukul@gmail.com",
				Address = "Bj369 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1994-01-29"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonAddRequest personAddRequest3 = new PersonAddRequest()
			{
				PersonName = "Riya",
				Email = "Riya@gmail.com",
				Address = "Bj370 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1995-04-15"),
				Gender = GenderOptions.Female,
				ReceiveNewsLetters = true
			};

			List<PersonAddRequest> person_list_from_search =
				new List<PersonAddRequest> { personAddRequest1, personAddRequest2, personAddRequest3 };

			List<PersonResponse> person_response = new List<PersonResponse>();

			foreach (PersonAddRequest personAddRequest in person_list_from_search)
			{
				PersonResponse person = _personsService.AddPerson(personAddRequest);

				person_response.Add(person);
			}

			//Act
			List<PersonResponse> persons_list_from_get = _personsService.GetFilteredPerson(nameof(Person.PersonName),"");

			//Assert
			foreach (PersonResponse personResponsefromAdd in person_response)
			{

				
						Assert.Contains(personResponsefromAdd, persons_list_from_get);
				
			}
		}

		[Fact]
		public void GetFilteredPersons_SearchByPersonName()
		{
			//Arrange
			//Arrange
			CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "India" };
			CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Nepal" };

			CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
			CountryResponse countryResponse2 = _countriesService.AddCountry(countryAddRequest2);


			PersonAddRequest personAddRequest1 = new PersonAddRequest()
			{
				PersonName = "Vipul",
				Email = "Vipul@gmail.com",
				Address = "Bj368 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1990-09-21"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonAddRequest personAddRequest2 = new PersonAddRequest()
			{
				PersonName = "Mukul",
				Email = "Mukul@gmail.com",
				Address = "Bj369 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1994-01-29"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonAddRequest personAddRequest3 = new PersonAddRequest()
			{
				PersonName = "Riya",
				Email = "Riya@gmail.com",
				Address = "Bj370 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1995-04-15"),
				Gender = GenderOptions.Female,
				ReceiveNewsLetters = true
			};

			List<PersonAddRequest> person_list_from_search =
				new List<PersonAddRequest> { personAddRequest1, personAddRequest2, personAddRequest3 };

			List<PersonResponse> person_response = new List<PersonResponse>();

			foreach (PersonAddRequest personAddRequest in person_list_from_search)
			{
				PersonResponse person = _personsService.AddPerson(personAddRequest);

				person_response.Add(person);
			}

			//Act
			List<PersonResponse> persons_list_from_get = _personsService.GetFilteredPerson(nameof(Person.PersonName), "ul");

			//Assert
			foreach (PersonResponse personResponsefromAdd in person_response)
			{

				if (personResponsefromAdd != null)
				{
					if (personResponsefromAdd.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
					{
						Assert.Contains(personResponsefromAdd, persons_list_from_get);
					}
				}
			}
		}
		#endregion

		#region GetSortedPerson

		[Fact]
		public void GetSortedPerson()
		{
			//Arrange
			//Arrange
			CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "India" };
			CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Nepal" };

			CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
			CountryResponse countryResponse2 = _countriesService.AddCountry(countryAddRequest2);


			PersonAddRequest personAddRequest1 = new PersonAddRequest()
			{
				PersonName = "Vipul",
				Email = "Vipul@gmail.com",
				Address = "Bj368 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1990-09-21"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonAddRequest personAddRequest2 = new PersonAddRequest()
			{
				PersonName = "Mukul",
				Email = "Mukul@gmail.com",
				Address = "Bj369 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1994-01-29"),
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonAddRequest personAddRequest3 = new PersonAddRequest()
			{
				PersonName = "Riya",
				Email = "Riya@gmail.com",
				Address = "Bj370 Sector21, Kolkata",
				CountryID = countryResponse1.CountryId,
				DateOfBirth = DateTime.Parse("1995-04-15"),
				Gender = GenderOptions.Female,
				ReceiveNewsLetters = true
			};

			List<PersonAddRequest> person_list_from_search =
				new List<PersonAddRequest> { personAddRequest1, personAddRequest2, personAddRequest3 };

			List<PersonResponse> person_response = new List<PersonResponse>();

			foreach (PersonAddRequest personAddRequest in person_list_from_search)
			{
				PersonResponse person = _personsService.AddPerson(personAddRequest);

				person_response.Add(person);
			}

			//print person_response_list_from_add
			_testOutputHelper.WriteLine("Expected");
			foreach (PersonResponse person_response_from_add in person_response)
			{
				_testOutputHelper.WriteLine(person_response_from_add.ToString());
			}

			List<PersonResponse> allPersons = 
				_personsService.GetAllPersons();

			//Act
			List<PersonResponse> persons_list_from_sort = 
				_personsService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC);

			//Print person list from sort
			_testOutputHelper.WriteLine("Actual:");
			foreach(PersonResponse person_list_from_get in persons_list_from_sort)
			{
				_testOutputHelper.WriteLine(person_list_from_get.ToString());
			}


			person_response = person_response.OrderByDescending(temp => temp.PersonName).ToList();

			//Assert
			for(int i = 0; i < person_response.Count; i++)
			{
				Assert.Equal(person_response, persons_list_from_sort); 
			}


			
		}

		#endregion

		#region UpdatePerson

		//When we supply null value as PersonupdateRequest, it should throw ArgumentNullException

		[Fact]
		public void UpdatePerson_NullPerson()
		{
			//Arrange
			PersonUpdateRequest? person_update_request = null;

			//Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				//Act
				_personsService.UpdateRequest(person_update_request);
			});

			
		}
		//When PersonId is invalid it should throw ArgumentException
		[Fact]
		public void UpdatePerson_InvalidPersonID()
		{
			//Arrange
			PersonUpdateRequest? person_update_request = new PersonUpdateRequest()
			{
				PersonID = Guid.NewGuid()
			};

			//Assert
			Assert.Throws<ArgumentException>(() =>
			{
				//Act
				_personsService.UpdateRequest(person_update_request);
			});


		}

		//When PersonName is null it should throw ArgumentException
		[Fact]
		public void UpdatePerson_PersonNameIsNull()
		{
			//Arrange 
			CountryAddRequest country_add_request = new CountryAddRequest()
			{
				CountryName = "UK"
			};

			CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

			PersonAddRequest person_add_request = new PersonAddRequest()
			{
				PersonName = "John",
				CountryID = country_response_from_add.CountryId,
				Email = "copy@cat.com",
				Address = "....Address",
				Gender = GenderOptions.Male
			};

			PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

			PersonUpdateRequest person_update_request =  person_response_from_add.ToPersonUpdateRequest();

			person_update_request.PersonName = null;

			//Assert
			Assert.Throws<ArgumentException>(() =>
			{
				//Act
				_personsService.UpdateRequest(person_update_request);
			});
		}

		//First add a new Person and try to update the person name and email
		[Fact]
		public void UpdatePerson_PersonFullDetailsUpdation()
		{
			//Arrange 
			CountryAddRequest country_add_request = new CountryAddRequest()
			{
				CountryName = "UK"
			};

			CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

			PersonAddRequest person_add_request = new PersonAddRequest()
			{
				PersonName = "John",
				CountryID = country_response_from_add.CountryId,
				Address = "abcd Road",
				DateOfBirth = DateTime.Parse("2000-10-12"),
				Email = "abc@gmail.com",
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

			PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();

			person_update_request.PersonName = "Vipul";
			person_update_request.Email = "xyz@gmail.com";

			//Act
			PersonResponse person_respose_from_update = _personsService.UpdateRequest(person_update_request);

			PersonResponse person_reponse_from_get = _personsService.GetPersonByPersonId(person_respose_from_update.PersonID);


			//Assert
			Assert.Equal(person_reponse_from_get, person_respose_from_update);
		}

		#endregion

		#region DeletePerson

		//If you supply valid PersonID it should return true
		[Fact]
		public void DeletePerson_ValidPersonID()
		{
			//Arrange
			CountryAddRequest country_add_request = new CountryAddRequest()
			{
				CountryName = "India"
			};
			CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

			PersonAddRequest person_add_request = new PersonAddRequest()
			{
				PersonName = "Vipul Singh",
				Address = "vipul@test.in",
				CountryID = country_response_from_add.CountryId,
				DateOfBirth = Convert.ToDateTime("1990-09-21"),
				Email = "Vipul@gmail.com",
				Gender = GenderOptions.Male,
				ReceiveNewsLetters = true
			};

			PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);


			//Act
			bool isDeleted = _personsService.DeletePerson(person_response_from_add.PersonID);

			//Assert
			Assert.True(isDeleted);

		}

		//If you supply invalid PersonID it should return false
		[Fact]
		public void DeletePerson_InvalidPersonID()
		{
		
			//Act
			bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());

			//Assert
			Assert.False(isDeleted);

		}

		#endregion
	}
}
