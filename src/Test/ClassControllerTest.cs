namespace Test;

public class ClassControllerTest
{
    int statusCode = (int)HttpStatusCodeEnum.Success;

    [Fact]
    public async Task When_get_all_classes_expect_ok()
    {
        var factory = new WebApplicationFactory<Program>();

        var client = factory.CreateClient();

        var response =
            await client.GetFromJsonAsync<Result<List<ListClassViewModel>>>("/api/Class/Classes");

        response!.StatusCode.Should().Be(statusCode);
    }

    [Fact]
    public async Task When_create_class_expect_ok()
    {
        var factory = new WebApplicationFactory<Program>();

        var client = factory.CreateClient();

        var model = new CreateClassViewModel
        {
            ClassCode = "302",
        };

        var response =
            await client.PostAsJsonAsync("/api/Class/Create", model);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task When_delete_class_expect_ok()
    {
        var factory = new WebApplicationFactory<Program>();

        var client = factory.CreateClient();

        var response =
           await client.DeleteAsync(requestUri: "/api/Class/DeleteClass?classId=1");

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}