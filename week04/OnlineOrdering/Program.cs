using System;
using System.Collections.Generic;

class Address
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    public bool IsInUSA() => _country == "USA";

    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}

class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public bool LivesInUSA() => _address.IsInUSA();

    public string GetName() => _name;
    public Address GetAddress() => _address;
}

class Product
{
    private string _name;
    private string _productId;
    private double _pricePerUnit;
    private int _quantity;

    public Product(string name, string productId, double pricePerUnit, int quantity)
    {
        _name = name;
        _productId = productId;
        _pricePerUnit = pricePerUnit;
        _quantity = quantity;
    }

    public double GetTotalCost() => _pricePerUnit * _quantity;

    public string GetName() => _name;
    public string GetProductId() => _productId;
}

class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalCost()
    {
        double total = 0;
        foreach (Product p in _products)
            total += p.GetTotalCost();

        total += _customer.LivesInUSA() ? 5.0 : 35.0;
        return total;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product p in _products)
            label += $"  {p.GetName()} (ID: {p.GetProductId()})\n";
        return label.TrimEnd();
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n  {_customer.GetName()}\n  {_customer.GetAddress().GetFullAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Order 1 — US customer
        Address address1 = new Address("742 Evergreen Terrace", "Springfield", "IL", "USA");
        Customer customer1 = new Customer("Homer Simpson", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Donut Box (12 pack)", "DNT-012", 8.99, 2));
        order1.AddProduct(new Product("Power Plant Safety Manual", "PPM-001", 14.50, 1));
        order1.AddProduct(new Product("Bowling Ball", "SPT-047", 29.99, 1));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Order Total: ${order1.GetTotalCost():F2}");

        Console.WriteLine("\n" + new string('-', 40) + "\n");

        // Order 2 — international customer
        Address address2 = new Address("10 Downing Street", "London", "England", "UK");
        Customer customer2 = new Customer("Winston Blake", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Mechanical Keyboard", "ELC-204", 89.99, 1));
        order2.AddProduct(new Product("USB-C Hub", "ELC-088", 34.99, 2));

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Order Total: ${order2.GetTotalCost():F2}");
    }
}
