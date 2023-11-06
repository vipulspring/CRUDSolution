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

		/// <summary>
		/// Returns all countries from the list.
		/// </summary>
		/// <returns></returns>
		List<CountryResponse> GetAllCountries();

		/// <summary>
		/// Returns a country object based on the given country id
		/// </summary>
		/// <param name="CountryID">CountryID (Guid) to search</param>
		/// <returns>atching country as CountryResponse Object</returns>
		CountryResponse? GetCountryByCountryID(Guid? CountryID);

		
	}
}