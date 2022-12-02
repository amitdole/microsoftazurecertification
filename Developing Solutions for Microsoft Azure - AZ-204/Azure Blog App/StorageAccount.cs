//using Azure.Identity;
//using Azure.Storage.Blobs;
//using Azure.Storage.Blobs.Specialized;

//string connectionString = "DefaultEndpointsProtocol=https;AccountName=azure400storagegroup;AccountKey=0TD8ND+dOTOnw3r6R7E3pVyzWo2/np50zifGmD5Jw836p8x2aLRe/+colXOWMg84N+ri6TGFlvtL+AStkem9rQ==;EndpointSuffix=core.windows.net";
//string containerName = "scripts";
//string blobName = "DML.sql";
//string filePath = "C:\\Dev\\Temp\\DML.sql";

//////create container
////var blobServiceClient = new BlobServiceClient(connectionString);

////blobServiceClient.CreateBlobContainerAsync(containerName,Azure.Storage.Blobs.Models.PublicAccessType.Blob);
////Console.WriteLine($"Container created - {containerName}");

////Upload to blob
////var blobContainerClient = new BlobContainerClient(connectionString, containerName);
////var blobClient = blobContainerClient.GetBlobClient(blobName);

////await blobClient.UploadAsync(filePath, true);

////Console.WriteLine($"Blobtainer created - {blobName}");

//////list blobs
////var blobContainerClient = new BlobContainerClient(connectionString, containerName);

////await foreach(var item in  blobContainerClient.GetBlobsAsync())
////{
////    Console.WriteLine($"Blob Name- {item.Name}");
////    Console.WriteLine($"Blob Szie- {item.Properties.ContentLength}");
////}

//// Download bob

////var blobClient = new BlobClient(connectionString,containerName,blobName);
////await blobClient.DownloadToAsync(filePath);

////Console.WriteLine($"Blob Downloaded- {blobName}");

////Set/Get blob metadata

////await SetblobMetadata();
////await GetblobMetadata();

////Get Lease

//await AquireLease();

//async Task AquireLease()
//{
//    string blobName = "DML.sql";
//    var blobClient = new BlobClient(connectionString, containerName, blobName);

//    var blobLeaseClient = blobClient.GetBlobLeaseClient();
//    var leasetime = new TimeSpan(0, 0, 0, 30);

//    var response = await blobLeaseClient.AcquireAsync(leasetime);

//    Console.WriteLine($"Blob Lease Id- {response.Value.LeaseId}");
//}

//async Task SetblobMetadata()
//{
//    string blobName = "DML.sql";
//    var blobClient = new BlobClient(connectionString,containerName,blobName);
//    var metadata = new Dictionary<string, string>();
//    metadata.Add("Department", "Development");
//    metadata.Add("Application", "App A");

//    await blobClient.SetMetadataAsync(metadata);

//    Console.WriteLine($"metadata added");
//}

//async Task GetblobMetadata()
//{
//    string blobName = "DML.sql";
//    var blobClient = new BlobClient(connectionString, containerName, blobName);

//    var properties = await blobClient.GetPropertiesAsync();
//    foreach (var item in properties.Value.Metadata)
//    {
//        Console.WriteLine($"{item.Key} - {item.Value}");
//    }
//}

//Console.ReadLine();