using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphicalProgrammingLanguage;
using System.Collections.Generic;

namespace ProgrammingLanguage
{
    [TestClass]
    public class ProgrammingTests
    {
        ShapeFactory factory = new ShapeFactory();
        CommandParser parser = new CommandParser();
        CustomMethods custom = new CustomMethods();
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
        public void is_Possible_Command()
        {
            //Arrange
            string givenCommand = "movetoo";
            string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE", "PEN", "FILL" };
            bool isCommand = false;

            //Act
            isCommand = custom.isPossibleCommand(possibleCommands, givenCommand);

            //Assert
            Assert.IsFalse(isCommand);
        }


        [TestMethod]
        public void isValidVariable()
        {
            //Arrange
            string varName = "VAR2";
            int expectedResult = 30;
            Dictionary<string, int> varDictionary = new Dictionary<string, int>();
            varDictionary.Add("VAR1", 20);
            varDictionary.Add("VAR2", 30);
            varDictionary.Add("VAR3", 40);

            //Act
            string[] parameter = CustomMethods.getValueFromDictionary(varDictionary, varName);
            int resultVar = int.Parse(parameter[0]);

            //Assert
            Assert.AreEqual(resultVar, expectedResult);
        }

        [TestMethod]
        public void testOperator()
        {
            //Arrange
            string opperator = "<";
            int num1 = 20;
            int num2 = 100;
            bool result = false;

            //Act
            result = custom.checkForOperator(opperator, num1, num2);

            //Assert
            Assert.IsTrue(result);
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
        public void is_Triangle()
        {
            //Arrange
            Shape newShape = factory.getShape("triangle");
            bool isTriangle = false;

            //Act
            if (newShape is Triangle)
            {
                isTriangle = true;
            }

            //Assert
            Assert.IsTrue(isTriangle);
        }
    }
}
