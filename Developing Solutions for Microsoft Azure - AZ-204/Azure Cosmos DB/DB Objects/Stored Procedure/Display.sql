function Display()
{
    var context = getContext();
    var response = context.getResponse();
    
    response.setBody("Hi There");
}