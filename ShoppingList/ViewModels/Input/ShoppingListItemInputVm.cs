namespace ShoppingListApp.ViewModels.Input
{
    public class ShoppingListItemInputVm
    {
        public int ShoppingListId { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public bool IsChecked { get; set; }
    }
}
