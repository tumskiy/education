class Person
{
    public string name;
    public string surname;
    public string OTCHESTVO;
}
class Employee : Person
{
    public int serialnumber;
    public string departmentCode;
    public void buyItem(Store store)
    {
        store.Sell();
    }
}
class Customer : Person
{
    public int countByu;
    public bool isBuy;
}
class Item
{
    public string title;
    public double price;
    public int quantity;
    public Item(string title, double price, int quantity)
    {
        this.title = title;
        this.price = price;
        this.quantity = quantity;
    }
    public Item() { }
}
class Store
{
    Item[] items = { new Item("burenka", 78.99, 1000), new Item("bun", 567.567, 676767) };
    public Item Sell()
    {
        return items[1];
    }
}

static class Program
{
    static public void Main()
    {

    }

}