using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonsService
    {

        /// <summary>
        /// Adds a new person into the list of persons
        /// </summary>
        /// <param name="personAddRequest">Person to add</param>
        /// <returns>Returns the same person details. along with the newly generated PersonID</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);


        /// <summary>
        /// Returns all persons from the repo
        /// </summary>
        /// <returns>List of PersonResponse</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Peturns the person object (as Personresponse) based on the given person id
        /// </summary>
        /// <param name="personID">Person id to search</param>
        /// <returns>Returns matching Person object</returns>
        PersonResponse? GetPersonByPersonID(Guid? personID);
    }
}
