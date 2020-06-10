using BuildTicket.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildTicket.Domain.Contracts
{
     public interface ITicketDomain
    {
        Ticket Add(Ticket model);
        Status UpdateStatus(Ticket model);
        Status Delete(int id);
        List<Ticket> List();


    }
}
