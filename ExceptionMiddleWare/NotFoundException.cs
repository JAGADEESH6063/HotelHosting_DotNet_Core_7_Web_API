namespace HotelHosting.ExceptionMiddleWare
    {
    public class NotFoundException : ApplicationException
        {
        public NotFoundException(string name, object key) : base($"{name} with {key} was not found")
            {

            }
        }
    }
