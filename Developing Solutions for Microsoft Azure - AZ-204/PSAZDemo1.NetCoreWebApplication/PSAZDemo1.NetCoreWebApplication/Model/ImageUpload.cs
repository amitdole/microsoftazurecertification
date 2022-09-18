namespace PSAZDemo1.NetCoreWebApplication.Model
{
    public class ImageUpload
    {
        public string Name { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
