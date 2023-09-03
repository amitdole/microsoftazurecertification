using StackExchange.Redis;
using ConsoleApp1;
using Newtonsoft.Json;

var connectionstring = "cache400.redis.cache.windows.net:6380,password=xW4jj75jCRFplCTnwIUMYYxtwfM210M1rAzCaOGY7lQ=,ssl=True,abortConnect=False";

var redis = ConnectionMultiplexer.Connect(connectionstring);

//SetCacheData("top:3:courses", "AZ-104,AZ204,AZ-900");
//GetCacheData("top:3:courses");
//GetCacheData("top:4:courses");

SetProductCacheData("user1", 1, 100);
SetProductCacheData("user1", 2, 200);
SetProductCacheData("user1", 3, 300);

GetProductCacheData("user1");

Console.ReadLine();


void SetCacheKeyExpiration(string key, TimeSpan expiry)
{
    var database = redis.GetDatabase();

    if (database.KeyExists(key))
    {
        database.KeyExpire(key, expiry);    
    }
    Console.WriteLine("Redis key expiry set");
}


void DeleteCacheKey(string key)
{
    var database = redis.GetDatabase();

    if (database.KeyExists(key))
    {
        database.KeyDelete(key);
    }
     Console.WriteLine("Redis key deleted");
}

void SetProductCacheData(string user, int ProductId, int Quantity)
{
    var database = redis.GetDatabase();

    var cartItem = new CartItem
    {
        ProductId = ProductId,
        Quantity = Quantity
    };

    var key = $"{user}:cartitems";
    database.ListRightPush(key, JsonConvert.SerializeObject(cartItem));

    Console.WriteLine("Redis set");
}

void GetProductCacheData(string user)
{
    var database = redis.GetDatabase();
    var key = $"{user}:cartitems";

    if (database.KeyExists(key))
    {
        while (database.KeyExists(key))
        {
            var data = database.ListRightPop(key);
            var cartItem = JsonConvert.DeserializeObject<CartItem>(data);

            Console.WriteLine($"User - {key}, ordered product - {cartItem.ProductId} and quantity - {cartItem.Quantity}");
        }
    }
    else
    {
        Console.WriteLine("Key does not exists");
    }
}


void SetCacheData(string key, string data)
{
    var database = redis.GetDatabase();

    database.StringSet(key, data);

    Console.WriteLine("Redis set");
}

void GetCacheData(string key)
{
    var database = redis.GetDatabase();

    if (database.KeyExists(key))
    {
        Console.WriteLine(database.StringGet(key));
    }
    else
    {
        Console.WriteLine("Key does not exists");
    }
}