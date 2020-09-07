using System.Collections;
using System.Linq;
using Ui.Entities;
using Ui.Models;

namespace Ui.Tests.Helpers
{
    public class TestComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var dogOwnerViewModel = (DogOwnerViewModel)x;
            var dogOwner = (DogOwner)y;

            if (dogOwnerViewModel?.OwnerName != dogOwner?.Owner?.OwnerName) return 1;

            var names = dogOwner?.Dogs.Select(dog => dog.DogName).ToList();

            if (names == null || !(dogOwnerViewModel?.DogNames.SequenceEqual(names) ?? false)) return -1;

            return 0;
        }
    }
}