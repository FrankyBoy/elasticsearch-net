using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class FormatPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line13()
		{
			// tag::7f465b7e8ed42df6c42251b4481e699e[]
			var response0 = new SearchResponse<object>();
			// end::7f465b7e8ed42df6c42251b4481e699e[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""date"": {
			        ""type"":   ""date"",
			        ""format"": ""yyyy-MM-dd""
			      }
			    }
			  }
			}");
		}
	}
}