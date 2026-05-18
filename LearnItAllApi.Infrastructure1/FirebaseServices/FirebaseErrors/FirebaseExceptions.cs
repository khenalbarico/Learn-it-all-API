namespace LearnItAllApi.Infrastructure1.FirebaseServices.FirebaseErrors;

public class FirebaseAuthException(string message) : Exception(message);
public class FirebaseRealtimeDbException(string message) : Exception(message);
public class FirebaseStorageException(string message) : Exception(message);
public class FirebaseFirestoreDbException(string message) : Exception(message);
