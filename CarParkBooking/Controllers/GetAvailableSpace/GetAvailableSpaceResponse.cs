namespace CarParkBooking.API.Controllers.GetAvailableSpace
{
    public sealed record GetAvailableSpaceResponse(GetAvailableSpaceResponse.SpaceResponse[] Spaces)
    {
        public sealed record SpaceResponse(int Id);
    };
}