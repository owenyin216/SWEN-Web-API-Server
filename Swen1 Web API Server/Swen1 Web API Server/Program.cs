using Semester_Projekt.Data_Access;
using Semester_Projekt.Models;
using Swen1_Web_API_Server.Service;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add services to the container.
var userRepository = new UserRepository(connectionString);
var userService = new UserService(userRepository);
var newUser = new User
{
    Username = "TestUser",
    Birthdate = new DateOnly(2010, 1, 1)
};

try
{
    userService.RegisterUser(newUser);
}
catch (Exception ex)
{
    Console.WriteLine($"Fehler bei Registrierung: {ex.Message}");
}


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


/*
Console.WriteLine("Hello, World!");


#region TestUserRepository
var userRepository = new UserRepository(connectionString);
Console.WriteLine("--- Test: AddUser ---");
var testUser = new User
{
    Username = "MaxMustermann",
    Password = "SecurePassword123",
    Roles_Id = 1,
    Created_At = DateTime.Now,
    Email = "max@mustermann.de",
    Email_Certified = true,
    Birthdate = new DateOnly(1990, 5, 15),
    Ageverified = true
};

userRepository.AddUser(testUser);
Console.WriteLine("User erfolgreich hinzugefügt.");


Console.WriteLine("\n--- Test: GetUserByUsername ---");
var fetchedUser = userRepository.GetUserByUsername("MaxMustermann");
Console.WriteLine($"Gefunden: ID {fetchedUser.User_id}, Username: {fetchedUser.Username}, Email: {fetchedUser.Email}");


Console.WriteLine("\n--- Test: GetUserByID ---");
var userById = userRepository.GetUserByID(fetchedUser.User_id);
Console.WriteLine($"Gefunden: ID {userById.User_id}, Username: {userById.Username}");


Console.WriteLine("\n--- Test: GetAllUsers ---");
var allUsers = userRepository.GetAllUsers();
foreach (var u in allUsers)
{
    Console.WriteLine($"- ID: {u.User_id} | Name: {u.Username} | Datum: {u.Created_At}");
}


Console.WriteLine("\n--- Test: UpdateUser ---");
fetchedUser.Email = "max.neu@mustermann.de";
try
{
    userRepository.UpdateUser(fetchedUser);
    Console.WriteLine("User erfolgreich aktualisiert.");
}
catch (Exception ex)
{
    // Dies wird fehlschlagen, da in deiner UpdateUser-Methode aktuell nur der Parameter "user_id" 
    // an den NpgsqlCommand übergeben wird, aber das SQL-Statement weitere Parameter (@username, @password, etc.) erwartet.
    Console.WriteLine($"Fehler beim Update: {ex.Message}");
}


Console.WriteLine("\n--- Test: DeleteUserByID ---");
userRepository.DeleteUserByID(fetchedUser.User_id);
Console.WriteLine("User erfolgreich gelöscht.");


// Kontrolle, ob der User wirklich gelöscht wurde
Console.WriteLine("\n--- Test: GetAllUsers nach Delete ---");
var usersAfterDelete = userRepository.GetAllUsers();
Console.WriteLine($"Anzahl der User in der Datenbank: {usersAfterDelete.Count}");

#endregion


#region TestPostsRepository
var postRepository = new PostRepositories(connectionString);

Console.WriteLine("--- Test: UploadPost ---");
var testPost = new Post
{
    Title = "Mein erster Post",
    Description = "Eine kurze Beschreibung",
    Body = "Das ist der vollständige Text des Posts.",
    Poster_Id = 1, // Setzt voraus, dass ein User mit der ID 1 in der Datenbank existiert
    Created_at = DateTime.Now
};
postRepository.UploadPost(testPost);
Console.WriteLine("Post erfolgreich hochgeladen.");


Console.WriteLine("\n--- Test: GetAllPosts ---");
var allPosts = postRepository.GetAllPosts();
foreach (var p in allPosts)
{
    Console.WriteLine($"- ID: {p.Post_id} | Titel: {p.Title} | Poster-ID: {p.Poster_Id}");
}


// Wir nehmen den zuletzt hinzugefügten Post aus der Liste für die nächsten Tests
var fetchedPost = allPosts[allPosts.Count - 1];


Console.WriteLine("\n--- Test: GetPost ---");
var singlePost = postRepository.GetPost(fetchedPost.Post_id);
Console.WriteLine($"Gefunden: ID {singlePost.Post_id}, Titel: {singlePost.Title}, Body: {singlePost.Body}");


Console.WriteLine("\n--- Test: EditPost ---");
singlePost.Title = "Aktualisierter Titel";
singlePost.Description = "Aktualisierte Beschreibung";
try
{
    postRepository.EditPost(singlePost);
    Console.WriteLine("Post erfolgreich bearbeitet.");
}
catch (Exception ex)
{
    Console.WriteLine($"Fehler beim Bearbeiten: {ex.Message}");
}


Console.WriteLine("\n--- Test: DeletePost ---");
// Die Methode DeletePost in der Klasse erwartet aktuell einen 'string' als Parameter,
// daher wird die int-ID mit .ToString() umgewandelt.
postRepository.DeletePost(singlePost.Post_id);
Console.WriteLine("Post erfolgreich gelöscht.");


Console.WriteLine("\n--- Test: GetAllPosts nach Delete ---");
var postsAfterDelete = postRepository.GetAllPosts();
Console.WriteLine($"Anzahl der Posts in der Datenbank: {postsAfterDelete.Count}");
#endregion

#region TestRatingsRepositoty
var ratingRepository = new RatingRepository(connectionString);

Console.WriteLine("--- Test: AddRating ---");
var testRating = new Ratings
{
    Rating_id = 1, // Nur setzen, falls die DB kein Auto-Increment (SERIAL) nutzt
    Rating_comment = "Sehr guter Post!",
    User_Id = 1,   // Setzt voraus, dass User 1 existiert
    Post_Id = 1,   // Setzt voraus, dass Post 1 existiert
    Rating = 5
};
try
{
    ratingRepository.AddRating(testRating);
    Console.WriteLine("Bewertung erfolgreich hinzugefügt.");
}
catch (Exception ex)
{
    Console.WriteLine($"Fehler beim Hinzufügen: {ex.Message}");
}

Console.WriteLine("\n--- Test: GetRatingsByPost ---");
var ratingsForPost = ratingRepository.GetRatingsByPost(1);
foreach (var r in ratingsForPost)
{
    Console.WriteLine($"- Rating ID: {r.Rating_id} | Sterne: {r.Rating} | Kommentar: {r.Rating_comment}");
}

Console.WriteLine("\n--- Test: GetAverageScoreForPost ---");
double avgScore = ratingRepository.GetAverageScoreForPost(1);
Console.WriteLine($"Durchschnittsbewertung für Post 1: {avgScore}");

Console.WriteLine("\n--- Test: UpdateRating ---");
if (ratingsForPost.Count > 0)
{
    var ratingToUpdate = ratingsForPost[0];
    ratingToUpdate.Rating = 4;
    ratingToUpdate.Rating_comment = "War doch nur okay.";

    ratingRepository.UpdateRating(ratingToUpdate);
    Console.WriteLine("Bewertung erfolgreich aktualisiert.");
}

Console.WriteLine("\n--- Test: GetRatingByID ---");
if (ratingsForPost.Count > 0)
{
    var singleRating = ratingRepository.GetRatingByID(ratingsForPost[0].Rating_id);
    if (singleRating != null)
    {
        Console.WriteLine($"Gefunden: ID {singleRating.Rating_id}, neue Sterne: {singleRating.Rating}");
    }
}

Console.WriteLine("\n--- Test: DeleteRatingByID ---");
if (ratingsForPost.Count > 0)
{
    ratingRepository.DeleteRatingByID(ratingsForPost[0].Rating_id);
    Console.WriteLine("Bewertung erfolgreich gelöscht.");
}
#endregion
*/

app.Run();
