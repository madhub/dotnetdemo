// See https://aka.ms/new-console-template for more information
using EFLiteDemo;

Console.WriteLine("Hello, World!");

using var db = new RequestRecordContext ();
db.Records.RemoveRange (db.Records);
db.SaveChanges ();
db.Records.Add (new RequestRecord() { CreateDate = DateTime.UtcNow ,Data = "some payload"});
db.Records.Add(new RequestRecord() { CreateDate = DateTime.UtcNow.AddSeconds(10), Data = "some payload2" });
db.Records.Add(new RequestRecord() { CreateDate = DateTime.UtcNow.AddSeconds(20), Data = "some payload2" });
db.SaveChanges ();

var records = db.Records.Select( o => o).Where( o => o.Data.Contains("2")).OrderByDescending(r => r.CreateDate);
foreach (var record in records)
{
    Console.WriteLine($"{record.Id } , {record.CreateDate}, {record.Data}");
}

Console.WriteLine("");
