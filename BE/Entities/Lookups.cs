using System.Collections.Generic;

namespace WebApi.Entities
{
	public class Lookups
	{
		public List<ReferrerListOption> Referrers { get; set; }
	}

	public class ListOption
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}

	public class ReferrerListOption : ListOption
	{
		public bool CanEnterManually { get; set; }
	}

}