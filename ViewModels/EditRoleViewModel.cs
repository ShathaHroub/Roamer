using System.ComponentModel.DataAnnotations;

namespace Roamer.ViewModels
{
	public class EditRoleViewModel
	{
        public EditRoleViewModel()
        {
			Users = new List<string>();
        }
        public string? RoleId { get; set; }

		[Required(ErrorMessage ="Insert Role Name")]
		[MinLength(3,ErrorMessage ="Minimum 3 characters")]
		[MaxLength(20, ErrorMessage ="Maximum 20 characters")]
		public string? RoleName { get; set; }

		public List<string> Users { get; set; }
	}
}
