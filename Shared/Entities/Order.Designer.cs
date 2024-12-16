namespace Shared.Entities;
public partial class Order
{
    public void UpdateQuantity(int newQuantity)
    {
        Quantity = newQuantity;
    }
    public void ChangeProduct(int newProductId)
    {
        ProductId = newProductId;
    }
    public override string ToString()
    {
        return $"Order: {Id}, Product: {ProductId}, Quantity: {Quantity}, Date: {Date}";
    }
}
