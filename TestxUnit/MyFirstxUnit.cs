using Task;

namespace TestxUnit;

// 1. Правило наименования: Класс обычно называется ИмяТестируемогоКлассаTests
public class CalculatorTests
{
    [Theory]
    [InlineData(new[]{ 1, 3, 5, 7, 9, 10 }, 5)]
    [InlineData(new[] { 2, 3, 5, 7, 9, 10 }, 0)]
    [InlineData(new[] { 1, 3, 5, 8, 9, 10 }, 3)]
    public void FindFirstEven_Correct(int[] a, int res)
    {
        Program.FindFirstEven(a);

        Assert.Equal(res, Program.FindFirstEven(a));
    }    
    
    // Простой одиночный тест (аналог обычного факта)
    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
    {
        // Arrange (Подготовка данных)
        var calculator = new Calculator();
        int a = 5;
        int b = 10;

        // Act (Выполнение действия)
        int result = calculator.Add(a, b);

        // Assert (Проверка результата)
        Assert.Equal(15, result);
    }

    // Параметризованный тест (Theory) со статическими данными
    [Theory]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(0, true)]
    public void IsEven_WhenCalled_ReturnsExpectedResult(int number, bool expected)
    {
        var calculator = new Calculator();

        bool result = calculator.IsEven(number);

        Assert.Equal(expected, result);
    }

    // Современный подход v3: Строго типизированные динамические данные
    public static TheoryData<int, int, int> DivisionTestData => new()
    {
        { 10, 2, 5 },
        { 9, 3, 3 },
        { 0, 5, 0 }
    };

    [Theory]
    [MemberData(nameof(DivisionTestData))]
    public void Divide_ValidInputs_ReturnsExpectedQuotient(int dividend, int divisor, int expected)
    {
        var calculator = new Calculator();

        int result = calculator.Divide(dividend, divisor);

        Assert.Equal(expected, result);
    }

    // Тестирование исключений (Exceptions)
    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        var calculator = new Calculator();

        // Проверяем, что метод выбросит именно это исключение
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }
}

// Упрощенный класс, который мы тестируем
public class Calculator
{
    public int Add(int a, int b) => a + b;
    public bool IsEven(int number) => number % 2 == 0;
    public int Divide(int dividend, int divisor) => dividend / divisor;
}
