using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(IndicesMultiSyntaxFormatter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Indices : IUrlParameter, IReadOnlyList<IndexName> {
		private readonly List<IndexName> _indices;

		public Indices(IndexName index) : this(new[] { index }) { }
		public Indices(params IndexName[] indices) : this((IEnumerable<IndexName>)indices) { }
		public Indices(IEnumerable<IndexName> indices) => _indices = indices.ToList();
		public Indices(string index) : this((IndexName)index) { }
		public Indices(params string[] indices) : this((IEnumerable<string>)indices) { }
		public Indices(IEnumerable<string> indices) => _indices = indices.Select(i => (IndexName)i).ToList();
		
		public static Indices All { get; } = new Indices(IndexName.All);
		public static Indices AllIndices { get; } = All;

		private string DebugDisplay =>
			$"Count: {_indices.Count} [" + string.Join(",", _indices.Select((t, i) => $"({i + 1}: {t.DebugDisplay})")) + "]";

		public override string ToString() => DebugDisplay;

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) {
			if (_indices.Count == 1 && _indices[0].Name == "_all")
				return "_all";

			if (!(settings is IConnectionSettingsValues nestSettings))
				throw new Exception("Tried to pass index names on querysting but it could not be resolved because no nest settings are available");

			var infer = nestSettings.Inferrer;
			var indices = _indices.Select(i => infer.IndexName(i)).Distinct();
			return string.Join(",", indices);
		}

		public static Indices Parse(string indicesString) {
			if (indicesString.IsNullOrEmptyCommaSeparatedList(out var indices)) return null;
			return indices.Contains("_all") ? All : new Indices(indices.Select(i => (IndexName)i));
		}

		public static implicit operator Indices(string indicesString) => Parse(indicesString);
		
		public static implicit operator Indices(string[] many) => many.IsEmpty() ? null : new Indices(many);

		public static implicit operator Indices(IndexName[] many) => many.IsEmpty() ? null : new Indices(many);

		public static implicit operator Indices(IndexName index) => index == null ? null : new Indices(new[] { index });

		public static implicit operator Indices(Type type) => type == null ? null : new Indices(new IndexName[] { type });
		public static Indices Index<T>() => new Indices(new IndexName[] { typeof(T) });

		public static bool operator ==(Indices left, Indices right) => Equals(left, right);

		public static bool operator !=(Indices left, Indices right) => !Equals(left, right);

		public override bool Equals(object obj) => obj is Indices other && EqualsAllIndices(_indices, other._indices);

		private static bool EqualsAllIndices(IReadOnlyList<IndexName> thisIndices, IReadOnlyList<IndexName> otherIndices) {
			if (thisIndices == null && otherIndices == null) return true;
			if (thisIndices == null || otherIndices == null) return false;

			return thisIndices.Count == otherIndices.Count && !thisIndices.Except(otherIndices).Any();
		}

		public override int GetHashCode() => string.Concat(_indices.OrderBy(i => i.ToString())).GetHashCode();

		public Indices And<T>() => And(typeof(T));

		public Indices And(IndexName index) {
			_indices.Add(index);
			return this;
		}

		// IReadOnlyList<IndexName> members
		public IEnumerator<IndexName> GetEnumerator()  => _indices.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _indices.GetEnumerator();
		public int Count => _indices.Count;
		public IndexName this[int index] => _indices[index];

	}
}
