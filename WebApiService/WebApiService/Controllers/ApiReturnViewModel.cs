namespace WebApiService.Controllers
{
    public class ApiReturnViewModel<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}