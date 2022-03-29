namespace FlappyAlby.API.DTOs;

public record PlayerDto (string Name, TimeSpan Total, int? Id = default);