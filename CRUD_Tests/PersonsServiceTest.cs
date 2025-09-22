using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace CRUD_Tests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;

        public PersonsServiceTest()
        {
            _personsService = new PersonsService();
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
    }
}
