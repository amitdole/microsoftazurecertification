//using Azure.Identity;
//using Azure.Storage.Blobs;

//string ClientId = "b07b45a9-1425-48bb-8b00-a172c00829f2";
//string TenantId = "aabcd75f-ebd5-4531-8652-f02e88eb40ce";
//string ClientSecret = "Cv28Q~PwD0n7BBREXodxuuuBgv5hTMp5w-H0oa7C";

//string blobUrl = "https://azure400storagegroup.blob.core.windows.net/data/DML.sql";
//string filePath = "C:\\Dev\\Temp\\DML.sql";

//ClientSecretCredential credential = new ClientSecretCredential(TenantId, ClientId, ClientSecret);

//BlobClient blobClient = new BlobClient(new Uri(blobUrl), credential);

//await blobClient.DownloadToAsync(filePath);

//Console.WriteLine("Download Sucessfull !");
//Console.ReadLine();
