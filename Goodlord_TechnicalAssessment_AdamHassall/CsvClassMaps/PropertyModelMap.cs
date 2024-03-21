using CsvHelper.Configuration;

namespace Goodlord_TechnicalAssessment_AdamHassall.CsvClassMaps
{
    public class PropertyModelMap: ClassMap<Property>
    {
        public PropertyModelMap() 
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Address).Name("Address");
            Map(m => m.PricePerCalandarMonth).Name("Price (pcm)");
        }
    }
}
