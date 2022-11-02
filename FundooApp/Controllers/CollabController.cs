using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL icollabBL;


        public CollabController(ICollabBL icollabBL)
        {
            this.icollabBL = icollabBL;

        }
    }
}
