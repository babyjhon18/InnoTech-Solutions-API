using InnoTech_Solutions.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InnoTech_Solutions.Controllers
{
    [ApiController]
    [Route("InnoTech")]
    public class DeviceController : CustomController
    {
        public DeviceController(IInnoTechDataRepository repository) :
            base(repository)
        {
        }

        [HttpPost]
        [Route("DeviceParameters")]
        public Object Parameters([FromBody] Object body)
        {
            return Status(InnoTechRepository.Counter.View(new CounterParamsView(body.ToString()),
                    ControllerContext.HttpContext.Request.Query, CurrentUser));
        }

        [HttpPost]
        [Route("DeviceData")]
        public Object Data([FromBody] Object body)
        {
            return Status(InnoTechRepository.Counter.Data(new DataParamsView(body.ToString()),
                CurrentUser));
        }
    }
}
