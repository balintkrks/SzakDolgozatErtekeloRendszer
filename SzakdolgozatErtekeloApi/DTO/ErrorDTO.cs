namespace SzakdolgozatErtekeloApi.DTO
{
    public class ErrorDTO : Exception
    {
        public int Id { get; set; }

        public string Message { get; set; }
    }
}
