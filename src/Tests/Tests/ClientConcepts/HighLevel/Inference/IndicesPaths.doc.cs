﻿using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Domain;
using Tests.Framework.DocumentationTests;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class IndicesPaths : DocumentationTestBase
	{
		/**[[indices-paths]]
		* === Indices paths
		*
		* Some APIs in Elasticsearch take an index name, a collection of index names,
		* or the special `_all` marker (used to specify all indices), in the URI path of the request, to specify the indices that
		* the request should execute against.
		*
		* In NEST, these index names can be specified using the `Indices` type.
		*
		* ==== Implicit Conversion
		*
		* To make working with `Indices` easier, several types implicitly convert to it:
		*
		* - `string`
		* - comma separated `string`
		* - `string` array
		* - a CLR type, <<index-name-inference, where a default index name or index name for the type has been specified on `ConnectionSettings`>>
		* - `IndexName`
		* - `IndexName` array
		*
		* Here are some examples of how implicit conversions can be used to specify index names
		*/
		[U] public void ImplicitConversions()
		{
			Nest.Indices singleIndexFromString = "name";
			Nest.Indices multipleIndicesFromString = "name1, name2";
			Nest.Indices multipleIndicesFromStringArray = new [] { "name1", "name2" };
			Nest.Indices allFromString = "_all";

			Nest.Indices allWithOthersFromString = "_all, name2"; //<1> `_all` will override any specific index names here

			Nest.Indices singleIndexFromType = typeof(Project); //<2> The `Project` type has been mapped to a specific index name using <<index-name-type-mapping,`.DefaultMappingFor<Project>`>>

			Nest.Indices singleIndexFromIndexName = IndexName.From<Project>();

			singleIndexFromString.Should().HaveCount(1).And.Contain("name");

			multipleIndicesFromString.Should().HaveCount(2).And.Contain("name2");

			allFromString.Should().BeNull();

			allWithOthersFromString.Should().BeNull();

			multipleIndicesFromStringArray.Should().HaveCount(2).And.Contain("name2");

			singleIndexFromType.Should().HaveCount(1).And.Contain(typeof(Project));

			singleIndexFromIndexName.Should().HaveCount(1).And.Contain(typeof(Project));
		}

		/**
		* [[nest-indices]]
		*==== Using Nest.Indices methods
		* To make creating `IndexName` or `Indices` instances easier, `Nest.Indices` also contains several static methods
		* that can be used to construct them.
		*
		*===== Single index
		*
		* A single index can be specified using a CLR type or a string, and the `.Index()` method.
		*
		* [TIP]
		* ====
		* This example uses the static import `using static Nest.Indices;` in the using directives to shorthand `Nest.Indices.Index()`
		* to simply `Index()`. Be sure to include this static import if copying any of these examples.
		* ====
		*/
		[U] public void UsingStaticPropertyField()
		{

			var client = TestClient.Default;

			var singleString = new Nest.Indices("name1"); // <1> specifying a single index using a string
			var singleTyped = new Nest.Indices(typeof(Project)); //<2> specifying a single index using a type

			ISearchRequest singleStringRequest = new SearchDescriptor<Project>().Index(singleString);
			ISearchRequest singleTypedRequest = new SearchDescriptor<Project>().Index(singleTyped);

			((IUrlParameter)singleStringRequest.Index).GetString(Client.ConnectionSettings).Should().Be("name1");
			((IUrlParameter)singleTypedRequest.Index).GetString(Client.ConnectionSettings).Should().Be("project");

			var invalidSingleString = new Nest.Indices("name1, name2"); //<3> an **invalid** single index name
		}

		/**===== Multiple indices
		*
		* Similarly to a single index, multiple indices can be specified using multiple CLR types or multiple strings
		*/
		[U] public void MultipleIndices()
		{
			var manyStrings = new Nest.Indices("name1", "name2"); //<1> specifying multiple indices using strings
			var manyTypes = new Nest.Indices(typeof(Project)).And<Developer>(); //<2> specifying multiple indices using types
			var client = TestClient.Default;

			ISearchRequest manyStringRequest = new SearchDescriptor<Project>().Index(manyStrings);
			ISearchRequest manyTypedRequest = new SearchDescriptor<Project>().Index(manyTypes);

			((IUrlParameter)manyStringRequest.Index).GetString(Client.ConnectionSettings).Should().Be("name1,name2");
			((IUrlParameter)manyTypedRequest.Index).GetString(Client.ConnectionSettings).Should().Be("project,devs"); // <3> The index names here come from the Connection Settings passed to `TestClient`. See the documentation on <<index-name-inference, Index Name Inference>> for more details.

			manyStringRequest = new SearchDescriptor<Project>().Index(new[] { "name1", "name2" });
			((IUrlParameter)manyStringRequest.Index).GetString(Client.ConnectionSettings).Should().Be("name1,name2");
		}

		/**===== All Indices
		*
		* Elasticsearch allows searching across multiple indices using the special `_all` marker.
		*
		* NEST exposes the `_all` marker with `Indices.All` and `Indices.AllIndices`. Why expose it in two ways, you ask?
		* Well, you may be using both `Nest.Indices` and `Nest.Types` in the same file and you may also be using C#6
		* static imports too; in this scenario, the `All` property becomes ambiguous between `Indices.All` and `Types.All`, so the
		* `_all` marker for indices is exposed as `Indices.AllIndices`, to alleviate this ambiguity
		*/
		[U]
		public void IndicesAllAndAllIndicesSpecifiedWhenUsingStaticUsingDirective()
		{
			var indicesAll = Nest.Indices.All;
			var allIndices = Nest.Indices.AllIndices;

			ISearchRequest indicesAllRequest = new SearchDescriptor<Project>().Index(indicesAll);
			ISearchRequest allIndicesRequest = new SearchDescriptor<Project>().Index(allIndices);

			((IUrlParameter)indicesAllRequest.Index).GetString(Client.ConnectionSettings).Should().Be("_all");
			((IUrlParameter)allIndicesRequest.Index).GetString(Client.ConnectionSettings).Should().Be("_all");
		}
	}
}
