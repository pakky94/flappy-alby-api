namespace FlappyAlby.API.Domain;

public record Player(string Name, TimeSpan Total, int? Id = default) : EntityBase(Id);