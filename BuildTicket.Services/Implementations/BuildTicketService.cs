using BuildTicket.Entity.Models;
using BuildTicket.Repository.Contracts;
using BuildTicket.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildTicket.Services.Implementations
{
    public class BuildTicketService : IBuildTicketService
    {
        private readonly IBuildRepository _IbuildRepository;

        public BuildTicketService(IBuildRepository ibuildRepository)
        {
            this._IbuildRepository = ibuildRepository;
        }

        public Ticket Add(Ticket model)
        {
            return _IbuildRepository.Add(model);
        }

        public Status UpdateStatus(Ticket model)
        {
            return _IbuildRepository.UpdateStatus(model);
        }

        public Status Delete(int id)
        {
            return _IbuildRepository.Delete(id);
        }

        public List<Ticket> List()
        {
            return _IbuildRepository.List();
        }
    }
}
