using Server.Infrastructure.Attributes;

namespace Server.Infrastructure.Base;

[ApiVersion("1")]
[CustomExceptionHandlerAttribute]
[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
}
