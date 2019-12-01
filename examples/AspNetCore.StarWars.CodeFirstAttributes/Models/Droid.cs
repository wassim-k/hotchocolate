using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using StarWars.Types;

namespace StarWars.Models
{
    /// <summary>
    /// A droid in the Star Wars universe.
    /// </summary>
    public class Droid
       : ICharacter
    {
        public Droid(
            string id,
            string name,
            IReadOnlyList<string> friends,
            IReadOnlyList<Episode> appearsIn,
            string primaryFunction,
            double height)
        {
            Id = id;
            Name = name;
            Friends = friends;
            AppearsIn = appearsIn;
            PrimaryFunction = primaryFunction;
            Height = height;
        }

        /// <inheritdoc />
        [GraphQLType(typeof(NonNullType<IdType>))]
        public string Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        [UsePaging(typeof(ICharacter))]
        public IReadOnlyList<string> Friends { get; set; }

        /// <inheritdoc />
        public IReadOnlyList<Episode> AppearsIn { get; set; }

        /// <summary>
        /// The droid's primary function.
        /// </summary>
        public string PrimaryFunction { get; set; }

        /// <inheritdoc />
        [UseCalculateUnit]
        public double Height { get; } = 1.72d;
    }
}
