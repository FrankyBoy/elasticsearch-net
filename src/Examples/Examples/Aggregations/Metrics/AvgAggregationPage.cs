using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class AvgAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line10()
		{
			// tag::d9d28e9e9d7021a72c983f8e79aa8c6c[]
			var response0 = new SearchResponse<object>();
			// end::d9d28e9e9d7021a72c983f8e79aa8c6c[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_grade"" : { ""avg"" : { ""field"" : ""grade"" } }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line43()
		{
			// tag::d05bbafb8c88850879b5990119a96f5e[]
			var response0 = new SearchResponse<object>();
			// end::d05bbafb8c88850879b5990119a96f5e[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_grade"" : {
			            ""avg"" : {
			                ""script"" : {
			                    ""source"" : ""doc.grade.value""
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line63()
		{
			// tag::c04f4a48d0cb550a879fdc93454852de[]
			var response0 = new SearchResponse<object>();
			// end::c04f4a48d0cb550a879fdc93454852de[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_grade"" : {
			            ""avg"" : {
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
		public void Line88()
		{
			// tag::91994d98e766230911b3e659b3e51f17[]
			var response0 = new SearchResponse<object>();
			// end::91994d98e766230911b3e659b3e51f17[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""avg_corrected_grade"" : {
			            ""avg"" : {
			                ""field"" : ""grade"",
			                ""script"" : {
			                    ""lang"": ""painless"",
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
		public void Line117()
		{
			// tag::2ec33e09d6080723ee2013bad694f35a[]
			var response0 = new SearchResponse<object>();
			// end::2ec33e09d6080723ee2013bad694f35a[]

			response0.MatchesExample(@"POST /exams/_search?size=0
			{
			    ""aggs"" : {
			        ""grade_avg"" : {
			            ""avg"" : {
			                ""field"" : ""grade"",
			                ""missing"": 10 \<1>
			            }
			        }
			    }
			}");
		}
	}
}