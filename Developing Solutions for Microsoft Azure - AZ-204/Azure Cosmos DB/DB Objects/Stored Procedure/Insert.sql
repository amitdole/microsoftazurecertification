function CreateItems(items)
{
    var context = getContext();
    var response = context.getResponse();
    
    if (!items)
    {
        response.setBody("Error: Items are undefined");
        return;
    }
    
    var numberofItems = items.length
    
    response.setBody("Info: Total Items" + numberofItems);
     
    checkLength(numberofItems);
    
    for(let i=0; i<numberofItems; i++ )
    {
        createItem(items[i]);
    }
    
     function checkLength(itemLength)
     {
         if (itemLength == 0)
         {
            response.setBody("Error: No items");
            return;
         }
     }
     
     function createItem (item)
     {
         var collection = context.getCollection();
         var collectionLink = collection.getSelfLink();
         
         response.setBody("Creating Item");
          
         collection.createDocument(collectionLink,item);
         
          response.setBody("Item created");
     }
    
}