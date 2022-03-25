using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Seeds
{
    public static class WorkPlacesSeed
    {
        public static List<WorkPlace> GetWorkPlaces(List<Map> maps)
        {
            var workPlaces = new List<WorkPlace>();
            foreach (var map in maps)
            {
                workPlaces.AddRange(new[]
                {
                    new WorkPlace
                    {
                        Number = "A1",
                        Type = SeatsType.VIP,
                        IsNextToWindow = true,
                        HasPC = true,
                        HasMonitor = true,
                        HasKeyboard = true,
                        HasMouse = true,
                        HasHeadset = true,
                        IsBlocked = false,
                        MapId = map.Id
                    },
                    new WorkPlace
                    {
                        Number = "A2",
                        Type = SeatsType.VIP,
                        IsNextToWindow = false,
                        HasPC = true,
                        HasMonitor = true,
                        HasKeyboard = true,
                        HasMouse = true,
                        HasHeadset = true,
                        IsBlocked = false,
                        MapId = map.Id
                    },
                    new WorkPlace
                    {
                        Number = "A3",
                        Type = SeatsType.CommonSeat,
                        IsNextToWindow = false,
                        HasPC = true,
                        HasMonitor = true,
                        HasKeyboard = true,
                        HasMouse = false,
                        HasHeadset = false,
                        IsBlocked = false,
                        MapId = map.Id
                    },
                    new WorkPlace
                    {
                        Number = "B1",
                        Type = SeatsType.CommonSeat,
                        IsNextToWindow = false,
                        HasPC = false,
                        HasMonitor = false,
                        HasKeyboard = true,
                        HasMouse = true,
                        HasHeadset = true,
                        IsBlocked = false,
                        MapId = map.Id
                    },
                    new WorkPlace
                    {
                        Number = "B2",
                        Type = SeatsType.VIP,
                        IsNextToWindow = false,
                        HasPC = true,
                        HasMonitor = true,
                        HasKeyboard = false,
                        HasMouse = true,
                        HasHeadset = false,
                        IsBlocked = false,
                        MapId = map.Id
                    },
                    new WorkPlace
                    {
                        Number = "C1",
                        Type = SeatsType.VIP,
                        IsNextToWindow = true,
                        HasPC = true,
                        HasMonitor = true,
                        HasKeyboard = true,
                        HasMouse = true,
                        HasHeadset = true,
                        IsBlocked = false,
                        MapId = map.Id
                    },
                });
            }
            return workPlaces;
        }
    }
}
