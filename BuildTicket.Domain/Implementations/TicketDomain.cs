using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildTicket.Domain.Contracts;
using BuildTicket.Entity.Models;
using BuildTicket.Services.Contracts;

namespace BuildTicket.Domain.Implementations
{
    public class TicketDomain : ITicketDomain
    {
        private readonly IBuildTicketService _IbuildTicket;

        //Injeção de dependência da Service 
        public TicketDomain(IBuildTicketService buildTicket)
        {
            this._IbuildTicket = buildTicket;
        }

        //Adicionar ou editar ticket
        public Ticket Add(Ticket model)
        {
            model.Date_Ocurrence = DateTime.Now;
            model.Date_Change = DateTime.Now;
            model.IsActive = false;

            model.Status = model.Status == Ticket.EnumStatus.Pendente ? model.Status = Ticket.EnumStatus.Pendente : model.Status;

            return _IbuildTicket.Add(model);
        }

        //Atualizar Status e IsActive 
        public Status UpdateStatus(Ticket model)
        {
            List<Ticket> listTicket = new List<Ticket>();
            Status status = new Status() { Success = true };
            
            //Alta
            int countPriorityHigh = 0;
            //Media
            int countPriorityAverage = 0;
            //Baixa
            int countPriorityLow = 0;

            listTicket = _IbuildTicket.List();

            foreach (var item in listTicket)
            {
                //Realizar contagem das prioridades com status "Pendente"
                if (item.Priority == Ticket.EnumPriority.Alta && item.Status == Ticket.EnumStatus.Pendente)
                {
                    countPriorityHigh++;
                }
                else if (item.Priority == Ticket.EnumPriority.Media && item.Status == Ticket.EnumStatus.Pendente)
                {
                    countPriorityAverage++;
                }
                else if (item.Priority == Ticket.EnumPriority.Baixa && item.Status == Ticket.EnumStatus.Pendente)
                {
                    countPriorityLow++;
                }
            }

            //Caso o usuário esteja resolvendo um ticket com prioridade "ALTA", Resolver normalmente
            if (model.Priority == Ticket.EnumPriority.Alta && model.Status == Ticket.EnumStatus.Pendente)
            {
                model.Status = Ticket.EnumStatus.Resolvido;
                model.Date_Change = DateTime.Now;
                model.IsActive = true;

                var result =  _IbuildTicket.UpdateStatus(model);

                if (result.Success)
                {
                    status.Message = "Ticket resolvido com sucesso";
                }
                else
                {
                    status.Message = result.Message;
                }
                
                return status;
            }

            ///Caso o usuário esteja resolvendo um ticket com prioridade "Media" ou "Baixa" e tiver algum ticket com prioridade "ALTA" e com o status "PEDENTE", Não deixar o usuário resolver antes que resolva todos os ticket com prioridade "ALTA".
            if ((model.Priority == Ticket.EnumPriority.Media || model.Priority == Ticket.EnumPriority.Baixa) && countPriorityHigh > 0)
            {
                status.Id = -1;
                status.Success = false;
                status.Message = "Resolva os tickets com prioridade ALTA primeiramente!";

                return status;

            }

            //Caso o usuário esteja resolvendo um ticket com prioridade "BAIXA" e não houver tickets com prioridade "Media", resolver ticket, caso ao contrario, Não deixar o usuário resolver antes que resolva todos os ticket com prioridade "MEDIA".
            if (model.Priority == Ticket.EnumPriority.Baixa && countPriorityAverage > 0)
            {
                status.Id = -1;
                status.Success = false;
                status.Message = "Resolva os ticket com prioridade MÈDIA primeiramente!";

                return status;
            }
            else //Usuario esta resolvendo um prioridade "BAIXA" e não tem nenhuma prioridade "ALTA" ou "MEDIA" com status "Pendente", Então resolver o ticket normalmente.
            {
                model.Status = Ticket.EnumStatus.Resolvido;
                model.Date_Change = DateTime.Now;
                model.IsActive = true;

                var result = _IbuildTicket.UpdateStatus(model);

                if (result.Success)
                {
                    status.Message = "Ticket resolvido com sucesso!";
                }
                else
                {
                    status.Message = result.Message;
                }

                return status;
            }
        }

        //Delete Ticket
        public Status Delete(int id)
        {
            return _IbuildTicket.Delete(id);
        }

        //Listar todos os tickets salvo no banco de dados. 
        public List<Ticket> List()
        {
            return _IbuildTicket.List();
        }
    }
}
