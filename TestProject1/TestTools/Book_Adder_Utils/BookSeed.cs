namespace TestProject1.TestTools.Book_Adder_Utils;

record BookSeed(
    string  Prefix,
    string  Category,
    string  Title,
    string  Description,
    double  Price,
    string  CoverFileName = "Cover_image.png",
    bool    Skip          = false);
