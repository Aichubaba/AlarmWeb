using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlazorApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlarmsController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceiveAlarm([FromBody] AlarmDto alarm)
        {
            if (alarm == null)
                return BadRequest("Пустое тело запроса");

            // Здесь должна быть логика обработки аларма (сохранение в БД, уведомления и т.д.)
            Console.WriteLine($"Получен аварийный сигнал: {alarm.Message} от {alarm.Source}");

            return Ok(new { status = "принято" });
        }
    }

    public class AlarmDto
    {
        public string? Source { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}