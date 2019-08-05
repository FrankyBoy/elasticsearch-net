using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class ExtendedstatsAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::b1c3e5c4a1a22ac329bbdec4d0de1082[]
			var response0 = new SearchResponse<object>();
			// end::b1c3e5c4a1a22ac329bbdec4d0de1082[]

			response0.MatchesExample(@"GET /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""grades_stats"" : { ""extended_stats"" : { ""field"" : ""grade"" } }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line59()
		{
			// tag::eb8df98231df40c61f5feef4946b1a92[]
			var response0 = new SearchResponse<object>();
			// end::eb8df98231df40c61f5feef4946b1a92[]

			response0.MatchesExample(@"GET /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""extended_stats"" : {
			                ""field"" : ""grade"",
			                ""sigma"" : 3 \<1>
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line93()
		{
			// tag::83476d04b393850da0697e1bfae58b4a[]
			var response0 = new SearchResponse<object>();
			// end::83476d04b393850da0697e1bfae58b4a[]

			response0.MatchesExample(@"GET /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""extended_stats"" : {
			                ""script"" : {
			                    ""source"" : ""doc['grade'].value"",
			                    ""lang"" : ""painless""
			                 }
			             }
			         }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line115()
		{
			// tag::2cf036d054901b5d7b4a84780c320f2d[]
			var response0 = new SearchResponse<object>();
			// end::2cf036d054901b5d7b4a84780c320f2d[]

			response0.MatchesExample(@"GET /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""extended_stats"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"": {
			                        ""field"": ""grade""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line141()
		{
			// tag::533b447e1ca8c575e38ecd9b1917c17c[]
			var response0 = new SearchResponse<object>();
			// end::533b447e1ca8c575e38ecd9b1917c17c[]

			response0.MatchesExample(@"GET /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""extended_stats"" : {
			                ""field"" : ""grade"",
			                ""script"" : {
			                    ""lang"" : ""painless"",
			                    ""source"": ""_value * params.correction"",
			                    ""params"" : {
			                        ""correction"" : 1.2
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line171()
		{
			// tag::44a7cf8482bdc3d1c11f4b3b35683b99[]
			var response0 = new SearchResponse<object>();
			// end::44a7cf8482bdc3d1c11f4b3b35683b99[]

			response0.MatchesExample(@"GET /exams/_search
			{
			    ""size"": 0,
			    ""aggs"" : {
			        ""grades_stats"" : {
			            ""extended_stats"" : {
			                ""field"" : ""grade"",
			                ""missing"": 0 \<1>
			            }
			        }
			    }
			}");
		}
	}
}