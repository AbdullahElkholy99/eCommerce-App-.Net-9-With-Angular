namespace Ecom.Core.Sharing;

public class ProductParams
{

    public string Sort { get; set; }
    public int? CategoryId { get; set; }

    private int MaxPageSize { get; set; } = 6;
    private int _pageSize { get; set; }
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
    }

    public int PageNumber { get; set; } = 1;


    public string Search { get; set; }
}
