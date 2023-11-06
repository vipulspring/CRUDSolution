using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using Entities;
using Services;
using ServiceContracts.DTO;

namespace CRUDTest
{
	public class CountriesServiceTest
	{

		private readonly ICountriesService _countriesServices;
		public CountriesServiceTest()
		{
			_countriesServices = new CountriesService(false);
		}

		#region AddCountry
		// When CountryAddRequest is null, it should throw ArgumentNullException
		[Fact]
		public void AddCountry_NullCountry()
		{
			//Arrange
			CountryAddRequest? request = null;

			//Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				//Act
				_countriesServices.AddCountry(request);
			});
		}

		// When CountryName is null, it should throw ArgumentException
		[Fact]
		public void AddCountry_CountryNameIsN()
		{
			//Arrange
			CountryAddRequest? request = new CountryAddRequest() { CountryName = null};

			//Assert
			Assert.Throws<ArgumentException>(() =>
			{
				//Act
				_countriesServices.AddCountry(request);
			});
		}

		// When CountryName is duplicate, it should throw ArgumentException
		[Fact]
		public void AddCountry_DuplicateCountryName()
		{
			//Arrange
			CountryAddRequest? request1 = new CountryAddRequest()
			{ CountryName = "USA" };
			CountryAddRequest? request2 = new CountryAddRequest() 
			{ CountryName = "USA" };

			//Assert
			Assert.Throws<ArgumentException>(() =>
			{
				//Act
				_countriesServices.AddCountry(request1);
				_countriesServices.AddCountry(request2);
			});
		}

		//When you supply proper CountryName, it should insert (add) the
		//country to the existing list of count

		[Fact]
		public void AddCountry_ProperCountryDetails()
		{
			//Arrange
			CountryAddRequest? request1 = new CountryAddRequest()
			{ CountryName = "Japan" };
			
			
				//Act
				CountryResponse response = _countriesServices.AddCountry(request1);
			List<CountryResponse> countries_from_GetAllCountries = _countriesServices.GetAllCountries();

			//Assert
			Assert.True(response.CountryId != Guid.Empty);
			Assert.Contains(response, countries_from_GetAllCountries);
		}	

		#endregion


		#region GetAllCountries

		[Fact]
		//The list of countries should be empty by default (Before adding any countries)
		public void GetAllCountries_EmptyList()
		{
			//Act
			List<CountryResponse> actual_country_response_list = _countriesServices
				.GetAllCountries();


			//Assert
			Assert.Empty(actual_country_response_list);

		}

		[Fact]
		public void GetAllCountries_AddFewContries()
		{
			//Arrange
			List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
			{
				new CountryAddRequest() {CountryName = "India"},
				new CountryAddRequest() {CountryName = "USA"}
			};


			List<CountryResponse> country_list_from_add_country = new List<CountryResponse>(); 

			//Act
			foreach(CountryAddRequest country_request in country_request_list)
			{
				country_list_from_add_country.Add(_countriesServices.AddCountry(country_request));
			}

			List<CountryResponse> actualCountryResponseList = _countriesServices.GetAllCountries();

			//Read each elements from country_list_from_add_country
			foreach(CountryResponse expected_country in country_list_from_add_country)
			{
				Assert.Contains(expected_country, actualCountryResponseList);
			}
		}

		#endregion

		#region GetcountryByCountryID

		[Fact]
		//If we supply null as countryID, it should return null as CountryResponse
		public void GetCountryByCountryID_NullCountryID()
		{
			//Arrange
			Guid? countryID = null;

			//Act
			CountryResponse? country_response_from_get_method =  
				_countriesServices.GetCountryByCountryID(countryID);

			//Assert
			Assert.Null(country_response_from_get_method);
		}

		[Fact]
		//If we supply valid country id, it should return the matching
		//country details as CountryResponse object

		public void GetCountryByCountryID_ValidCountryID()
		{
			//Arrange
			CountryAddRequest? country_add_request = 
				new CountryAddRequest() { CountryName = "China" };
			CountryResponse? country_response_from_add = 
				_countriesServices.AddCountry(country_add_request);

			//Act
			CountryResponse? country_response_from_get=
			_countriesServices.GetCountryByCountryID(country_response_from_add.CountryId);

			//Assert

			Assert.Equal(country_response_from_add, country_response_from_get);
		}

		#endregion
	}
}
