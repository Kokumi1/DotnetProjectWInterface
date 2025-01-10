using System.Net.Http.Headers;
using BackEnd;
using BackEnd.Datas;
using BackEnd.Entities;
using BackEnd.Repositories;
using BackEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<PeriodicCsvGenerator>();
builder.Services.AddHostedService<PeriodicJsonGenerator>();


/*builder.Services.AddDbContext<BackEndDbContext>(options =>
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBBackEnd;");
            options.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    );*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


#region Debug field
/*List<Transaction> transactions = new List<Transaction>();
Transaction transaction = new Transaction()
{
    CardNumber = new CreditCard().getCreditCard(),
    type = Transaction.TransactionType.creditCardInvoice,
};
Transaction transaction2 = new Transaction()
{
    CardNumber = new CreditCard().getCreditCard(),
    type = Transaction.TransactionType.creditCardInvoice,
};
Transaction transaction3 = new Transaction()
{
    CardNumber = new CreditCard("4974 0185 0223 9999 1", false).getCreditCard(),
    type = Transaction.TransactionType.ATMWithdrawals,
};

transactions.Add(transaction);
transactions.Add(transaction2);
transactions.Add(transaction3);

List<Transaction>[] transactionTab = AnnomalyChecker.CheckAnnomaly(transactions);
Console.WriteLine("-------------  valid  ---------------");
foreach (Transaction tab in transactionTab[0])
{
    
    Console.WriteLine(tab.CardNumber);
    Console.WriteLine(tab.type.ToString());
}
Console.WriteLine("-------------  invalid  ---------------");
foreach (Transaction tab in transactionTab[1])
{
    Console.WriteLine(tab.CardNumber);
    Console.WriteLine(tab.type.ToString());
}*/
//Console.WriteLine(creditCard.LuhnCheck());
//Console.WriteLine(creditCard.getCreditCard());

/*
List<Transaction> transactions = new List<Transaction>()
{
    new Transaction
    {
                    Type = Transaction.TransactionType.creditCardInvoice,
                    CardNumber = new CreditCard().getCreditCard(),
                    Amount = 15.99,
                    Date = DateOnly.FromDateTime(System.DateTime.Now),
                    Devise = "EUR",
                    ExchangeRate = 1
    },
    new Transaction
    {
                    Type = Transaction.TransactionType.creditCardInvoice,
                    CardNumber = new CreditCard().getCreditCard(),
                    Amount = 11.99,
                    Date = DateOnly.FromDateTime(System.DateTime.Now),
                    Devise = "EUR",
                    ExchangeRate = 1
    }
};
DatabaseTalker.InsertTransactionListToDatabase(transactions);

List<Anomaly> anomalies = [
    new Anomaly
    {
                    Type = Anomaly.TransactionType.creditCardInvoice,
                    CardNumber = new CreditCard("4974 0185 0223 1111 6", false).getCreditCard(),
                    Amount = 11.99,
                    Date = DateOnly.FromDateTime(System.DateTime.Now),
                    Devise = "EUR",
                    ExchangeRate = 1
    }
];
DatabaseTalker.InsertAnomalyListToDatabase(anomalies);
*/

//JsonTalker.WriteJson(DatabaseTalker.SelectTransactionsFromDatabase());
/*var period = new PeriodicJsonGenerator();
Task.Run(async () =>
{
    await Task.Delay(6000);
    period.Stop();
});*/
//PeriodicCsvGenerator generator = new PeriodicCsvGenerator();
//Task? executeTask = generator.ExecuteTask;
//executeTask.Start();
//CreditCard creditCard = new CreditCard();
//CSVTalker.ReadCSV();
#endregion


app.Run();