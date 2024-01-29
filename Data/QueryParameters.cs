namespace HotelHosting.Data
    {
    public class QueryParameters
        {
        private int pageSize = 10;
        public int startIndex { get; set; }
        public int pageNumber { get; set; }
        public int PageSize
            {
            get
                {
                return pageSize;
                }
            set
                {
                pageSize = value;
                }
            }
        }
    }
