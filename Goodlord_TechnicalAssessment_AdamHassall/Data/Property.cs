public class Property
{
    public int Id { get; set; }
    public string Address { get; set; }
    public decimal PricePerCalandarMonth { get; set; }

    public Property(int id, string address, decimal pricePerCalandar)
    {
        Id = id;
        Address = address;
        PricePerCalandarMonth = pricePerCalandar;
    }

    public Property()
    {

    }
}
