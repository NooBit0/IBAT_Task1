using CsvHelper.Configuration;
using Task1.Model;

namespace Task1.FileExtensions
{
    public class CollectionMap : ClassMap<User>
    {
        CollectionMap()
        {
            Map(item => item.Date).Index(0);
            Map(item => item.FirstName).Index(1);
            Map(item => item.LastName).Index(2);
            Map(item => item.SurName).Index(3);
            Map(item => item.City).Index(4);
            Map(item => item.Country).Index(5);
        }
    }
}
