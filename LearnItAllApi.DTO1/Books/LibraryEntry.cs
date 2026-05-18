using Google.Cloud.Firestore;

namespace LearnItAllApi.DTO1.Books;

[FirestoreData]
public class LibraryEntry
{
    [FirestoreProperty] public string  BookUid         { get; set; } = "";
    [FirestoreProperty] public string  OrderId         { get; set; } = "";
    [FirestoreProperty] public decimal PriceAtPurchase { get; set; }
    [FirestoreProperty] public string  PurchasedAt     { get; set; } = DateTime.UtcNow.ToString("o");
}
