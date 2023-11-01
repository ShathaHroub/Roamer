using System.ComponentModel.DataAnnotations;

namespace Roamer.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		public string? RoleName { get; set; }

	}
}
