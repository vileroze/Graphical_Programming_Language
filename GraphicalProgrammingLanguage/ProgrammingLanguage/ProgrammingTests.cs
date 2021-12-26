using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphicalProgrammingLanguage;

namespace ProgrammingLanguage
{
    [TestClass]
    public class ProgrammingTests
    {
        ShapeFactory factory = new ShapeFactory();
        CommandParser parser = new CommandParser();
        //Form1 form = new();


        [TestMethod]
        public void ShapeFactory_Return_Shape()
        {
            // Arrange
            string shapePassed = "Circe";
            

            // Act and Assert
            Assert.ThrowsException<System.ArgumentException>(() => factory.getShape(shapePassed));
        }


        [TestMethod]
        public void is_Circle()
        {
            //Arrange
            Shape newShape = factory.getShape("Circle");
            bool isCircle = false;

            //Act
            if(newShape is Circle)
            {
                isCircle = true;
            }

            //Assert
            Assert.IsTrue(isCircle);
        }



        [TestMethod]
        public void is_Rectangle()
        {
            //Arrange
            Shape newShape = factory.getShape("rectangle");
            bool isRectangle = false;

            //Act
            if (newShape is Circle)
            {
                isRectangle = false;
            }

            //Assert
            Assert.IsFalse(isRectangle);
        }


        [TestMethod]
        public void is_Possible_Command()
        {
            //Arrange
            string givenCommand = "moveto";
            string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE" };
            bool isCommand = false;

            //Act
            isCommand = parser.isPossibleCommand(possibleCommands, givenCommand);

            //Assert
            Assert.IsTrue(isCommand);

        }
    }
}
