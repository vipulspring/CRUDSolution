using ServiceContracts.DTO;

namespace ServiceContracts
{
	/// <summary>
	/// Represent business logic for manipultating Country Entity
	/// </summary>
	public interface ICountriesService
	{
		/// <summary>
		/// Adds a country object to the list of countries
		/// </summary>
		/// <param name="countryAddRequest">Country Object to add</param>
		/// <returns></returns>
		CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
	}
}