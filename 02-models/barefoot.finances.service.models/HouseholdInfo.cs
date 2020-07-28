using System.Collections.Generic;
using Newtonsoft.Json;

namespace barefoot.finances.service.models
{
    public class HouseholdInfo
    {
        public int NumberOfPeopleWithIncome { get; set; }
        public List<PersonIncome> PersonInformation { get; set; }

        public HouseholdInfo()
        {
            PersonInformation = new List<PersonIncome>();
        }
    }
}