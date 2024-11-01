using System;
using System.Reflection;

public class Product
{
    private string productName;               
    public int stock;                         
    protected decimal price;                 
    internal bool isAvailable;                
    public static string category = "Electronics"; 

    public Product(string name, int stock, decimal price, bool available)
    {
        productName = name;
        this.stock = stock;
        this.price = price;
        isAvailable = available;
    }

    public void DisplayProduct()
    {
        Console.WriteLine($"Product: {productName}, Stock: {stock}, Price: {price}, Available: {isAvailable}");
    }

    public void SetStock(int newStock)
    {
        stock = newStock;
        Console.WriteLine($"Stock updated to {stock}");
    }

    private void CalculateDiscount(decimal discountPercentage)
    {
        decimal discount = price * discountPercentage / 100;
        Console.WriteLine($"Discounted Price: {price - discount}");
    }
}

public class ReflectionExample
{
    public static void Main()
    {
        // 1. Робота з Type та TypeInfo
        Type productType = typeof(Product);
        TypeInfo typeInfo = productType.GetTypeInfo();

        Console.WriteLine("Type and TypeInfo:");
        Console.WriteLine($"Type Name: {typeInfo.Name}");
        Console.WriteLine($"Namespace: {typeInfo.Namespace}");
        Console.WriteLine($"Is Class: {typeInfo.IsClass}\n");

        // 2. Робота з MemberInfo
        Console.WriteLine("MemberInfo:");
        MemberInfo[] members = productType.GetMembers();
        foreach (var member in members)
        {
            Console.WriteLine($"{member.MemberType}: {member.Name}");
        }

        // 3. Робота з FieldInfo
        Console.WriteLine("\nFieldInfo:");
        FieldInfo[] fields = productType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            Console.WriteLine($"Field: {field.Name}, Type: {field.FieldType}, IsStatic: {field.IsStatic}");
        }

        // 4. Робота з MethodInfo і виклик методу через Reflection
        Console.WriteLine("\nMethodInfo and invoking method:");
        Product productInstance = new Product("Laptop", 10, 999.99m, true);

        MethodInfo calculateDiscountMethod = productType.GetMethod("CalculateDiscount", BindingFlags.NonPublic | BindingFlags.Instance);
        if (calculateDiscountMethod != null)
        {
            Console.WriteLine("Invoking CalculateDiscount method with reflection:");
            calculateDiscountMethod.Invoke(productInstance, new object[] { 10m });
        }
    }
}
