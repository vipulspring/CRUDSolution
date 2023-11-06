using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
	public class CountriesService : ICountriesService
	{
		//Private List
		private readonly List<Country> _countries;

		//Constructor
		public CountriesService(bool initialize = true)
		{
			_countries = new List<Country>();
			if (initialize)
			{
				_countries.AddRange(new List<Country>() {
				new Country() { CountryId = Guid.Parse("C9B6C39A-051A-4CA6-BBC3-827FFFD8ED00"), CountryName = "USA" },
				new Country() { CountryId = Guid.Parse("5E000C9C-0853-431D-A15B-44BF941BA658"), CountryName = "Canada" },
				new Country() { CountryId = Guid.Parse("455413DC-3698-40A1-9ADB-70AA896A3FA8"), CountryName = "UK" },
				new Country() { CountryId = Guid.Parse("DE3FF7F7-7D54-45C2-906C-760387EBB2C8"), CountryName = "India" },
				new Country() { CountryId = Guid.Parse("BB2A4D1E-39B6-4793-A1B6-BC27501F8247"), CountryName = "Australia" },
			});
			}
		}
		

		public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
		{
			//Validation: CountryAddRequest parameter cannot be null
			if(countryAddRequest == null)
			{
				throw new ArgumentNullException(nameof(countryAddRequest));
			}

			//Validation: Country name cannot be Null
			if(countryAddRequest.CountryName == null)
			{
				throw new ArgumentException(nameof(countryAddRequest.CountryName));
			}

			//Validation: Country name cannot be duplicate
			if (_countries.Where(temp => temp.CountryName == 
			countryAddRequest.CountryName).Count() > 0)
			{
				throw new ArgumentException("Given Country name already exists");
			}

			//Convert CountryAddRequest to Country type.
			Country country =  countryAddRequest.ToCountry();

			//Generate Country ID
			country.CountryId = Guid.NewGuid();

			_countries.Add(country);
			return country.ToCountryResponse();
		}

		public List<CountryResponse> GetAllCountries()
		{
			return _countries.Select(country =>  country.ToCountryResponse()).ToList();
		}

		public CountryResponse? GetCountryByCountryID(Guid? CountryID)
		{
			if(CountryID == null)
			{
				return null;
			}

		 	Country? country = _countries.FirstOrDefault(temp => temp.CountryId == CountryID);

			if(country == null)
			{
				return null;
			}

			return country.ToCountryResponse();
		}
	}
}