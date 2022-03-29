namespace FlappyAlby.API.Domain;

public record Player(string Name, long TotalMilliseconds, int? Id = default) : EntityBase(Id);