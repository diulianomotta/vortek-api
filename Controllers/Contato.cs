using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;

[ApiController]
[Route("api/[controller]")]
public class ContatoController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> EnviarContato([FromBody] ContatoDto dados)
    {
        var mensagem = new MimeMessage();
        mensagem.From.Add(new MailboxAddress("Vortek", "diulianomotta@gmail.com"));
        mensagem.To.Add(new MailboxAddress("Você", "diulianomotta@gmail.com"));

        mensagem.Subject = "Novo contato do site";

        mensagem.Body = new TextPart("plain")
        {
            Text = $"Nome: {dados.Nome}\n" +
                   $"Email: {dados.Email}\n" +
                   $"Telefone: {dados.Telefone}\n" +
                   $"Mensagem: {dados.Mensagem}"
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("diulianomotta@gmail.com", "ybmnvpqbewqbyubv");
            await client.SendAsync(mensagem);
            await client.DisconnectAsync(true);
        }

        return Ok();
    }
}