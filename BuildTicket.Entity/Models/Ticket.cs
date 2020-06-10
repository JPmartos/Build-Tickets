using System;
using System.ComponentModel;

namespace BuildTicket.Entity.Models
{
    #region Ticket
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date_Ocurrence { get; set; }
        public string Email { get; set; }
        public EnumPriority Priority { get; set; }
        public EnumStatus Status { get; set; }
        public DateTime? Date_Change { get; set; }
        public bool IsActive { get; set; }//Saber se o botão está ativo ou não caso o status seja "Resolvido".


        public enum EnumPriority
        {
            [Description("Alta")]
            Alta = 1,
            [Description("Media")]
            Media = 2,
            [Description("Baixa")]
            Baixa = 3,
        }

        public enum EnumStatus
        {
            [Description("Pendente")]
            Pendente = 1,
            [Description("Resolvido")]
            Resolvido = 2,
        }
    }
    #endregion  


}
