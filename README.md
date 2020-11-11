# Use the new Azurite Table Preview with Project Tye and the new Azure Tables Client Library

We just released the first alpha of [Azurite](https://github.com/Azure/Azurite/) with Table support and [Azure.Data.Tables 3.0.0-beta2](https://www.nuget.org/packages/Azure.Data.Tables).

This sample includes Project Tye, a .NET 5 console app, the new Azurite alpha with Table support referenced via image tag, and the new Azure Tables client library.

Here's the tye.yaml
```yaml
name: azurite-table-tye
services:
- name: azurite-table-tye
  project: azurite-table-tye.csproj
- name: azurite
  image: mcr.microsoft.com/azure-storage/azurite:alpha
  bindings:
    - name: blob
      port: 10000
      containerPort: 10000
      protocol: http
    - name: queue
      port: 10001
      containerPort: 10001
      protocol: http
    - name: table
      port: 10002
      containerPort: 10002
      protocol: http
```

Here's the app:
```csharp
var tableClient = new TableClient(
    new Uri("http://127.0.0.1:10002/devstoreaccount1"),
    "people",
    new TableSharedKeyCredential(
        "devstoreaccount1",
        "[see Program.cs for key]")
);

await tableClient.CreateIfNotExistsAsync();

while (true)
{
    var response = await tableClient.AddEntityAsync(
        new TableEntity("jong", Guid.NewGuid().ToString())
            {
                {"FirstName", "Jon"}, 
                {"LastName", "Gallant"}
            }
    );
    Console.WriteLine(response.Status);
    await Task.Delay(TimeSpan.FromSeconds(1));
}
```

Get it running:

1. Install [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
1. Install [Project Tye](https://aka.ms/tye)
1. Install [Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)
1. Open the project.
1. Open a terminal.
1. Run `tye run`
1. Open Storage Explorer
1. Connect to Azurite via the 'Attach to a local emulator' option.
1. Navigate to the people table and view the entities being inserted by the project.


## Azurite Table Preview Notes

GitHub Release: https://github.com/Azure/Azurite/releases/tag/v3.9.0-table-alpha.1

GitHub Table Branch: https://github.com/Azure/Azurite/tree/table

Npm:  https://www.npmjs.com/package/azurite/v/3.9.0-table-alpha.1
- `npm install -g azurite@alpha` // alpha tag always points to latest alpha version
- `npm install -g azurite@3.9.0-table-alpha.1`

MCR:
- `docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 mcr.microsoft.com/azure-storage/azurite:alpha` // alpha tag always points to latest alpha version
- `docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 mcr.microsoft.com/azure-storage/azurite:3.9.0-table-alpha.1`

