﻿namespace HRAAB_Management.Business.Entities
{
    public class Hotel : IEntity
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<RoomType> RoomTypes { get; set; } = [];

        public List<Room> Rooms { get; set; } = [];

    }
}
