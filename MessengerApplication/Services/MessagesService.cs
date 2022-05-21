using MessengerApplication.Dtos;
using MessengerApplication.Models;
using MongoDB.Driver;

namespace MessengerApplication.Services;

public class MessagesService
{
    private readonly IMongoCollection<Message> _messages;
    private readonly IMongoDatabase _mongoDatabase;
    
    public MessagesService(DatabaseProviderService databaseProvider)
    {
        _mongoDatabase = databaseProvider.GetAccess();
        _messages = _mongoDatabase.GetCollection<Message>("Messages");
    }

    public async Task CreateMessageAsync(CreateMessageDto message)
    {
        var newMessage = new Message
        {
            ChatId = message.ChatId,
            Payload = message.Payload,
            Sender = message.Sender,
            Date = DateTime.Now
        };

        await _messages.InsertOneAsync(newMessage);
    }

    public async Task<List<Message>> GetMessagesAsync(string chatId) 
        => await _messages.Find(x => x.ChatId.Equals(chatId))
            .SortBy(x => x.Date)
            .ToListAsync();
}