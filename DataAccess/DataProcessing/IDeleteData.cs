﻿
namespace DataAccess
{
    internal interface IDeleteData
    {
        IDataAccess DataAccess { get; set; }

        Task DeleteDesk(int deskID);
        Task DeleteLocation(int locationID);
        Task DeleteReservation(int reservationID);
    }
}