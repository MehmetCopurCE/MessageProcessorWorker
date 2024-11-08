using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MessageAPI.Data;
using MessageAPI.Models;
using MessageAPI.Models.DTOs;
using MessageAPI.Models.Enums;

namespace MessageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly MessageDbContext _context;

        public MessagesController(MessageDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(MessageRequestDto messageDto)
        {
            // Gelen DTO'dan yeni bir Message nesnesi oluşturuyoruz
            var message = new Message
            {
                Content = messageDto.Content,
                MessageType = messageDto.MessageType,
                CreatedAt = DateTime.UtcNow,
                Status = MsgStatus.Pending // Varsayılan olarak 'Pending' atanır
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessages", new { id = message.Id }, message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, MessageRequestDto messageDto)
        {
            var existingMessage = await _context.Messages.FindAsync(id);
            if (existingMessage == null)
            {
                return NotFound();
            }

            // Gelen verileri mevcut mesaja uyguluyoruz
            existingMessage.Content = messageDto.Content;
            existingMessage.MessageType = messageDto.MessageType;
            existingMessage.Status = MsgStatus.Pending;

            _context.Entry(existingMessage).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
