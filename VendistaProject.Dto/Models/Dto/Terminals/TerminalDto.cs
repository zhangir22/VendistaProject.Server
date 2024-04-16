using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendistaProject.Dto.Models.Dto.Terminals
{
    public class TerminalDto
    {
        public int page_number { get; set; }
        public int items_per_page { get; set; }
        public int itmes_count { get; set; }
        public BodyTerminalDto[] items { get; set; }
    }
    public class CommandTerminalDto
    {
        public int terminal_id { get; set; }
        public int command_id { get; set; }
        public int parameter1 { get; set; }
        public int parameter2 { get; set; }
        public int parameter3 { get; set; }
        public int parameter4 { get; set; }
        public string str_parameter1 { get; set; }
        public string str_parameter2 { get; set; }
        public int state { get; set; }
        public string state_name { get; set; }
        public DateTime time_created { get; set; }
        public DateTime time_delevered { get; set; }
        public int id { get; set; }
    }
    public class MultyCommandDto
    {
        public int command_id { get; set; }
        public int? parameter1 { get; set; } = 0;
        public int? parameter2 { get; set; } = 0;
        public int? parameter3 { get; set; } = 0;
        public int? parameter4 { get; set; } = 0;
        public string str_parameter1 { get; set; } = "string";
        public string str_parameter2 { get; set; } = "string";
    }
    public class BodyTerminalDto
    {
        public string bank_id { get; set; }

        public string serial_number { get; set; }

        public int? version { get; set; }

        public string gsm_operator { get; set; }

        public int? gsm_rssi { get; set; }

        public Int64 imei { get; set; }

        public int? partner_id { get; set; }

        public string partner_name { get; set; }

        public int? external_server_id { get; set; }

        public int? last_online_time { get; set; }

        public int? last24_hours_online { get; set; }

        public int? last_hour_online { get; set; }

        public int? last24_hours_packets_count { get; set; }

        public string sber_qrid { get; set; }

        public int? auto_cancel_timeout { get; set; }

        public float? bonus_percent { get; set; }

        public bool send_cash { get; set; }

        public bool send_cashless { get; set; }

        public int? type { get; set; }

        public int? sim_balance { get; set; }

        public string sim_number { get; set; }

        public int? division_id { get; set; }

        public int? bootloader_version { get; set; }

        public string comment { get; set; }

        public int? owner_id { get; set; }

        public double longitude { get; set; }

        public double latitude { get; set; }

        public string owner_name { get; set; }

        public string color { get; set; }

        public int? sum { get; set; }

        public int? id { get; set; }

    }
}
