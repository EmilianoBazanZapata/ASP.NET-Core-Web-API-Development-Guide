namespace HotelListing.API.Middleware
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} with Id ({key}) was not found")
        {

        }
    }
}
