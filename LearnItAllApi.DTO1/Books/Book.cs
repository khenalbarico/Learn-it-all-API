using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace LearnItAllApi.DTO1.Books;

[FirestoreData]
public class Book
{
    [Required]
    [FirestoreProperty] public string  Uid         { get; set; } = "";
    [FirestoreProperty] public string  Category    { get; set; } = "";
    [FirestoreProperty] public string  Title       { get; set; } = "";
    [FirestoreProperty] public string  Description { get; set; } = "";
    [FirestoreProperty] public double  Price       { get; set; }
}
