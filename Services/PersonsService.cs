using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

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

            //check PersonName
            if (string.IsNullOrEmpty(personAddRequest.PersonName))
                throw new ArgumentException(nameof(personAddRequest));

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
