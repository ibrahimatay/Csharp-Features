
// User-defined explicit and implicit conversion operators
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators

// Type-testing operators and cast expressions - is, as, typeof and casts
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#cast-expression


Product product01 = new Product() { Id = 1, Name = "Same product", Price = 12};

ProductDto productDto01 = product01;
var productDto02 = (ProductDto)product01;

Console.WriteLine(productDto01);
Console.WriteLine(productDto02);


Celsius cel = new Celsius(45);

Fahrenheit far = cel;

float temperature = cel;

Console.WriteLine($"Celsius: {temperature}");

float Fahrenheit = far;

Console.WriteLine($"Fahrenheit: {Fahrenheit}");

Console.WriteLine(far);

Celsius cel2 = far;

Console.WriteLine(cel2);

class Temperature
{
    public float Degrees { get; set; }

    public static implicit operator float(Temperature temperature) => temperature.Degrees;
}

class Celsius : Temperature
{
    public Celsius(float degrees) => Degrees = degrees;

    public static implicit operator Fahrenheit(Celsius c) => new Fahrenheit((9.0f / 5.0f) * c.Degrees + 32);

    public override string ToString() => $"{Degrees} °C";
}

class Fahrenheit : Temperature
{
    public Fahrenheit(float degrees) => Degrees = degrees;
 
    public static implicit operator Celsius(Fahrenheit fahrenheit) => new Celsius((5.0f / 9.0f) * (fahrenheit.Degrees - 32));

    public override string ToString() => $"{Degrees} °F";
}

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public static implicit operator ProductDto(Product product) => new ProductDto
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
    };
}

class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Price { get; set; }

    public override string ToString() => $"Id: {Id} Name: {Name} Price: {Price}";
}