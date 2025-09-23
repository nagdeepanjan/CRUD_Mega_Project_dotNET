using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace CRUD_Tests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;

        public PersonsServiceTest()
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService();
        }

        #region TEST AddPerson()

        //When we supply null value as PersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            // Arrange
            PersonAddRequest? personAddRequest = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _personsService.AddPerson(personAddRequest));
        }

        //When we supply null value as PersonName, it should throw an ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            // Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _personsService.AddPerson(personAddRequest));
        }

        //When we supply proper person details, it should insert the person into the list and return the PersonResponse with the newly generated PersonID
        [Fact]
        public void AddPerson_ProperPersonsDetails()
        {
            // Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest
            {
                PersonName = "John Doe",
                Email = "john.doe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = GenderOptions.Male,
                Address = "123 Main St",
                ReceiveNewsLetters = true,
                CountryID = Guid.NewGuid()
            };

            // Act
            PersonResponse personResponse_from_add = _personsService.AddPerson(personAddRequest);
            List<PersonResponse> persons_list = _personsService.GetAllPersons();

            //Assert
            Assert.True(personResponse_from_add.PersonID != Guid.Empty);
            Assert.Contains(personResponse_from_add, persons_list);


        }

        #endregion

        #region TEST GetPersonByPersonID()
        //If we supply null s PersonID, it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            //Arrange
            Guid? personID = null;

            //Act
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID(personID);

            //Assert
            Assert.Null(personID);
        }

        //If we supply a valid person id, it should return the valid person details as PersonResponse object
        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            //Arrange
            CountryAddRequest country_request = new CountryAddRequest { CountryName = "Mexico" };
            CountryResponse country_response = _countriesService.AddCountry(country_request);


            //Act
            PersonAddRequest person_request = new PersonAddRequest
            {
                PersonName = "Beethoven",
                CountryID = country_response.CountryID,
                Gender = GenderOptions.Male,
                Address = "Bonn",
                ReceiveNewsLetters = false,
                DateOfBirth = new DateTime(1770, 1, 1),
                Email = "beethoven@music.com"
            };
            PersonResponse person_response_from_add = _personsService.AddPerson(person_request);

            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID(person_response_from_add.PersonID);

            //Assert
            Assert.Equal(person_response_from_add, person_response_from_get);
        }


        #endregion

        #region TEST GetAllPersons()
        //The GetALlPersons() should return empty list by default

        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Arrange
            //Act
            List<PersonResponse> persons_from_get = _personsService.GetAllPersons();

            //Assert
            Assert.Empty(persons_from_get);
        }



        //First, we will add a few persons, and then when we call GetAllPersons(), it should return the same persons that were added 
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            CountryAddRequest country_request_1 = new CountryAddRequest { CountryName = "Japan" };
            CountryAddRequest country_request_2 = new CountryAddRequest { CountryName = "China" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest
            {
                PersonName = "Sakura",
                Email = "sakura@japan.com",
                DateOfBirth = new DateTime(1985, 3, 21),
                Gender = GenderOptions.Female,
                Address = "Tokyo",
                ReceiveNewsLetters = true,
                CountryID = country_response_1.CountryID
            };

            PersonAddRequest person_request_2 = new PersonAddRequest
            {
                PersonName = "Li",
                Email = "li@china.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Gender = GenderOptions.Male,
                Address = "Beijing",
                ReceiveNewsLetters = false,
                CountryID = country_response_2.CountryID
            };

            PersonAddRequest person_request_3 = new PersonAddRequest
            {
                PersonName = "Manu",
                Email = "sakura@japan.com",
                DateOfBirth = new DateTime(1988, 3, 8),
                Gender = GenderOptions.Male,
                Address = "Djakarta",
                ReceiveNewsLetters = true,
                CountryID = country_response_1.CountryID
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest> { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }




            //Act
            List<PersonResponse> persons_list_from_get = _personsService.GetAllPersons();

            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);

            }



        }

        #endregion
    }
}
