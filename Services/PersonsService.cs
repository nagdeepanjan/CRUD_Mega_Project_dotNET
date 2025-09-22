using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        public PersonsService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //Check if argument is null
            if (personAddRequest is null)
                throw new ArgumentNullException(nameof(personAddRequest));

            //check PersonName (this is more validations are now being done with model validations)
            //if (string.IsNullOrEmpty(personAddRequest.PersonName))
            //    throw new ArgumentException(nameof(personAddRequest));

            ValidationHelper.ModelValidation(personAddRequest);

            //convert personAddRequest to Person type
            Person person = personAddRequest.ToPerson();
            person.PersonID = Guid.NewGuid();

            //add Person to persons lisst
            _persons.Add(person);

            //Convert Person object to PersonResponse type
            return ConvertPersonToPersonResponse(person);

        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            //Convert Person object to PersonResponse type
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }
    }
}
