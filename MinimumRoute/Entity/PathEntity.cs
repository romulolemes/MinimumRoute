using MinimumRoute.Model;
using MinimumRoute.Search.Dijkstra;
using MinimumRoute.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimumRoute.Entity
{
    public class PathEntity : ISerializer
    {
        public PathEntity()
        {
            NodeVisit = new HashSet<Node>();
        }

        public ICollection<Node> NodeVisit { get; set; }
        public int? Distance { get; set; }

        public string Serializer(Func<object, string> deserializeField)
        {
            StringBuilder textDeserialize = new StringBuilder();
            foreach (var node in NodeVisit)
            {
                textDeserialize.Append(deserializeField(node.Code));
            }
            textDeserialize.Append(deserializeField(Distance));

            return textDeserialize.ToString();
        }

        public override string ToString()
        {
            return $"{string.Join(" ", NodeVisit)} {Distance}";
        }
    }
}
