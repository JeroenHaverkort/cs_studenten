using MemoryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryLibraryTests
{
    public class TurnTests
    {
        [Fact]
        public void Turn_AddPick_ShouldThrowInvalidOperationException_WhenTurnIsComplete()
        {
            // Arrange
            var turn = new Turn();
            var card1 = new Card() { Value = "A" };
            var card2 = new Card() { Value = "B" };

            turn.Picks.Add(new Pick { Card = card1 });
            turn.Picks.Add(new Pick { Card = card2 });

            // Act
            var addPick = () => turn.AddPick(new Card());

            // Assert
            Assert.Throws<InvalidOperationException>(addPick);
        }

        [Fact]
        public void Turn_AddPick_ShouldThrowInvalidOperationException_WhenSameCardIsPickedTwice()
        {
            // Arrange
            var turn = new Turn();
            var guid = Guid.NewGuid();
            var card = new Card() { Id = guid };

            turn.Picks.Add(new Pick { Card = card });

            // Act
            var addPick = () => turn.AddPick(card);

            // Assert
            Assert.Throws<InvalidOperationException>(addPick);
        }

        [Fact]
        public void Turn_EvaluatePicks_ShouldSetIsMatchToTrue_WhenPicksMatch()
        {
            // Arrange
            var turn = new Turn();
            var card1 = new Card() { Value = "A" };
            var card2 = new Card() { Value = "A" };

            turn.Picks.Add(new Pick { Card = card1 });
            turn.Picks.Add(new Pick { Card = card2 });

            // Act
            turn.EvaluatePicks();

            // Assert
            Assert.True(turn.IsMatch);
        }
    }
}
