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


		public override bool Equals(object? obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(CountryResponse)) return false;

			CountryResponse country_to_compare = obj as CountryResponse;

			return this.CountryId == country_to_compare.CountryId &&
				this.CountryName == country_to_compare.CountryName;
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
	}

	public static class CountryExtension
	{
		public static CountryResponse ToCountryResponse(this Country countnry)
		{
			return new CountryResponse() { CountryId = countnry.CountryId, CountryName = countnry.CountryName };	
		}
	}
}
