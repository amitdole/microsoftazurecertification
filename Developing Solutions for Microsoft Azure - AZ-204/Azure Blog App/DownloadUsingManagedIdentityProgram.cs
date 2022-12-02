//using Azure.Identity;
//using Azure.Storage.Blobs;

//string blobUrl = "https://azure400storagegroup.blob.core.windows.net/data/DML.sql";
//string filePath = "C:\\Dev\\Temp\\DML.sql";

//var tokenCredential = new DefaultAzureCredential();

//BlobClient blobClient = new BlobClient(new Uri(blobUrl), tokenCredential);

//await blobClient.DownloadToAsync(filePath);

//Console.WriteLine("Download Sucessfull !");
//Console.ReadLine();
