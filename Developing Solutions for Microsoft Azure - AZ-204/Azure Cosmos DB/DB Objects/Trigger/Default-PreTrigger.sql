function trigger()
{
    var context = getContext();
    var request = context.getRequest();
    
    var item = request.getBody();
    
    if (!("name" in item))
    {
        item["name"] = "Test user";
    }
    
    request.setBody(item);  
}