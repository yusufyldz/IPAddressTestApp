using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;

namespace IPAddressTestApp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IpaddressController : ControllerBase
    {
        

        public IpaddressController()
        {
            
        }

        [HttpGet(Name = "GetIpAddress")]
        public  List<string> GetIpAddress()
        {
            // X-Forwarded-For baþlýðýný kontrol et
            var xForwardedFor = Request.Headers["X-Forwarded-For"].FirstOrDefault();

            var dnshostaddres = Dns.GetHostAddresses(Dns.GetHostName()).ToString();

            var XForwardedProto = Request.Headers["X-Forwarded-Proto"].FirstOrDefault();

            var XForwardedHost = Request.Headers["X-Forwarded-Host"].FirstOrDefault();
            // Forwarded baþlýðýný kontrol et
            var forwarded = Request.Headers["Forwarded"].FirstOrDefault();

            // X-Real-IP baþlýðýný kontrol et
            var xRealIp = Request.Headers["X-Real-IP"].FirstOrDefault();
            var ConnectServerAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

             var ipAddressDifrent = HttpContext.GetServerVariable("HTTP_X_FORWARDED_FOR");

            return new List<string>()
            {
                xForwardedFor, forwarded, xRealIp,ConnectServerAddress,ipAddressDifrent, XForwardedProto, XForwardedHost , dnshostaddres
            };
        }

        [HttpGet(Name = "GetLocal")]
        public List< string> GetLocalIPAddress()
        {
            var list  = new List<string>();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
               list.Add(ip.ToString());
            }
            return list;
        }
    }
}
