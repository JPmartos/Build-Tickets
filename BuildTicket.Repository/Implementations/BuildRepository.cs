using BuildTicket.DataAccess.EntityFramework;
using BuildTicket.Entity.Models;
using BuildTicket.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildTicket.Repository.Implementations
{
    public class BuildRepository : IBuildRepository
    {
        private readonly dbBuildTicketContext _context;

        public BuildRepository(dbBuildTicketContext context)
        {
            this._context = context;
        }

        public Ticket Add(Ticket model)
        {
            try
            {
                _context.Entry(model).State = model.Id > 0 ? EntityState.Modified : EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public Status UpdateStatus(Ticket model)
        {
            Status status = new Status() { Success = true };

            Ticket  ticket = _context.Ticket.FirstOrDefault(u => u.Id == model.Id);

            ticket.Status = model.Status;
            ticket.IsActive = model.IsActive;
            ticket.Date_Change = model.Date_Change;

            try
            {
                if (ticket.Id > 0)
                {
                    _context.Entry(ticket).State = EntityState.Modified;
                    _context.SaveChanges();

                    return status;
                }
            }
            catch (Exception ex)
            {
                status.Id = -1;
                status.Success = false;
                status.Message = "Ocorreu um erro";
            }

            return status;
        }

        public Status Delete(int id)
        {
            Status status = new Status() { Success = true };

            try
            {
                var ticket = _context.Ticket.Find(id);

                if (ticket == null)
                {
                    status.Id = 1;
                    status.Success = false;
                    status.Message = "Ticket não encontrado";

                    return status;
                }

                _context.Ticket.Remove(ticket);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                status.Id = -1;
                status.Success = false;
                status.Message = "Ocorreu um erro";
            }

            return status;
        }

        public List<Ticket> List()
        {
            return _context.Ticket.OrderByDescending(e => e.Date_Ocurrence).ToList();
        }
    }
}
