using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class RequestBodyPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line9()
		{
			// tag::0ce3606f1dba490eef83c4317b315b62[]
			var response0 = new SearchResponse<object>();
			// end::0ce3606f1dba490eef83c4317b315b62[]

			response0.MatchesExample(@"GET /twitter/_search
			{
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line147()
		{
			// tag::bfcd65ab85d684d36a8550080032958d[]
			var response0 = new SearchResponse<object>();
			// end::bfcd65ab85d684d36a8550080032958d[]

			response0.MatchesExample(@"GET /_search?q=message:number&size=0&terminate_after=1");
		}
	}
}