using System.Text.Json.Serialization;

using MassTransit.Mediator;

using Microsoft.AspNetCore.Mvc;

namespace TestMassTransit.Controllers
{
	[ApiController]
	[Route("Test")]
	public class TestController : ControllerBase
	{		
		private readonly ILogger<TestController> _logger;

		private IMediator? _mediator;
		protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

		public TestController(ILogger<TestController> logger)
		{
			_logger = logger;
		}


		[HttpPost]
		[Route("testMassTransit")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> TestMassTransit(BookAppointmentCommand payload)
		{
			var client = Mediator.CreateRequestClient<BookAppointmentCommand>();
			var response = await client.GetResponse<TextResponse>(payload);

			return Ok(response.Message);
		}

	}

	public class TextResponse
	{
		public TextResponse(string message)
		{
			Message = message;
		}

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}