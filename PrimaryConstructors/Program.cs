/*
 
 Primary constructors
 https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/instance-constructors#primary-constructors
 https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-12.0/primary-constructors 
 */
var emp01 = new Employee01();
var emp02 = new Employee02(TimeSpan.Zero, TimeSpan.Zero);
var emp03 = new Employee03(TimeSpan.Zero, TimeSpan.Zero);
var emp04 = new Employee03();

class Employee01
{
    
}

class Employee02
{
    private TimeSpan _startTime;
    private TimeSpan _stopTime;
    public Employee02(TimeSpan startTime, TimeSpan stopTime)
    {
        _startTime = startTime;
        _stopTime = stopTime;
    }
}

class Employee03(TimeSpan startTime, TimeSpan stopTime)
{
    public Employee03(): this(new TimeSpan(0,0,0), new TimeSpan(0,0,0))
    {
        
    }
    
    public TimeSpan GetStartTime()
    {
        return startTime;
    }
}