using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using System.Text;

string tenantId = "aabcd75f-ebd5-4531-8652-f02e88eb40ce";
string clientId = "36c2f1e3-51d3-45eb-a0a7-ddae3260bc07";
string clientSecret = "cNc8Q~q1_6o~KZZi6IZ4jcysDdZpc-jTxWjw-ccb";

var clientSecretCredentials = new ClientSecretCredential(tenantId, clientId, clientSecret);

var keyVaultUrl = "https://azure400vault.vault.azure.net/";
var keyName = "appkey";
var text = "This is the secret";

var client = new KeyClient(new Uri(keyVaultUrl), clientSecretCredentials);

var key = client.GetKey(keyName);

var cryptographicClient = new CryptographyClient(key.Value.Id, clientSecretCredentials);

var byteText = Encoding.UTF8.GetBytes(text);

var result = cryptographicClient.Encrypt(EncryptionAlgorithm.RsaOaep, byteText);

Console.WriteLine(Convert.ToBase64String(result.Ciphertext));

var cipherBytes = result.Ciphertext;

var decryptdata = cryptographicClient.Decrypt(EncryptionAlgorithm.RsaOaep, cipherBytes);

Console.WriteLine(Encoding.UTF8.GetString(decryptdata.Plaintext));

Console.ReadLine();
