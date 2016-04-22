using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;
namespace My.Server.Data.Model
{
    public class ExcuteProcModelData
    {
        [Alias("message_id")]
        public int MessageId { get; set; }
    }
}
