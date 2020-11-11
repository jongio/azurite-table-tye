using System;
using System.Threading.Tasks;
using Azure.Data.Tables;

namespace azurite_table_tye
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tableClient = new TableClient(
                new Uri("http://127.0.0.1:10002/devstoreaccount1"),
                "people",
                new TableSharedKeyCredential(
                    "devstoreaccount1",
                    "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==")
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
        }
    }
}