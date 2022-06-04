using System.Net;

namespace APBD_EF_Example;

public class StatusResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public Object Content { get; set; }
}