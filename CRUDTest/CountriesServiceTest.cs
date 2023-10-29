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
			_countriesServices = new CountriesService();
		}

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

			//Assert

			Assert.True(response.CountryId != Guid.Empty);
		}

	}
}
