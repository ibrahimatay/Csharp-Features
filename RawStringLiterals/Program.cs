// See https://aka.ms/new-console-template for more information

// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#raw-string-literals

string Istanbul = """     
    Istanbul is a major city in Turkey that straddles Europe and Asia across the Bosphorus Strait. 
    Its Old City reflects cultural influences of the many empires that once ruled here. 
    In the Sultanahmet district, the open-air, Roman-era Hippodrome was for centuries the site of chariot races, and Egyptian obelisks also remain. 
    The iconic Byzantine Hagia Sophia features a soaring 6th-century dome and rare Christian mosaics.  
    """;

Console.WriteLine(Istanbul);

var Longitude = 41.0082;
var Latitude = 28.9784;

var location = $$"""
   You are at {{{Longitude}}, {{Latitude}}}
   """;
Console.WriteLine();
Console.WriteLine(location);