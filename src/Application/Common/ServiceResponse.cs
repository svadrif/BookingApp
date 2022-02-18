namespace Application.Common;

public class ServiceResponse<T>
{
    public T Data { get; set; }
    public bool success { get; set; } = true;
    public string message { get; set; } = null;
}