using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// DTO Class that is used as return type for most of CountriesService methods
	/// </summary>
	public class CountryResponse
	{
		public Guid CountryId { get; set; }
		public string? CountryName { get; set; }	

	}

	public static class CountryExtension
	{
		public static CountryResponse ToCountryResponse(this Country countnry)
		{
			return new CountryResponse() { CountryId = countnry.CountryId, CountryName = countnry.CountryName };	
		}
	}
}
