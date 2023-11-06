using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
	public interface IPersonsService
	{
		PersonResponse AddPerson(PersonAddRequest? personAddRequest);
		List<PersonResponse> GetAllPersons();

		PersonResponse? GetPersonByPersonId(Guid? personId);

		List<PersonResponse> GetFilteredPerson(string? searchBy, string? searchName);

		List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrderOption);


		/// <summary>
		/// Update the specified person detail based on the given person ID
		/// </summary>
		/// <param name="personUpdateRequest">Person Detail to update, including person id</param>
		/// <returns>Return the Person Response object after updation</returns>
		PersonResponse UpdateRequest(PersonUpdateRequest personUpdateRequest);

		/// <summary>
		/// Delete person based on personid
		/// </summary>
		/// <param name="personId"></param>
		/// <returns>if delete is successfully it returns true or false if person id is incorrect</returns>
		bool DeletePerson(Guid? personId);
	}

}
