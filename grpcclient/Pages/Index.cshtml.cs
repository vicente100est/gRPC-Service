using Grpc.Net.Client;
using GrpcService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpcclient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Greeter.GreeterClient _client;
        [BindProperty]
        public string Nombre { get; set; }
        public string Mensaje { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            var url = "https://localhost:5001";
            var channel = GrpcChannel.ForAddress(url);
            _client = new Greeter.GreeterClient(channel);
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task OnPost()
        {
            var helloRequest = new HelloRequest();
            helloRequest.Name = Nombre;

            var result = await _client.SayHelloAsync(helloRequest);

            Mensaje = result.Message;
        }
    }
}
