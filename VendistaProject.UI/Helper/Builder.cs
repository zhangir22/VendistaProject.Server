using VendistaProject.Dto.Models;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.UI;
using VendistaProject.UI.Services;

namespace VendistaProject.UI.Helper
{
    public class Builder
    {
        protected BaseApiService service { get; set; }
        public Builder(BaseApiService service) 
        {
            this.service = service;
        }
        public MultyCommand BuildMultyCommand(int idCommand, string? commandParameterName1, string? commandParameterName2, string? commandParameterName3)
        {
            return new MultyCommand
            {
                command_id = idCommand,
                parameter1 = commandParameterName1 != null ? int.Parse(commandParameterName1) : 0,
                parameter2 = commandParameterName2 != null ? int.Parse(commandParameterName2) : 0,
                parameter3 = commandParameterName3 != null ? int.Parse(commandParameterName3) : 0,
            }; 
        }
       
    }
}
