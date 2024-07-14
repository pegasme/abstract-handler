using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xm.TestTask.Messages;

namespace Xm.TestTask.Tests;

public class WebhookControllerTests
{
    [Test]
    public async Task WebhookPostAsync_ShouldReturnBadRequest_IfDataTypeIsNotExists()
    {
        // arrange
        using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        var content = new StringContent("Test", Encoding.UTF8,"application/x-www-form-urlencoded");

        // act
        var result = await client.PostAsync("/Webhook", content);

        // assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    [TestCase("")]
    [TestCase("    ")]
    public async Task WebhookPostAsync_ShouldReturnBadRequest_IfDataTypeIsNullOrEmpty(string value)
    {
        // arrange
        using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Add("x-xdt", value);

        var content = new StringContent("Test", Encoding.UTF8, "application/x-www-form-urlencoded");

        // act
        var result = await client.PostAsync("/Webhook", content);

        // assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task WebhookPostAsync_ShouldReturnInternalError_IfDataIsIncorrect()
    {
        // assert
        using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Add("x-xdt", "notification");

        var content = new StringContent("Test", Encoding.UTF8, "application/octet-stream");

        // act
        var result = await client.PostAsync("/Webhook", content);

        // assert
        result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Test]
    public async Task WebhookPostAsync_ShouldReturnOk_IfDataIsCorrect()
    {
        // assert
        using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Add("x-xdt", "notification");
        var notification = new NotificationMessage() { Message = "Test" };
        var notificationJson = JsonConvert.SerializeObject(notification);

        var content = new StringContent(notificationJson, Encoding.UTF8, "application/octet-stream");

        // act
        var result = await client.PostAsync("/Webhook", content);

        // assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}