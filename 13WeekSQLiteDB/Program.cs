using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = true; Compress=true");

try
{
    connection.Open()
    Console.WriteLine("DB found.");
}
catch
{
    Console.WriteLine("DB not found.");
}

return connection;
}
 
static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteDataReader command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT customer.firstName, customer.lastName, status.statustype" +
        "FROM customeerStatus" +
        "JOIN customer on customer.rowid = customerStatus.customerId" +
        "JOIN status on status.rowid = customer.statusId";

    reader = command.ExecuteReader();

        while (reader.Read())
        {
        string readerStringFirstName = reader.GetString(0);
        string readerStringLastName = reader.GetString(1);
        string readerStringStatus = reader.GetString(2);


        Console.WriteLine($"Full name: {readerStringFirstName} {readerStringLastName}; Satus: {readerStringStatus});

        }
        myConnection.Close();
}

//andmete lisamine
{ 
SQLiteCommand command;
string fName, lName, dob;

Console.WriteLine("Enter first name:");
fName = Console.ReadLine();
Console.WriteLine("Enter last name:");
lName = Console.ReadLine();
Console.WriteLine("Enter date of birth (mm-dd-yyy):");
dob = Console.ReadLine();

command = myConnection.CreateCommand();
command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " + $"VALUES ('{fName}', '{lName}', '{dob}'";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");
    ReadData(myConnection);
}

//andmete kustutamine
static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    Console.WriteLine("Enter an id to delete a customer:");
    string idToDelete;
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery(); 
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myConnection);
}