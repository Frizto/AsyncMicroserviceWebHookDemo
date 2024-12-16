namespace Shared.Entities;
public partial class Product
{
    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }

    public void RenameProduct(string newName)
    {
        Name = newName;
    }

    public override string ToString()
    {
        return $"Product: {Name}, Price: {Price:C}";
    }
}
