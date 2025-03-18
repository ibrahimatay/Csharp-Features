
// Chunk.cs
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Linq/src/System/Linq/Chunk.cs

// how do I chunk an enumerable?
// https://stackoverflow.com/questions/12389203/how-do-i-chunk-an-enumerable

var movies = new List<Movie>
{
    new Movie("Titanic", 1998, 4.5f),
    new Movie("The Fifth Element", 1997, 4.6f),
    new Movie("Terminator 2", 1991, 4.7f),
    new Movie("Avatar", 2009, 5),
    new Movie("Platoon", 1986, 4),
    new Movie("My Neighbor Totoro", 1988, 5)
};

Console.WriteLine("---------Chunk-----------");

// Chunk
foreach (var movie in movies.Where(movie => movie.Rating > 4.5f).Chunk(3))
{
    Console.WriteLine(string.Join(",", movies.Select(currentMovie => currentMovie.Name)));
}

Console.WriteLine("---------DistinctBy-----------");

// DistinctBy
foreach (var movie in movies.DistinctBy(movie => movie.Rating))
{
    Console.WriteLine(string.Join(",", movies.Select(currentMovie => currentMovie.Name)));
}

Console.WriteLine("---------Take-----------");

// Take with Ranges
foreach (var movie in movies.Take(^5..3))
{
    Console.WriteLine(string.Join(",", movies.Select(currentMovie => currentMovie.Name)));
}

record Movie(string Name, int Year, float Rating);