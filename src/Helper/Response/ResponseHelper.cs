namespace Helper.Response;

public static class ResponseHelper
{
    public static ObjectResult ApiResult(this Response response)
    {
        ObjectResult objectResult = new OkObjectResult(response);

        if (response.StatusCode == 200)
        {
            objectResult = new OkObjectResult(response);
        }

        switch (response.StatusCode)
        {
            case (int)HttpStatusCodeEnum.BadRequest:
                objectResult = new BadRequestObjectResult(response);

                break;

            case (int)HttpStatusCodeEnum.NotFound:
                objectResult = new NotFoundObjectResult(response);

                break;

            case (int)HttpStatusCodeEnum.ServerError:
                objectResult = new ObjectResult(response);

                break;
        }

        return objectResult;
    }
}
