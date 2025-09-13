using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }



        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            #region Validations
            //Validation: countryAddRequest parameter cannot be empty
            if (countryAddRequest is null)
                throw new ArgumentNullException(nameof(countryAddRequest));


            //Validation: countryAddRequest.CountryName cannot be null
            if (countryAddRequest.CountryName is null)
                throw new ArgumentException(nameof(countryAddRequest.CountryName));


            //Validation: countryAddRequest.CountryName duplicates are not allowed
            if (_countries.Any(c => c.CountryName.ToLower() == countryAddRequest.CountryName.ToLower()))
                throw new ArgumentException("Given country already exists");
            #endregion

            //Convert CountryAddRequest object to Country object
            Country country = countryAddRequest.ToCountry();

            //Generate and add a new GUID for this country
            country.CountryID = Guid.NewGuid();

            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(c => c.ToCountryResponse()).ToList();
        }
    }
}
