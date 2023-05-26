namespace task.Models
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }

        public Person data { get; set; }
    
    public Response()
    {
        success=false;
    }
    }
}