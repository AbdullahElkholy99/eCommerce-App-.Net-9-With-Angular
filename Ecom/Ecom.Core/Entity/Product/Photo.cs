using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.Entity.Product;
public class Photo:BaseEntity<int>
{

    public string  Name { get; set; }

    // Relation between photo and product (m:1)
    public int ProductId{ get; set; }
    //[ForeignKey(nameof(ProductId))]
    //public Product Product{ get; set; }




}
